import Alert from "@/components/common/Alert";
import {
  Box,
  Button,
  Flex,
  Grid,
  Group,
  Heading,
  Skeleton,
  Text,
} from "@chakra-ui/react";
import useFetchAPI from "@/hooks/useFetchAPI";
import RoomService from "@/services/api/RoomService";
import RoomCardItem from "@/components/features/rooms/RoomCardItem";
import PageContent from "@/layout/PageContent";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { roomBreadcrumbs } from "@/router/breadcrumbs";
import { RoomSummary } from "@/model/room/Room";
import { useState } from "react";

const RoomsPage = () => {
  return (
    <PageContent>
      <Breadcrumb items={roomBreadcrumbs.list} />
      <RoomsPageContent />
    </PageContent>
  );
};
const RoomsPageContent = () => {
  const {
    entity: rooms,
    loading,
    error,
  } = useFetchAPI({
    fetchFunction: () => RoomService.getRooms(),
    initialFetch: true,
  });
  const [groupByCinema, setGroupByCinema] = useState(false);
  if (loading) {
    return (
      <Grid templateColumns="repeat(auto-fit, minmax(400px, 1fr))" gridGap={5}>
        {[1, 2, 3, 4, 5, 6, 7, 8, 9].map((i) => (
          <Skeleton key={i} height="6em" />
        ))}
      </Grid>
    );
  }

  if (error) {
    return <Alert status="error" description={error} />;
  }
  if (!rooms) {
    return (
      <Alert status="warning" description="No se pudieron cargar las salas" />
    );
  }

  if (!rooms.length)
    return <Text color="text.subtle">Aún no se han agregado salas</Text>;
  return (
    <>
      <Flex justifyContent="space-between">
        <Heading>Salas de cine</Heading>

        <Group attached>
          <Button
            onClick={() => setGroupByCinema(false)}
            variant={!groupByCinema ? "solid" : "outline"}
          >
            Todas las salas
          </Button>
          <Button
            onClick={() => setGroupByCinema(true)}
            variant={groupByCinema ? "solid" : "outline"}
          >
            Agrupar por cine
          </Button>
        </Group>
      </Flex>
      {groupByCinema ? (
        <RoomsByCinemaList rooms={rooms} />
      ) : (
        <SimpleRoomList rooms={rooms} />
      )}
    </>
  );
};

const SimpleRoomList = ({ rooms }: { rooms: RoomSummary[] }) => {
  return (
    <Grid templateColumns="repeat(auto-fit, minmax(400px, 5fr))" gridGap={5}>
      {rooms?.map((r) => (
        <RoomCardItem.Card room={r} key={r.id} />
      ))}
    </Grid>
  );
};

const RoomsByCinemaList = ({ rooms }: { rooms: RoomSummary[] }) => {
  const roomsByCinema = rooms?.reduce<Record<string, RoomSummary[]>>(
    (acc, room) => {
      const cinemaName = room.cinema.name;
      if (!acc[cinemaName]) acc[cinemaName] = [];
      acc[cinemaName].push(room);
      return acc;
    },
    {}
  );
  return Object.entries(roomsByCinema).map(([cinemaName, rooms]) => (
    <Box key={cinemaName}>
      <Heading my={2} size="sm">
        {cinemaName}
      </Heading>
      <Grid templateColumns="repeat(auto-fit, minmax(400px, 5fr))" gap={5}>
        {rooms.map((r) => (
          <RoomCardItem.Card room={r} key={r.id} />
        ))}
      </Grid>
    </Box>
  ));
};

export default RoomsPage;
