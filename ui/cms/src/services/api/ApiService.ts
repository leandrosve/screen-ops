import Logger from "@/utils/Logger";
import SessionService from "../SessionService";

type APISuccessfulResponse<T> = {
  status: number;
  data: T;
  hasError: false;
};
type APIErrorResponse<T> = {
  status: number;
  data?: T;
  error: string;
  hasError: true;
};
export type APIResponse<T> = APIErrorResponse<T> | APISuccessfulResponse<T>;

interface Options {
  preventSignOut?: boolean;
}

export default abstract class ApiService {
  protected static BASE_URL = `${import.meta.env.VITE_APP_API_BASE_URL}/api`;

  protected static PATH: string;

  protected static token?: string;

  public static initialize() {
    this.token = SessionService.getLocalSession()?.token;
  }

  protected static delay(ms: number) {
    return new Promise((res) => setTimeout(res, ms));
  }

  // COMMON METHODS HERE
  private static async doFetch<T>(
    method: string,
    path: string,
    params?: string,
    body?: any,
    options?: Options
  ): Promise<APIResponse<T>> {
    const headers: HeadersInit = new Headers();
    headers.set("Content-Type", "application/json");
    if (this.token) headers.set("Authorization", "Bearer " + this.token);

    const url = `${this.BASE_URL}${this.PATH || ""}${path || ""}${
      params ? params : ""
    }`;
    try {
      const res = await fetch(url, {
        method: method,
        headers: headers,
        body: JSON.stringify(body),
      });

      if (!res.ok) {
        if (res.status == 401 && !options?.preventSignOut) {
          Logger.info("User is unauthorized --> returning to log in");
          SessionService.destroyLocalSession();
          location.href = "/login?tokenExpired=true";
        } else {
          const responseBody = await res.json();
          return {
            status: res.status,
            error: responseBody["error"],
            hasError: true,
          } as APIErrorResponse<T>;
        }
      }
      const responseBody = await res.json();
      return {
        status: res.status,
        data: responseBody as T,
        hasError: false,
      } as APISuccessfulResponse<T>;
    } catch (err) {
      console.log(err);
      return { status: 400, hasError: true, error: "unknown_error" };
    }
  }

  protected static get<T>(path: string, params?: string) {
    return this.doFetch<T>("GET", path, params);
  }

  protected static post<T>(path: string, body?: any, options?: Options) {
    return this.doFetch<T>("POST", path, undefined, body, options);
  }

  protected static patch<T>(path: string, body?: any, options?: Options) {
    return this.doFetch<T>("PATCH", path, undefined, body, options);
  }

  protected static buildQueryParams<T extends Record<string, any>>(
    filters: T
  ): string {
    const params = new URLSearchParams();

    for (const [key, value] of Object.entries(filters)) {
      if (value === undefined || value === null) continue;

      if (Array.isArray(value)) {
        // Maneja arrays: ?genre=1&genre=2
        value.forEach((v) => params.append(key, v));
      } else if (value instanceof Date) {
        // Maneja fechas: ?from=2024-01-01
        params.append(key, value.toISOString().split("T")[0]);
      } else if (typeof value === "object") {
        // Maneja objetos anidados (opcional): ?country.code=US
        for (const [nestedKey, nestedValue] of Object.entries(value)) {
          if (nestedValue !== undefined && nestedValue !== null) {
            params.append(`${key}.${nestedKey}`, String(nestedValue));
          }
        }
      } else {
        // Valores primitivos: ?page=1
        params.append(key, String(value));
      }
    }

    return params.toString() ? `?${params.toString()}` : "";
  }
}
