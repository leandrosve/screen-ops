import { FaCirclePlay, FaFilm } from "react-icons/fa6";
import { RiBuilding3Line } from "react-icons/ri";
import { SidebarItemGroup } from "./Sidebar";
import Role from "@/model/user/Role";
import { LuTheater } from "react-icons/lu";
import { IoTicketSharp } from "react-icons/io5";

const sidebarItems: SidebarItemGroup[] = [
  {
    group: "Contenidos",
    items: [
      {
        path: "/movies",
        label: "Películas",
        icon: {type:FaFilm, boxSize: '1.25em'},
        roles: [Role.ADMIN, Role.MANAGER],
        subItems: [
          { path: "/movies", label: "Todas las películas" },
          { path: "/movies/create", label: "Añadir una película" },
        ],
      },
      {
        path: "/cinemas",
        label: "Cines",
        roles: [Role.ADMIN, Role.MANAGER],
        icon: {type: RiBuilding3Line},
        subItems: [
          { path: "/cinemas", label: "Todos los cines" },
          { path: "/cinemas/create", label: "Añadir un cine" },
        ],
      },
      {
        path: "/rooms",
        label: "Salas",
        roles: [Role.ADMIN, Role.MANAGER],
        icon: {type: LuTheater, boxSize: '1.3em'},
        subItems: [
          { path: "/rooms", label: "Todas las salas" },
          { path: "/rooms/create", label: "Añadir una sala" },
        ],
      },
      {
        path: "/screenings",
        label: "Funciones",
        roles: [Role.ADMIN, Role.MANAGER],
        icon: {type: FaCirclePlay, boxSize: '1.3em'},
        subItems: [
          { path: "/screenings", label: "Buscar funciones" },
          { path: "/screenings/create", label: "Añadir funciones" },
        ],
      },
      {
        path: "/tickets",
        label: "Tickets",
        roles: [Role.ADMIN, Role.MANAGER],
        icon: {type: IoTicketSharp, boxSize: '1.4em'},
      },
    ],
  },
  {
    group: "Administración",
    items: [
      {
        path: "/users",
        label: "Usuarios",
      },
      {
        path: "/settings",
        label: "Configuraciones",
      },
    ],
  },
];

export default sidebarItems;