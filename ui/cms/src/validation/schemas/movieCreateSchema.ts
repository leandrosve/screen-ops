import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import * as yup from "yup";

export const movieCreateSchema = yup.object<MovieCreateDto>().shape({
  originalTitle: yup
    .string()
    .required("El título original es obligatorio")
    .max(255, "El título original no puede superar los 255 caracteres"),

  localizedTitle: yup
    .string()
    .required("El título localizado es obligatorio")
    .max(255, "El título localizado no puede superar los 255 caracteres"),

  description: yup
    .string()
    .required("La descripción es obligatoria")
    .max(1000, "La descripción no puede superar los 1000 caracteres"),

  director: yup
    .string()
    .required("El director es obligatorio")
    .max(255, "El director no puede superar los 255 caracteres"),

  mainActors: yup
    .string()
    .required("Los actores principales son obligatorios")
    .max(500, "Los actores principales no pueden superar los 500 caracteres"),

  duration: yup
    .number()
    .typeError("Debes ingresar una duración válida")
    .required("La duración es obligatoria")
    .moreThan(0, "La duración debe ser mayor a 0"),

  originalReleaseYear: yup
    .number()
    .typeError("Debes ingresar un año válido")
    .required("El año de estreno es obligatorio")
    .min(1888, "El año de estreno no puede ser anterior a 1888")
    .max(
      new Date().getUTCFullYear() + 1,
      `El año de estreno no puede ser posterior a ${
        new Date().getUTCFullYear() + 1
      }`
    ),

  countryCode: yup.string().required("El código de país es obligatorio"),

  originalLanguageCode: yup
    .string()
    .required("El código de idioma original es obligatorio"),

  trailerUrl: yup
    .string()
    .required("La URL del trailer es obligatoria")
    .url("La URL no es válida")
    .matches(
      /(?:youtube\.com\/watch\?v=|youtu\.be\/)([\w-]+)/,
      "La URL no corresponde a un video de YouTube."
    ),
  posterUrl: yup
    .string()
    .required("La URL del poster es obligatoria")
    .url("La URL no es válida")
    .matches(
      /\.(jpeg|jpg|gif|png|webp|bmp|svg|avif)$/i,
      "La URL debe ser de una imagen válida"
    ),

  extraImageUrls: yup.array().default([]),

  genreIds: yup
    .array()
    .of(yup.number().required("Cada género debe ser un número"))
    .required("Debe seleccionar al menos un género")
    .min(1, "Debe seleccionar al menos un género"),
});
