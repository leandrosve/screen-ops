import EntityStatusBadge from "@/components/common/EntityStatusBadge";
import SafeImage from "@/components/common/SafeImage";
import { Cinema } from "@/model/cinema/Cinema";
import { CmsRoutes } from "@/router/routes";
import {
  Badge,
  Box,
  Card,
  Flex,
  HStack,
  Icon,
  IconButton,
  Menu,
  Skeleton,
  Text,
} from "@chakra-ui/react";
import { FaEllipsis, FaPen, FaRegRectangleList } from "react-icons/fa6";
import { LuMapPin } from "react-icons/lu";
import { MdPeople } from "react-icons/md";
import { Link } from "react-router-dom";

interface Props {
  cinema: Cinema;
}

const CinemaCardItem = ({ cinema }: Props) => {
  return (
    <Card.Root>
      <Card.Body gap={2}>
        <Flex justifyContent="space-between" gap={4}>
          <Link to={CmsRoutes.CINEMA_DETAIL.replace(":id", cinema.id)}>
            <Flex gap={3} alignItems="center">
              <SafeImage
                src={cinema.imageUrl}
                alt={cinema.name}
                aspectRatio={"1/1"}
                objectFit="fill"
                maxWidth="3em"
                borderRadius="full"
                border="2px solid"
                borderColor="borders.subtle"
              />
              <Card.Title>{cinema.name}</Card.Title>
            </Flex>
          </Link>

          <Flex gap={4} onClick={(e) => e.preventDefault()}>
            <EntityStatusBadge
              status={cinema.status}
              genre="masculine"
              minHeight="none"
            />
            {renderMenu(cinema.id)}
          </Flex>
        </Flex>
        <Flex gap={4}>
          <Flex direction="column" gap={2} alignItems="start">
            <Text color="text.subtle" lineClamp={3}>
              {cinema.description}
            </Text>
            <HStack>
              <Badge variant="outline" size="lg" padding={2}>
                <LuMapPin />
                Dirección: {cinema.location}
              </Badge>
              <Badge variant="outline" size="lg" padding={2} fontWeight="bold">
                <MdPeople />
                Capacidad: {cinema.capacity}
              </Badge>
            </HStack>
          </Flex>
        </Flex>
      </Card.Body>
    </Card.Root>
  );
};
const renderMenu = (cinemaId: string) => (
  <Menu.Root>
    <Menu.Trigger asChild>
      <IconButton variant="outline">
        <FaEllipsis />
      </IconButton>
    </Menu.Trigger>
    <Menu.Positioner>
      <Menu.Content>
        <Link to={CmsRoutes.CINEMA_DETAIL.replace(":id", cinemaId)}>
          <Menu.Item value="Ver detalle" justifyContent="space-between" gap={5}>
            Ver detalle <Icon as={FaRegRectangleList} />
          </Menu.Item>
        </Link>
        <Link to={CmsRoutes.CINEMA_UPDATE.replace(":id", cinemaId)}>
          <Menu.Item value="Editar" justifyContent="space-between" gap={5}>
            Editar <Icon as={FaPen} />
          </Menu.Item>
        </Link>
      </Menu.Content>
    </Menu.Positioner>
  </Menu.Root>
);
const CinemaCardItemSkeleton = () => {
  return <Skeleton height="250px" borderRadius="lg" />;
};

export default {
  Card: CinemaCardItem,
  Skeleton: CinemaCardItemSkeleton,
};
