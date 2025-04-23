import ApiService, { APIResponse } from "./ApiService";
import PagedResult from "@/model/common/PagedResult";
import Movie, { MovieSearchFilters } from "@/model/movies/Movie";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import MovieUpdateDto from "@/model/movies/MovieUpdateDto";

export default class MoviesService extends ApiService {
  protected static PATH = "/movies";

  static async getMovies(filters: MovieSearchFilters): Promise<APIResponse<PagedResult<Movie>>> {
    return this.get("", this.buildQueryParams(filters));
  }

  static async getMovieDetail(id: string): Promise<APIResponse<Movie>> {
    return this.get(`/${id}`);
  }

  static async create(dto: MovieCreateDto, forceCreate: boolean): Promise<APIResponse<Movie>> {
    return this.post("", {...dto, forceCreate});
  }

  static async update(id: string, dto: MovieUpdateDto, forceUpdate: boolean): Promise<APIResponse<Movie>> {
    return this.patch(`/${id}`, {...dto, forceUpdate});
  }
}
