import ApiService, { APIResponse } from "./ApiService";
import PagedResult from "@/model/common/PagedResult";
import Movie, { MovieSearchFilters } from "@/model/movies/Movie";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";

export default class MoviesService extends ApiService {
  protected static PATH = "/movies";

  static async getMovies(filters: MovieSearchFilters): Promise<APIResponse<PagedResult<Movie>>> {
    return this.get("", this.buildQueryParams(filters));
  }

  static async create(dto: MovieCreateDto, forceCreate: boolean): Promise<APIResponse<Movie>> {
    return this.post("", {...dto, forceCreate});
  }
}
