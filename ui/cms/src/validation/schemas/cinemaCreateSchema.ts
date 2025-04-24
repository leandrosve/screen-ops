import { CinemaCreateDto } from "@/model/cinema/Cinema";
import * as yup from "yup";
export const cinemaCreateSchema = yup.object<CinemaCreateDto>().shape({
  name: yup
    .string()
    .required("El nombre del cine es obligatorio")
    .max(100, "El nombre no puede superar los 100 caracteres"),

  location: yup
    .string()
    .required("La ubicación del cine es obligatoria")
    .max(150, "La ubicación no puede superar los 150 caracteres"),

  description: yup
    .string()
    .required("La descripción del cine es obligatoria")
    .max(500, "La descripción no puede superar los 500 caracteres"),

  capacity: yup
    .number()
    .required("La capacidad del cine es obligatoria")
    .moreThan(0, "La capacidad debe ser mayor a 0"),

  imageUrl: yup.string().nullable().url("La URL de la imagen debe ser válida"),
}) as yup.ObjectSchema<CinemaCreateDto>;
