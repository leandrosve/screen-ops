import { EntityStatus } from "@/model/common/EntityStatus";
import { MovieStatus } from "@/model/movies/Movie";
import { Badge } from "@chakra-ui/react";

interface Props {
  status: EntityStatus;
  genre?: "femenine" | "masculine";
}

const options = {
  [EntityStatus.DRAFT]: {
    label: "BORRADOR",
    labelMasculine: "BORRADOR",
    color: "pink",
  },
  [EntityStatus.PUBLISHED]: {
    label: "PUBLICADA",
    labelMasculine: "PUBLICADO",
    color: "green",
  },
  [EntityStatus.HIDDEN]: {
    label: "OCULTA",
    labelMasculine: "OCULTO",
    color: "blue",
  },
  [EntityStatus.ARCHIVED]: {
    label: "ARCHIVADA",
    labelMasculine: "ARCHIVADO",
    color: "orange",
  },
  [EntityStatus.DELETED]: {
    label: "ELIMINADA",
    labelMasculine: "ELIMINADO",
    color: "red",
  },
};

const EntityStatusBadge = ({ status, genre }: Props) => {
  return (
    <Badge paddingX={4} variant="subtle" colorPalette={options[status]?.color}>
      {genre == "masculine"
        ? options[status]?.labelMasculine ?? options[status]?.label
        : options[status]?.label}
    </Badge>
  );
};

export default EntityStatusBadge;
