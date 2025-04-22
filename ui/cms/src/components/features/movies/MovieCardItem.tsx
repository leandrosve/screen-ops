import DynamicAvatar from "@/components/common/DynamicAvatar";
import Movie, { MovieStatus } from "@/model/movies/Movie";
import SessionService from "@/services/SessionService";
import {
  Card,
  Image,
  Icon,
  Menu,
  IconButton,
  Badge,
  Skeleton,
} from "@chakra-ui/react";
import { FaEllipsis, FaPen, FaRegRectangleList } from "react-icons/fa6";

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
    <Card.Root maxWidth={400}>
      <Image height="200px" src="https://i.pravatar.cc/400?u=1" />
      <Card.Body gap="1" alignItems="start">
        <Badge colorPalette={status.color}>{status.name}</Badge>
        <Card.Title mt="2">{movie.originalTitle}</Card.Title>
        <Card.Description>{movie.originalTitle}</Card.Description>
      </Card.Body>
      <Card.Footer justifyContent="flex-end">{renderMenu()}</Card.Footer>
    </Card.Root>
  );
};

const MovieCardItemSkeleton = () => {
  return <Skeleton height="400px"/>;
};

const renderMenu = () => (
  <Menu.Root>
    <Menu.Trigger asChild>
      <IconButton variant="outline">
        <FaEllipsis />
      </IconButton>
    </Menu.Trigger>
    <Menu.Positioner>
      <Menu.Content>
        <Menu.Item value="Editar" justifyContent="space-between" gap={5}>
          Editar <Icon as={FaPen} />
        </Menu.Item>
        <Menu.Item value="Ver detalle" justifyContent="space-between" gap={5}>
          Ver detalle <Icon as={FaRegRectangleList} />
        </Menu.Item>
      </Menu.Content>
    </Menu.Positioner>
  </Menu.Root>
);

export default {
  Card: MovieCardItem,
  Skeleton: MovieCardItemSkeleton,
};