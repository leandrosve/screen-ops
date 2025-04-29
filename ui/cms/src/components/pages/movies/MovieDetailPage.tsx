import Alert from "@/components/common/Alert";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import EntityStatusBadge from "@/components/common/EntityStatusBadge";
import PageLoader from "@/components/common/PageLoader";
import SafeImage from "@/components/common/SafeImage";
import YouTubePlayer from "@/components/common/YouTubePlayer";
import { toaster } from "@/components/ui/toaster";
import useMovieDetail from "@/hooks/movies/useMovieDetail";
import { useYoutubeVideoId } from "@/hooks/movies/useYoutubeVideoId";
import PageContent from "@/layout/PageContent";
import { EntityStatus } from "@/model/common/EntityStatus";
import Movie from "@/model/movies/Movie";
import { movieBreadcrumbs } from "@/router/breadcrumbs";
import { CmsRoutes } from "@/router/routes";
import MoviesService from "@/services/api/MoviesService";
import { MovieCreateErrors } from "@/validation/api-errors/MovieErrors";
import {
  Box,
  Flex,
  Heading,
  Text,
  Image,
  Card,
  VStack,
  Separator,
  Grid,
  Tag,
  Badge,
  Button,
  Icon,
} from "@chakra-ui/react";
import { useCallback, useState } from "react";
import { BiWorld } from "react-icons/bi";
import { FaPaintBrush } from "react-icons/fa";
import { FaEyeSlash } from "react-icons/fa6";
import { Link } from "react-router-dom";

const details: {
  label: string;
  getValue: (movie: Movie) => React.ReactNode;
}[] = [
  {
    label: "Título Original",
    getValue: (movie) => movie.originalTitle,
  },
  {
    label: "Director",
    getValue: (movie) => movie.director,
  },
  {
    label: "País",
    getValue: (movie) => movie.country?.name,
  },
  {
    label: "Idioma Original",
    getValue: (movie) => movie.originalLanguage?.name,
  },
  {
    label: "Actores Principales",
    getValue: (movie) => movie.mainActors,
  },
  {
    label: "Año de Estreno",
    getValue: (movie) => movie.originalReleaseYear,
  },
  {
    label: "Duración",
    getValue: (movie) => (movie.duration ? `${movie.duration} min` : null),
  },

  {
    label: "Géneros",
    getValue: (movie) => (
      <Flex wrap="wrap" gap={2}>
        {movie.genres.map((g) => (
          <Tag.Root variant="subtle" key={g.id}>
            <Tag.Label>{g.name}</Tag.Label>{" "}
          </Tag.Root>
        ))}
      </Flex>
    ),
  },
];

const breadcrumbs = [{title: "Películas", path: CmsRoutes.MOVIES}]

const MovieDetailPage = () => {
  const { movie, loading, error, setMovie } = useMovieDetail();

  const [updateStatus, setUpdateStatus] = useState({
    loading: false,
  });

  const videoId = useYoutubeVideoId(movie?.trailerUrl);
  const { confirm } = useConfirmDialog();

  const handleStatusChange = useCallback(
    async (status: EntityStatus) => {
      if (!movie) return;
      const friendly = status == EntityStatus.PUBLISHED ? "Publicar" : "Ocultar";
      const friendlyPast =
        status == EntityStatus.PUBLISHED ? "publicado" : "ocultado";

      const confirmed = await confirm({
        title: `${friendly} Película`,
        message: `¿Estas seguro que deseas ${friendly.toLowerCase()} la película?`,
        confirmText: `Sí, ${friendly.toLowerCase()}`,
        cancelText: "No, cancelar",
      });
      if (!confirmed) return;

      setUpdateStatus({ loading: true });
      const res = await MoviesService.update(movie.id, { status }, false);

      if (res?.hasError) {
        toaster.error({
          description:
            MovieCreateErrors[res.error] ??
            "Algo salió mal! Intentalo nuevamente.",
          duration: 3000,
        });
        setUpdateStatus({
          loading: false,
        });
        return;
      }
      if (res.data) {
        setMovie(res.data);
        toaster.success({
          description: `Se ha ${friendlyPast} la película.`,
          duration: 3000,
        });
        setUpdateStatus({
          loading: false,
        });
      }
    },
    [movie]
  );
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
  return (
    <PageContent>
      <Breadcrumb items={movieBreadcrumbs.detail(movie.id, movie.localizedTitle)} />
      <VStack gap={5} align="start" width="100%" padding={5}>
        <Flex gap={5} width="100%">
          <Image
            width={300}
            height="fit-content"
            objectFit="cover"
            objectPosition="top"
            src={movie.posterUrl}
            fill="border.subtle"
          />
          <VStack gap={5} align="start" flex={1}>
            <Flex justifyContent="space-between" alignSelf="stretch">
              <Heading size="2xl">{movie.localizedTitle}</Heading>
              <Flex gap={3} wrap="wrap">
                <EntityStatusBadge status={movie.status} />
                {[EntityStatus.DRAFT, EntityStatus.HIDDEN].includes(
                  movie.status
                ) ? (
                  <Button
                    colorPalette="green"
                    fontWeight="bold"
                    variant="subtle"
                    loading={updateStatus.loading}
                    onClick={() => handleStatusChange(EntityStatus.PUBLISHED)}
                  >
                    <Icon as={BiWorld} boxSize="1em" /> Publicar
                  </Button>
                ) : (
                  <Button
                    fontWeight="bold"
                    variant="subtle"
                    loading={updateStatus.loading}
                    onClick={() => handleStatusChange(EntityStatus.HIDDEN)}
                  >
                    <Icon as={FaEyeSlash} boxSize="1em" /> Ocultar
                  </Button>
                )}

                <Link to={CmsRoutes.MOVIE_UPDATE.replace(":id", movie.id)}>
                  <Button as="span" colorPalette="brand" fontWeight="bold">
                    <Icon as={FaPaintBrush} boxSize="1em" /> Editar
                  </Button>
                </Link>
              </Flex>
            </Flex>
            <Flex
              direction="row"
              justifyContent="space-between"
              alignSelf="stretch"
            >
              <Box flex={1}>
                <Heading size="md"> Sinopsis:</Heading>
                {movie.description}
              </Box>
            </Flex>
            <Separator />
            <Card.Root
              alignSelf="stretch"
              borderColor="border.subtle"
              bg="bg.subtle"
              flex={1}
              _light={{ boxShadow: "sm" }}
            >
              <Card.Body gap={4} paddingY={4}>
                <Grid
                  templateColumns="repeat(auto-fit, minmax(260px, 5fr))"
                  gridGap={5}
                  alignSelf="stretch"
                >
                  {details.map((detail, index) => (
                    <Box key={index}>
                      <Text as="h3" color="text.subtle">
                        {detail.label}
                      </Text>
                      <Text as="span">{detail.getValue(movie) ?? "-"}</Text>
                    </Box>
                  ))}
                </Grid>
              </Card.Body>
            </Card.Root>
          </VStack>
        </Flex>
        <Separator width="full" />
        <Flex gap={5} alignSelf="stretch" wrap="wrap">
          {videoId && (
            <Box flexShrink={0}>
              <Heading marginBottom={3}>Trailer</Heading>
              <YouTubePlayer url={`https://www.youtube.com/embed/${videoId}`} />
            </Box>
          )}
          <Box alignSelf="stretch" flex={1}>
            <Heading marginBottom={3}>Imágenes</Heading>
            <Flex
              height="315px"
              padding={3}
              bg="bg.subtle"
              overflowX="auto"
              gap={3}
              flex={1}
              alignSelf="stretch"
            >
              {movie.extraImageUrls?.length <= 0 && (
                <Flex
                  alignItems="center"
                  justifyContent="center"
                  color="text.subtle"
                  flex={1}
                >
                  <Text display="block">No se han añadido imágenes</Text>
                </Flex>
              )}
              {movie.extraImageUrls.map((src, index) => (
                <SafeImage
                  key={index}
                  height="auto"
                  objectFit="cover"
                  objectPosition="top"
                  src={src}
                  fill="border.subtle"
                />
              ))}
            </Flex>
          </Box>
        </Flex>
      </VStack>
    </PageContent>
  );
};

export default MovieDetailPage;
