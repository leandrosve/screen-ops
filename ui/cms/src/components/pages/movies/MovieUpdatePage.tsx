import Alert from "@/components/common/Alert";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import PageLoader from "@/components/common/PageLoader";
import MovieForm from "@/components/features/movies/MovieForm";
import { toaster } from "@/components/ui/toaster";
import useMovieDetail from "@/hooks/movies/useMovieDetail";
import PageContent from "@/layout/PageContent";
import Movie from "@/model/movies/Movie";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import { CmsRoutes } from "@/router/routes";
import MoviesService from "@/services/api/MoviesService";
import DtoUtils from "@/utils/DtoUtils";
import { MovieCreateErrors } from "@/validation/api-errors/MovieErrors";

import { Flex, Heading } from "@chakra-ui/react";
import { useCallback, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

const MovieUpdatePage = () => {
  const { movie, loading, error } = useMovieDetail();

  if (loading) return <PageLoader />;

  if (error || !movie)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado la película"}
      />
    );

  if (!loading && movie) {
    return <MovieUpdateForm movie={movie} />;
  }
  return <></>;
};

const mapMovieToMovieCreateDto = (movie: Movie): MovieCreateDto => {
  return {
    originalTitle: movie.originalTitle,
    localizedTitle: movie.localizedTitle,
    description: movie.description,
    director: movie.director,
    mainActors: movie.mainActors, // Se convierte el array en un string
    duration: movie.duration,
    originalReleaseYear: movie.originalReleaseYear,
    countryCode: movie.country.code, // Suponiendo que 'country' tiene un campo 'code'
    originalLanguageCode: movie.originalLanguage.code, // Suponiendo que 'originalLanguage' tiene un campo 'code'
    trailerUrl: movie.trailerUrl,
    posterUrl: movie.posterUrl,
    extraImageUrls: movie.extraImageUrls,
    genreIds: movie.genres.map((genre) => genre.id), // Suponiendo que 'genre' tiene un campo 'id'
  };
};

const breadcrumbs = [{title: "Películas", path: CmsRoutes.MOVIES}]

const MovieUpdateForm = ({ movie }: { movie: Movie }) => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const { confirm } = useConfirmDialog();

  const initialValues = useMemo(() => {
    return mapMovieToMovieCreateDto(movie);
  }, [movie]);

  const submitAsync = useCallback(
    async (data: MovieCreateDto, forceUpdate: boolean) => {
      const modifiedFields = DtoUtils.getModifiedFields(initialValues, data);

      console.log({ modifiedFields });

      setLoading(true);
      setError("");

      const res = await MoviesService.update(
        movie.id,
        modifiedFields,
        forceUpdate
      );
      setLoading(false);

      if (res.hasError) {
        if (res.error == "original_title_repeated_use_force_update") {
          const confirmed = await confirm({
            title: "Advertencia",
            message: `Ya existe una pelicula con título "${data.originalTitle}" ¿Quieres continuar de todos modos?`,
            confirmText: "Sí, continuar",
            cancelText: "No, cancelar",
          });
          if (confirmed) {
            setLoading(true);
            submitAsync(data, true);
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
          title: "Se ha actualizado la película",
          type: "success",
          duration: 1500,
        });

        navigate(CmsRoutes.MOVIE_DETAIL.replace(":id", res.data?.id ?? ""));
      }
    },
    [navigate, confirm]
  );

  return (
    <PageContent>
      <Flex direction="column" gap={3} width="fit-content">
        <Breadcrumb items={[...breadcrumbs, {title: movie.localizedTitle, path: CmsRoutes.MOVIE_DETAIL.replace(":id", movie.id)}, {title:"Editar", path:""}]} />
        
        <Heading size="2xl">Editar Pelicula</Heading>

        <MovieForm
          onSubmit={(data) => submitAsync(data, false)}
          initialValues={initialValues}
          isSubmitting={loading}
          error={error}
        />
      </Flex>
    </PageContent>
  );
};

export default MovieUpdatePage;
