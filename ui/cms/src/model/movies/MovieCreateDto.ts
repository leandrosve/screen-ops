export interface MovieMediaCreateDto {
  type: string;
  url: string;
}



export interface MovieCreateDto {
  originalTitle: string;
  localizedTitle: string;
  description: string;
  director: string;
  mainActors: string;
  duration: number;
  originalReleaseYear: number;
  countryCode: string;
  originalLanguageCode: string;
  forceCreate?: boolean;
  trailerUrl: string;
  posterUrl: string;
  extraImageUrls: string[]
  genreIds: number[];

}
