import { EntityStatus } from "../common/EntityStatus";
import { MovieCreateDto } from "./MovieCreateDto";

interface MovieUpdateDto extends Partial<MovieCreateDto> {
  status?: EntityStatus;
}

export default MovieUpdateDto;
