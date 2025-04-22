import { ReactElement, Suspense, useContext } from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Layout from "../layout/Layout";
import routes, { CmsRoutes } from "./routes";
import { Flex, Spinner } from "@chakra-ui/react";
import SessionContext from "../context/SessionContext";
import Role from "../model/user/Role";

const CmsRouter = () => {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          {routes.map((route) => {
            const key = `${route.type}-${route.path}`;
            return (
              <Route
                path={`${route.path}${route.hasSubroutes ? "/*" : ""}`}
                key={key}
                element={
                  <RouteProtection
                    type={route.type}
                    roles={route.roles ?? []}
                    element={
                      <Suspense fallback={<Fallback />}>
                        {route.element}
                      </Suspense>
                    }
                  />
                }
              />
            );
          })}
        </Routes>
      </Layout>
    </BrowserRouter>
  );
};

const Fallback = () => (
  <Flex grow={1} align="center" justify={"center"} alignSelf="stretch">
    <Spinner mt={5} size="xl" color="primary.400" boxSize={["50px", 100]} />
  </Flex>
);
function RouteProtection({
  type,
  element,
  roles,
}: {
  type: string;
  element: ReactElement;
  roles: Role[];
}) {
  const { session } = useContext(SessionContext);

  // 1. Primero verificar rutas guest
  if (type === "guest" && session) {
    return <Navigate to={CmsRoutes.MOVIES} replace />;
  }

  // 2. Luego verificar rutas privadas
  if (type === "private" && !session) {
    return <Navigate to={CmsRoutes.LOGIN} replace />;
  }

  // 3. Finalmente verificar roles (solo si hay sesión)
  if (
    roles.length > 0 &&
    (!session || !session.user || !roles.includes(session.user.role))
  ) {
    return <Navigate to={CmsRoutes.LOGIN} replace />;
  }

  return element;
}

export default CmsRouter;
