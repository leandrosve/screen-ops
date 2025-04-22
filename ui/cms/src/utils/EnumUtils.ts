import { MovieStatus } from "@/model/movies/Movie";

export function toMovieStatus(stringArray: string[]): MovieStatus[] {
  return stringArray
    .map((str) => {
      // Primero intentamos convertir el string a número
      const num = Number(str);

      // Verificamos si el número existe en los valores del enum
      if (!isNaN(num) && Object.values(MovieStatus).includes(num)) {
        return num as MovieStatus;
      }

      // Opcional: Si los strings coinciden con los nombres de las keys
      if (str in MovieStatus) {
        return MovieStatus[str as keyof typeof MovieStatus];
      }

      return null; // O undefined si prefieres
    })
    .filter((item): item is MovieStatus => item !== null); // Filtramos valores nulos
}
