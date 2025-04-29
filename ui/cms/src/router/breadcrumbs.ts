import { CmsRoutes } from "./routes";

const moviesBase = { title: "Películas", path: CmsRoutes.MOVIES };
export const movieBreadcrumbs = {
  list: [moviesBase, {
    title:"Todas las películas",
    path: "",
  }],
  detail: (movieId: string, movieTitle: string) => [
    moviesBase,
    {
      title: movieTitle,
      path: CmsRoutes.MOVIE_DETAIL.replace(":id", movieId),
    },
  ],
  update: (movieId: string, movieTitle: string) => [
    moviesBase,
    {
      title: movieTitle,
      path: CmsRoutes.MOVIE_DETAIL.replace(":id", movieId),
    },
    {
      title: "Editar",
      path: "",
    },
  ],
  create: [
    moviesBase,
    {
      title: "Añadir Película",
      path: "",
    },
  ],
};

const cinemaBase = { title: "Cines", path: CmsRoutes.CINEMAS };
export const cinemaBreadcrumbs = {
  list: [cinemaBase, {title: "Todos los cines", path:""}],
  detail: (id: string, title: string) => [
    cinemaBase,
    {
      title: title,
      path: CmsRoutes.CINEMA_DETAIL.replace(":id", id),
    },
  ],
  update: (id: string, title: string) => [
    cinemaBase,
    {
      title: title,
      path: CmsRoutes.CINEMA_DETAIL.replace(":id", id),
    },
    {
      title: "Editar",
      path: "",
    },
  ],
  create: [
    cinemaBase,
    {
      title: "Añadir Cine",
      path: "",
    },
  ],
};


const roomBase = { title: "Salas", path: CmsRoutes.ROOMS };
export const roomBreadcrumbs = {
  list: [roomBase, {title: "Todos las salas", path:""}],
  detail: (id: string, title: string) => [
    roomBase,
    {
      title: title,
      path: CmsRoutes.ROOM_DETAIL.replace(":id", id),
    },
  ],
  update: (id: string, title: string) => [
    roomBase,
    {
      title: title,
      path: CmsRoutes.ROOM_DETAIL.replace(":id", id),
    },
    {
      title: "Editar",
      path: "",
    },
  ],
  create: [
    roomBase,
    {
      title: "Añadir Sala",
      path: "",
    },
  ],
};