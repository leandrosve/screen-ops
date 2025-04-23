import SafeImage from "@/components/common/SafeImage";
import Movie, { MovieStatus } from "@/model/movies/Movie";
import { CmsRoutes } from "@/router/routes";
import {
  Card,
  Icon,
  Menu,
  IconButton,
  Badge,
  Skeleton,
  Box,
  Text,
  HStack,
  Flex,
} from "@chakra-ui/react";
import { FaEllipsis, FaPen, FaRegRectangleList } from "react-icons/fa6";
import { Link } from "react-router-dom";

const getStatus = (status: MovieStatus) => {
  const statusMap = {
    [MovieStatus.DRAFT]: { name: "EN BORRADOR", color: "grey" },
    [MovieStatus.PUBLISHED]: { name: "PUBLICADA", color: "green" },
    [MovieStatus.HIDDEN]: { name: "OCULTA", color: "red" },
  };

  return statusMap[status];
};

const MovieCardItem = ({ movie }: { movie: Movie }) => {
  const status = getStatus(movie.status);
  return (
    <Card.Root>
      <Link
        to={CmsRoutes.MOVIE_DETAIL.replace(":id", movie.id)}
        style={{ width: "100%", overflow: "hidden" }}
      >
        <Box position="relative" zIndex={1}>
          {!!movie.posterUrl &&<SafeImage
            height="200px"
            width="full"
            objectFit="cover"
            position="absolute"
            top={0}
            filter="blur(20px)"
            left={0}
            zIndex={-1}
            src={movie.posterUrl}
          />}
          <SafeImage
            height="200px"
            width="full"
            objectFit="contain"
            placeholder='/placeholder-small.jpg'
            src={movie.posterUrl}
            zIndex={2}
            transition="all 0.3s ease"
            _hover={{
              transform: "scale(1.1)",
            }}
          />

          <Badge
            colorPalette={status.color}
            position="absolute"
            top="1em"
            right="1em"
          >
            {status.name}
          </Badge>
        </Box>
      </Link>
      <Card.Body gap="1" alignItems="start">
        <Card.Title mt="2" lineClamp="2">
          {movie.localizedTitle}
        </Card.Title>
        <Card.Description>{movie.originalTitle}</Card.Description>

        <Flex wrap="wrap" gap={1} mt={1}>
          {movie.genres.map((genre) => (
            <Badge key={genre.id} variant="outline" colorScheme="blue">
              {genre.name}
            </Badge>
          ))}
        </Flex>
      </Card.Body>
      <Card.Footer justifyContent="space-between">
        <HStack gap={2}>
          <Badge colorScheme="purple">{movie.originalReleaseYear}</Badge>
          <Badge colorScheme="teal">{movie.duration} min</Badge>
        </HStack>

        {renderMenu(movie.id)}
      </Card.Footer>
    </Card.Root>
  );
};

const MovieCardItemSkeleton = () => {
  return <Skeleton height="400px" />;
};

const renderMenu = (movieId: string) => (
  <Menu.Root>
    <Menu.Trigger asChild>
      <IconButton variant="outline">
        <FaEllipsis />
      </IconButton>
    </Menu.Trigger>
    <Menu.Positioner>
      <Menu.Content>
        <Link to={CmsRoutes.MOVIE_UPDATE.replace(":id", movieId)}>
          <Menu.Item value="Editar" justifyContent="space-between" gap={5}>
            Editar <Icon as={FaPen} />
          </Menu.Item>
        </Link>
        <Link to={CmsRoutes.MOVIE_DETAIL.replace(":id", movieId)}>
          <Menu.Item value="Ver detalle" justifyContent="space-between" gap={5}>
            Ver detalle <Icon as={FaRegRectangleList} />
          </Menu.Item>
        </Link>
      </Menu.Content>
    </Menu.Positioner>
  </Menu.Root>
);

export default {
  Card: MovieCardItem,
  Skeleton: MovieCardItemSkeleton,
};
