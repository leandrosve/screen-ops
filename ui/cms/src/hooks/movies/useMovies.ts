import PagedResult from "@/model/common/PagedResult";
import Movie, { MovieSearchFilters } from "@/model/movies/Movie";
import MoviesService from "@/services/api/MoviesService";
import NumberUtils from "@/utils/NumberUtils";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

export function useMovies() {
  const [searchParams, setSearchParams] = useSearchParams();
  const [data, setData] = useState<PagedResult<Movie> | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getFiltersFromParams = (): MovieSearchFilters => {
    var status = (searchParams.get("status") ?? "")
      .split(",")
      .map((s) => parseInt(s))
      .filter((s) => !isNaN(s));

    var page = NumberUtils.safeParsePositiveNumber(searchParams.get("page"), 1, 1);
    var pageSize = NumberUtils.safeParsePositiveNumber(searchParams.get("pageSize"), 10, 10, 100);
    return {
      searchTerm: searchParams.get("searchTerm") || undefined,
      includeDeleted: searchParams.get("includeDeleted") === "true",
      page: page,
      pageSize: pageSize,
      status: status,
    };
  };
  const fetchMovies = async () => {
    setLoading(true);
    setError(null);
    try {
      const filters = getFiltersFromParams();
      const response = await MoviesService.getMovies(filters);
      if (response.data) setData(response.data);
    } catch (err) {
      setError("Lo sentimos, ha ocurrido un problema. Intentalo más tarde.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMovies();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchParams.toString()]);

  const updateFilters = (newFilters: Partial<MovieSearchFilters>) => {
    const currentFilters = getFiltersFromParams();
    const updated = { ...currentFilters, ...newFilters };

    const params = new URLSearchParams();
    if (updated.searchTerm) params.set("searchTerm", updated.searchTerm);
    if (updated.includeDeleted)
      params.set("includeDeleted", String(updated.includeDeleted));
    if (updated.status?.length) params.set("status", updated.status.join(","));

    params.set("page", String(updated.page));
    params.set("pageSize", String(updated.pageSize));

    setSearchParams(params);
  };

  return {
    data,
    loading,
    error,
    updateFilters,
    filters: getFiltersFromParams(),
  };
}
