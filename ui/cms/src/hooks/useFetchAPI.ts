import { APIResponse } from "@/services/api/ApiService";
import { useCallback, useEffect, useState } from "react";

interface Props<T> {
  fetchFunction: () => Promise<APIResponse<T>>;
  errorMap?: Record<string, string>;
  initialFetch?: boolean;
}

const useFetchAPI = <T>({ fetchFunction, errorMap, initialFetch }: Props<T>) => {
  const [data, setData] = useState<T | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Unificar en un futuro
  const fetchOther = useCallback(async (otherFetchFunction: () => Promise<APIResponse<T>>) => {

    setLoading(true);
    setError(null);
    const res = await otherFetchFunction();

    setLoading(false);
    if (res.data) {
      setData(res.data);
      return;
    }
    
    if (res.hasError) {
      setError(errorMap?.[res.error] ?? "Lo sentimos, algo salió mal.");
    }
  }, [errorMap, fetchFunction]);

  const fetchEntity = useCallback(async () => {

    setLoading(true);
    setError(null);
    const res = await fetchFunction();

    setLoading(false);
    if (res.data) {
      setData(res.data);
      return;
    }
    
    if (res.hasError) {
      setError(errorMap?.[res.error] ?? "Lo sentimos, algo salió mal.");
    }
  }, [errorMap, fetchFunction]);

  const setEntity = useCallback((entity: T) => {
    setData(entity);
  }, []);

  useEffect(() => {
    if (initialFetch) {
      fetchEntity()
    }
  }, [])

  return {
    entity: data,
    loading,
    error,
    fetchEntity,
    setEntity,
    fetchOther
  };
};

export default useFetchAPI;
