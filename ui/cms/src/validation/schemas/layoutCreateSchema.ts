import { LayoutCreateDto } from "@/model/layout/Layout";
import * as yup from "yup";
export const layoutCreateSchema = yup.object<LayoutCreateDto>().shape({
  name: yup
    .string()
    .required("El nombre del layout es obligatorio")
    .max(100, "El nombre no puede superar los 100 caracteres"),
  elements: yup.array().default([]),
});
