import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import MovieForm from "@/components/features/movies/MovieForm";
import { toaster } from "@/components/ui/toaster";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import { CmsRoutes } from "@/router/routes";
import MoviesService from "@/services/api/MoviesService";
import { MovieCreateErrors } from "@/validation/api-errors/MovieErrors";

import { Flex, Heading } from "@chakra-ui/react";
import { useCallback, useState } from "react";
import { useNavigate } from "react-router-dom";

const MovieCreatePage = () => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const { confirm } = useConfirmDialog();

  const submitAsync = useCallback(
    async (data: MovieCreateDto, forceCreate: boolean) => {
      setLoading(true);
      setError("");
      const res = await MoviesService.create(data, forceCreate);
      setLoading(false);
      if (res.hasError) {
        if (res.error == "original_title_repeated_use_force_create") {
          const confirmed = await confirm({
            title: "Advertencia",
            message: `Ya existe una pelicula con título "${data.originalTitle}" ¿Quieres añadirla de todos modos?`,
            confirmText: "Sí, añadir",
            cancelText: "No, cancelar",
          });
          if (confirmed) {
            setLoading(true);
            submitAsync(data, true);
            return;
          }
          return;
        }
        setError(
          MovieCreateErrors[res.error] ??
            "Algo salió mal! Intentalo nuevamente."
        );
      }

      if (res.data) {
        toaster.create({
          title: "Se ha añadido la película",
          type: "success",
          duration: 1500,
        });

        navigate(CmsRoutes.MOVIE_DETAIL.replace(":id", res.data?.id ?? ""));
      }
    },
    [navigate, confirm]
  );

  return (
    <Flex direction="column" gap={3} width="fit-content">
      <Heading size="2xl">Añadir Pelicula</Heading>
      <MovieForm
        onSubmit={(data) => submitAsync(data, false)}
        isSubmitting={loading}
        error={error}
      />
    </Flex>
  );
};

export default MovieCreatePage;
