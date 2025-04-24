import { APIResponse } from "@/services/api/ApiService";
import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";

interface Props<T> {
  fetchFunction: (id:string) => Promise<APIResponse<T>>;
  notFoundError?: string;
}
const useEntityDetail = <T>({ fetchFunction, notFoundError }: Props<T>) => {
  const { id } = useParams<{ id: string }>();

  const [data, setData] = useState<T | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEntity = async () => {
    if (!id) {
      setError(notFoundError ?? "No se ha encontrado lo que buscas...");
      return;
    }

    setLoading(true);
    setError(null);
    const res = await fetchFunction(id);

    setLoading(false);
    if (res.data) {
      setData(res.data);
      return;
    }
    if (res.hasError && res.error.endsWith("not_found")) {
      setError(notFoundError ?? "No se ha encontrado lo que buscas...");
      return;
    }
    setError("Lo sentimos, algo salió mal.");
  };

  const setEntity = useCallback((entity: T) => {
    setData(entity);
  }, []);

  useEffect(() => {
    fetchEntity();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    entity: data,
    loading,
    error,
    setEntity,
  };
};

export default useEntityDetail;
