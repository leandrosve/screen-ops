import { EntityStatus } from "@/model/common/EntityStatus";

export function toEntityStatus(stringArray: string[]): EntityStatus[] {
  return stringArray
    .map((str) => {
      // Primero intentamos convertir el string a número
      const num = Number(str);

      // Verificamos si el número existe en los valores del enum
      if (!isNaN(num) && Object.values(EntityStatus).includes(num)) {
        return num as EntityStatus;
      }

      // Opcional: Si los strings coinciden con los nombres de las keys
      if (str in EntityStatus) {
        return EntityStatus[str as keyof typeof EntityStatus];
      }

      return null; // O undefined si prefieres
    })
    .filter((item): item is EntityStatus => item !== null); // Filtramos valores nulos
}
