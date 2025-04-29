import { RoomCreateDto } from "@/model/room/Room";
import * as yup from "yup";
export const roomCreateSchema = yup.object<RoomCreateDto>().shape({
  name: yup
    .string()
    .required("El nombre de la sala es obligatorio")
    .max(100, "El nombre no puede superar los 100 caracteres"),
  description: yup
    .string()
    .required("La descripción de la sala es obligatoria")
    .max(500, "La descripción no puede superar los 500 caracteres"),

  cinemaId: yup
    .string()
    .required("El cine es obligatorio"),
  layoutId: yup
    .string().required("El layout es obligatorio"),
});
