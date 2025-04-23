import { ReactElement, Suspense, useContext } from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Layout from "../layout/Layout";
import routes, { CmsRoutes } from "./routes";
import SessionContext from "../context/SessionContext";
import Role from "../model/user/Role";
import { Box } from "@chakra-ui/react";

const CmsRouter = () => {
  return (
    <BrowserRouter>
      <Layout>
        <Suspense
        key='what'
          fallback={
            <Box
              width="100vw"
              height="100vh"
              bg="red"
              position="absolute"
              zIndex={10000}
            >
              {" "}
            </Box>
          }
        >
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
                      element={route.element}
                    />
                  }
                />
              );
            })}
          </Routes>
        </Suspense>
      </Layout>
    </BrowserRouter>
  );
};

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
