export const CinemaErrors: Record<string, string> = {
  name_required: "El nombre del cine es obligatorio.",
  name_max_length: "El nombre del cine es demasiado largo.",
  name_already_exists: "Ya existe un cine con este nombre.",

  location_required: "La ubicación del cine es obligatoria.",
  location_max_length: "La ubicación del cine es demasiado larga.",

  description_required: "La descripción del cine es obligatoria.",
  description_max_length: "La descripción del cine es demasiado larga.",

  capacity_invalid:
    "La capacidad del cine debe ser un número válido mayor a cero.",
  image_url_invalid: "La URL de la imagen proporcionada no es válida.",

  cinema_not_found: "No se encontró el cine solicitado.",

  name_min_length: "El nombre del cine es demasiado corto.",

  location_min_length: "La ubicación del cine es demasiado corta.",

  description_min_length: "La descripción del cine es demasiado corta.",

  status_invalid: "El estado proporcionado no es válido.",
};
