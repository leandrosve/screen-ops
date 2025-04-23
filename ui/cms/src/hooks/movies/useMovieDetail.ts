import Movie from "@/model/movies/Movie";
import MoviesService from "@/services/api/MoviesService";
import React, { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
const useMovieDetail = () => {
  const { id } = useParams<{ id: string }>();

  const [data, setData] = useState<Movie | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchMovie = async () => {
    if (!id) {
      setError("No se ha encontrado la película.");
      return;
    }

    setLoading(true);
    setError(null);
    const res = await MoviesService.getMovieDetail(id);

    setLoading(false);
    if (res.data) {
      setData(res.data);
      return;
    }
    if (res.hasError && res.error == "movie_not_found") {
      setError("No se ha encontrado la película");
      return;
    }
    setError("No se ha encontrado la película.");
  };

  const setMovie = useCallback((movie: Movie) => {
    setData(movie);
  }, []);

  useEffect(() => {
    fetchMovie();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    movie: data,
    loading,
    error,
    setMovie
  };
};

export default useMovieDetail;
