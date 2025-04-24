import { Cinema, CinemaCreateDto, CinemaUpdateDto } from "@/model/cinema/Cinema";
import ApiService, { APIResponse } from "./ApiService";

export default class CinemaService extends ApiService {
  protected static PATH = "/cinemas";

  static async getCinemas(): Promise<APIResponse<Cinema[]>> {
    return this.get("");
  }

  static async getDetail(id: string): Promise<APIResponse<Cinema>> {
    return this.get(`/${id}`);
  }

  static async create(dto: CinemaCreateDto): Promise<APIResponse<Cinema>> {
    return this.post("", dto);
  }

  static async update(id: string, dto: CinemaUpdateDto): Promise<APIResponse<Cinema>> {
    return this.patch(`/${id}`, dto);
  }
}
