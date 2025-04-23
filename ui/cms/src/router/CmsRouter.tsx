import { Suspense } from "react";
import {
  createBrowserRouter,
  redirect,
} from "react-router-dom";
import Layout from "../layout/Layout";
import routes, { CmsRoutes } from "./routes";
import Role from "../model/user/Role";
import SessionService from "@/services/SessionService";
import PageLoader from "@/components/common/PageLoader";

type RouteType = "private" | "guest" | "any";

interface RouteProtectionProps {
  type: RouteType;
  roles?: Role[];
}
const protectRoutes = async ({ type, roles }: RouteProtectionProps) => {
  const session = SessionService.LOCAL_SESSION;
  // 1. Primero verificar rutas guest
  if (type === "guest" && session) {
    throw redirect(CmsRoutes.MOVIES);
  }

  // 2. Luego verificar rutas privadas
  if (type === "private" && !session) {
    throw redirect(CmsRoutes.LOGIN);
  }

  // 3. Finalmente verificar roles (solo si hay sesión)
  if (
    roles &&
    roles.length > 0 &&
    (!session || !session.user || !roles.includes(session.user.role))
  ) {
    throw redirect(CmsRoutes.LOGIN);
  }

  return null;
};

export const routerV2 = createBrowserRouter([
  {
    path: "/",
    Component: Layout,
    children: routes.map((r) => ({
      path: r.path + (r.hasSubroutes ? "/*" : ""),
      id: r.path,
      lazy: async () => {
        const protection = await protectRoutes({
          type: r.type,
          roles: r.roles,
        });

        if (!r.lazy) {
          return {
            loader: () =>
              protectRoutes({
                type: r.type,
                roles: r.roles,
              }),
            Component: null,
          };
        }

        const LazyComponent = r.lazy;
        return {
          protection,
          Component: () => (
            <Suspense
              fallback={
                <PageLoader/>
              }
            >
              <LazyComponent/>
            </Suspense>
          ),
        };
      },
      hydrateFallbackElement: (
        <PageLoader/>
      ),
    })),
  },
]);
