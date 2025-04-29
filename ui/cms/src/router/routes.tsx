import { JSX, lazy, LazyExoticComponent, ReactElement } from "react";
import Role from "../model/user/Role";

const LoginPage = lazy(() => import("@/components/pages/LoginPage"));
const MoviesPage = lazy(() => import("@/components/pages/movies/MoviesPage"));
const MovieCreatePage = lazy(
  () => import("@/components/pages/movies/MovieCreatePage")
);
const MovieUpdatePage = lazy(
  () => import("@/components/pages/movies/MovieUpdatePage")
);
const MovieDetailPage = lazy(
  () => import("@/components/pages/movies/MovieDetailPage")
);
const CinemasPage = lazy(
  () => import("@/components/pages/cinemas/CinemasPage")
);
const CinemaCreatePage = lazy(
  () => import("@/components/pages/cinemas/CinemaCreatePage")
);
const CinemaDetailPage = lazy(
  () => import("@/components/pages/cinemas/CinemaDetailPage")
);
const CinemaUpdatePage = lazy(
  () => import("@/components/pages/cinemas/CinemaUpdatePage")
);

const LayoutCreatePage = lazy(
  () => import("@/components/pages/layouts/LayoutCreatePage")
);
const LayoutUpdatePage = lazy(
  () => import("@/components/pages/layouts/LayoutUpdatePage")
);

const LayoutPage = lazy(() => import("@/components/pages/layouts/LayoutsPage"));

const LayoutDetailPage = lazy(
  () => import("@/components/pages/layouts/LayoutDetailPage")
);

const RoomCreatePage = lazy(
  () => import("@/components/pages/rooms/RoomCreatePage")
);

const RoomUpdatePage = lazy(
  () => import("@/components/pages/rooms/RoomUpdatePage")
);
const RoomDetailPage = lazy(
  () => import("@/components/pages/rooms/RoomDetailPage")
);
const RoomsPage = lazy(() => import("@/components/pages/rooms/RoomsPage"));

export enum CmsRoutes {
  HOME = "/",
  SIGNUP = "/signup",
  LOGIN = "/login",
  MOVIES = "/movies",
  MOVIE_CREATE = "/movies/create",
  MOVIE_DETAIL = "/movies/detail/:id",
  MOVIE_UPDATE = "/movies/update/:id",
  CINEMAS = "/cinemas",
  CINEMA_CREATE = "/cinemas/create",
  CINEMA_DETAIL = "/cinemas/detail/:id",
  CINEMA_UPDATE = "/cinemas/update/:id",
  LAYOUTS = "/layouts",
  LAYOUT_CREATE = "/layouts/create",
  LAYOUT_DETAIL = "/layouts/detail/:id",
  LAYOUT_UPDATE = "/layouts/update/:id",
  ROOMS = "/rooms",
  ROOM_CREATE = "/rooms/create",
  ROOM_DETAIL = "/rooms/detail/:id",
  ROOM_UPDATE = "/rooms/update/:id",
}

interface CmsRoute {
  path: string;
  type: "private" | "guest" | "any";
  title: string;
  hasSubroutes?: boolean;
  subroutes?: { title: string; path: string }[];
  element?: ReactElement;
  lazy?: LazyExoticComponent<() => JSX.Element>;
  roles?: Role[];
}

const routes: CmsRoute[] = [
  {
    path: CmsRoutes.MOVIE_CREATE,
    type: "private",
    title: "Añadir Película",
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieCreatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.MOVIE_DETAIL,
    type: "private",
    title: "Detalle de Película",
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieDetailPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.MOVIE_UPDATE,
    type: "private",
    title: "Actualizar Película",
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieUpdatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.MOVIES,
    type: "private",
    title: "Películas",
    hasSubroutes: false,
    subroutes: [],
    lazy: MoviesPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.CINEMA_CREATE,
    type: "private",
    title: "Crear Cine",
    hasSubroutes: false,
    subroutes: [],
    lazy: CinemaCreatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.CINEMA_DETAIL,
    type: "private",
    title: "Detalle del cine",
    hasSubroutes: false,
    subroutes: [],
    lazy: CinemaDetailPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.CINEMA_UPDATE,
    type: "private",
    title: "Editar cine",
    hasSubroutes: false,
    subroutes: [],
    lazy: CinemaUpdatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.CINEMAS,
    type: "private",
    title: "Cines",
    hasSubroutes: false,
    subroutes: [],
    lazy: CinemasPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },

  {
    path: CmsRoutes.LAYOUT_CREATE,
    type: "private",
    title: "Crear Layout",
    hasSubroutes: false,
    subroutes: [],
    lazy: LayoutCreatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.LAYOUT_UPDATE,
    type: "private",
    title: "Editar Layout",
    hasSubroutes: false,
    subroutes: [],
    lazy: LayoutUpdatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.LAYOUT_DETAIL,
    type: "private",
    title: "Detalle de Layout",
    hasSubroutes: false,
    subroutes: [],
    lazy: LayoutDetailPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.LAYOUTS,
    type: "private",
    title: "Layouts",
    hasSubroutes: false,
    subroutes: [],
    lazy: LayoutPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.ROOM_CREATE,
    type: "private",
    title: "Crear Sala",
    hasSubroutes: false,
    subroutes: [],
    lazy: RoomCreatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.ROOM_UPDATE,
    type: "private",
    title: "Editar Sala",
    hasSubroutes: false,
    subroutes: [],
    lazy: RoomUpdatePage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.ROOM_DETAIL,
    type: "private",
    title: "Detalle de Sala",
    hasSubroutes: false,
    subroutes: [],
    lazy: RoomDetailPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    path: CmsRoutes.ROOMS,
    type: "private",
    title: "Todas las Salas",
    hasSubroutes: false,
    subroutes: [],
    lazy: RoomsPage,
    roles: [Role.MANAGER, Role.ADMIN],
  },
  {
    type: "guest",
    path: "/",
    title: "Controla el progreso de tus alumnos",
    element: <div>Landing Page</div>,
  },
  {
    type: "guest",
    path: "/signup",
    title: "Sign Up",
    element: <div>Registrarse</div>,
  },
  {
    type: "guest",
    path: "/login",
    title: "Iniciar Sesión",
    lazy: LoginPage,
  },
  {
    path: "/mantenimiento",
    type: "any",
    title: "Mantenimiento",
    element: <div>mantenimiento</div>,
  },
  {
    path: "*",
    type: "any",
    title: "Página no encontrada",
    element: <div>Pagina no encontrada</div>,
  },
];

export default routes;
