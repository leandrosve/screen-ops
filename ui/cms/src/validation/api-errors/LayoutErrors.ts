export const LayoutErrors: Record<string, string> = {
  name_required:
    "El nombre es obligatorio. Por favor, ingrese un nombre para el layout.",
  name_max_length:
    "El nombre es demasiado largo. El máximo permitido es 100 caracteres.",
  name_already_exists:
    "Ya existe un layout con este nombre. Por favor, elija otro nombre.",

  elements_required:
    "Debe incluir elementos en el layout. No puede estar vacío.",
  elements_too_many:
    "El layout tiene demasiados elementos. El máximo permitido es 500.",

  seat_label_required: "Cada asiento debe tener una etiqueta identificadora.",
  seat_label_duplicated:
    "Hay etiquetas de asiento duplicadas. Cada asiento debe tener una etiqueta única.",

  dimensions_too_small:
    "Las dimensiones son demasiado pequeñas. Mínimo requerido: 5 filas × 5 columnas.",
  not_enough_seats:
    "No hay suficientes asientos. Se requieren al menos 10 asientos válidos.",

  duplicate_positions:
    "Hay posiciones duplicadas en el layout. Cada asiento debe tener una posición única.",
  missing_seat_positions:
    "Faltan posiciones de asientos. Complete todas las posiciones requeridas.",
};
