import EntityStatusBadge from "@/components/common/EntityStatusBadge";
import { RoomSummary, RoomSummaryShort } from "@/model/room/Room";
import { CmsRoutes } from "@/router/routes";
import {
  Badge,
  Card,
  Flex,
  Icon,
  IconButton,
  Menu,
  Skeleton,
  Text,
  VStack,
} from "@chakra-ui/react";
import { FaEllipsis, FaPen, FaRegRectangleList } from "react-icons/fa6";
import { RiBuilding3Line } from "react-icons/ri";

import { Link } from "react-router-dom";

interface Props {
  room: RoomSummaryShort;
}

const RoomShortCardItem = ({ room }: Props) => {
  return (
    <Card.Root>
      <Link to={CmsRoutes.ROOM_DETAIL.replace(":id", room.id)}>
        <Card.Body
          padding={3}
          gap={0}
          minHeight="4em"
          _hover={{ bg: "bg.muted" }}
          transition="background 200ms"
        >
          <Flex justifyContent="space-between" gap={4}>
            <VStack gap={0} align="start">
              <Card.Title fontSize="md" lineClamp={1}>
                {room.name}
              </Card.Title>
              <Text color="text.subtle" lineClamp={1}>
                {room.description}
              </Text>
            </VStack>
            <Flex gap={4} onClick={(e) => e.preventDefault()}>
              <EntityStatusBadge status={room.status} minHeight="none" />
              {renderMenu(room.id)}
            </Flex>
          </Flex>
        </Card.Body>
      </Link>
    </Card.Root>
  );
};

const RoomCardItem = ({ room }: { room: RoomSummary }) => {
  return (
    <Card.Root>
      <Link to={CmsRoutes.ROOM_DETAIL.replace(":id", room.id)}>
        <Card.Body
          padding={3}
          gap={0}
          minHeight="4em"
          _hover={{ bg: "bg.muted" }}
          transition="background 200ms"
        >
          <Flex justifyContent="space-between" gap={4}>
            <VStack gap={0} align="start">
              <Badge variant="outline"><RiBuilding3Line/>{room.cinema.name}</Badge>
              <Card.Title fontSize="md" ml={1} lineClamp={1}>
                {room.name}
              </Card.Title>
              <Text color="text.subtle" fontSize='sm' ml={1} lineClamp={1}>
                {room.description}
              </Text>
            </VStack>
            <Flex gap={4} onClick={(e) => e.preventDefault()}>
              <EntityStatusBadge status={room.status} minHeight="none" />
              {renderMenu(room.id)}
            </Flex>
          </Flex>
        </Card.Body>
      </Link>
    </Card.Root>
  );
};
const renderMenu = (roomId: string) => (
  <Menu.Root>
    <Menu.Trigger asChild>
      <IconButton variant="outline">
        <FaEllipsis />
      </IconButton>
    </Menu.Trigger>
    <Menu.Positioner>
      <Menu.Content>
        <Link to={CmsRoutes.ROOM_DETAIL.replace(":id", roomId)}>
          <Menu.Item value="Ver detalle" justifyContent="space-between" gap={5}>
            Ver detalle <Icon as={FaRegRectangleList} />
          </Menu.Item>
        </Link>
        <Link to={CmsRoutes.ROOM_UPDATE.replace(":id", roomId)}>
          <Menu.Item value="Editar" justifyContent="space-between" gap={5}>
            Editar <Icon as={FaPen} />
          </Menu.Item>
        </Link>
      </Menu.Content>
    </Menu.Positioner>
  </Menu.Root>
);
const roomCardItemSkeleton = () => {
  return <Skeleton height="250px" borderRadius="lg" />;
};

export default {
  ShortCard: RoomShortCardItem,
  Card: RoomCardItem,
  Skeleton: roomCardItemSkeleton,
};
