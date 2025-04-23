import { MovieStatus } from "@/model/movies/Movie";
import { Badge } from "@chakra-ui/react";

interface Props {
  status: MovieStatus;
}

const options = {
  [MovieStatus.DRAFT]: {
    label: "BORRADOR",
    color: "pink",
  },
  [MovieStatus.PUBLISHED]: {
    label: "PUBLICADA",
    color: "green",
  },
  [MovieStatus.HIDDEN]: {
    label: "OCULTA",
    color: "red",
  },
};

const MovieStatusBadge = ({ status }: Props) => {
  return <Badge paddingX={4} variant="subtle" colorPalette={options[status]?.color}>{options[status]?.label}</Badge>;
};

export default MovieStatusBadge;
