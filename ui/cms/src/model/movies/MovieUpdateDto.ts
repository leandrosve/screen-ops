import { MovieStatus } from "./Movie";
import { MovieCreateDto } from "./MovieCreateDto";

interface MovieUpdateDto extends Partial<MovieCreateDto> {
  status?: MovieStatus;
}

export default MovieUpdateDto;
