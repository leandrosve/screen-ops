export type MediaType = "POSTER" | "TRAILER" | "BACKDROP";
export enum MovieStatus {
  DRAFT = 0,
  PUBLISHED = 1,
  HIDDEN = 2,
}

export interface Genre {
  id: number;
  name: string;
}

export interface Country {
  name: string;
  code: string;
}

export interface Language {
  code: string;
  name: string;
}

export interface Media {
  id: number;
  type: MediaType;
  url: string;
}

interface Movie {
  id: string; // UUID
  originalTitle: string;
  localizedTitle: string;
  description: string;
  director: string;
  mainActors: string;
  duration: number; // en minutos
  originalReleaseYear: number;
  genres: Genre[];
  country: Country;
  originalLanguage: Language;
  trailerUrl: string;
  posterUrl: string;
  extraImageUrls: string[];
  status: MovieStatus;
  createdAt: Date;
  deletedAt: Date | null;
}

export interface MovieSearchFilters {
  searchTerm?: string;
  includeDeleted?: boolean;
  status?: MovieStatus[];
  page: number;
  pageSize: number;
}

export default Movie;
