export const MovieCreateErrors: Record<string, string> = {
    original_title_required: "El título original es obligatorio.",
    original_title_max_length: "El título original es demasiado largo.",
    original_title_repeated_use_force_create: "El título original ya existe. Usa 'forzar creación' si deseas continuar.",
  
    localized_title_required: "El título localizado es obligatorio.",
    localized_title_max_length: "El título localizado es demasiado largo.",
  
    description_required: "La descripción es obligatoria.",
    description_max_length: "La descripción es demasiado larga.",
  
    director_required: "El nombre del director es obligatorio.",
    director_max_length: "El nombre del director es demasiado largo.",
  
    main_actors_required: "Los actores principales son obligatorios.",
    main_actors_max_length: "Los actores principales son demasiado largos.",
  
    duration_required: "La duración es obligatoria.",
    duration_must_be_positive: "La duración debe ser un número positivo.",
  
    release_year_required: "El año de estreno es obligatorio.",
    release_year_invalid: "El año de estreno no es válido.",
  
    country_code_required: "El país es obligatorio.",
    country_code_invalid: "El país seleccionado no es válido.",
  
    language_code_required: "El idioma es obligatorio.",
    language_code_invalid: "El idioma seleccionado no es válido.",
  
    trailer_url_required: "La URL del tráiler es obligatoria.",
    trailer_url_invalid: "La URL del tráiler no es válida.",
  
    poster_url_required: "La URL del póster es obligatoria.",
    poster_url_invalid: "La URL del póster no es válida.",
  
    extra_image_url_invalid: "Una de las imágenes adicionales no es válida.",
  
    genres_required: "Debes seleccionar al menos un género.",
    genre_id_invalid: "Alguno de los géneros seleccionados no es válido.",
  };
