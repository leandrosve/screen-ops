import Alert from "@/components/common/Alert";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import EntityStatusBadge from "@/components/common/EntityStatusBadge";
import PageLoader from "@/components/common/PageLoader";
import LayoutVisualizer from "@/components/features/layouts/LayoutVisualizer";
import { toaster } from "@/components/ui/toaster";
import useEntityDetail from "@/hooks/useEntityDetail";
import PageContent from "@/layout/PageContent";
import { EntityStatus } from "@/model/common/EntityStatus";
import { Room } from "@/model/room/Room";
import { roomBreadcrumbs } from "@/router/breadcrumbs";
import { CmsRoutes } from "@/router/routes";
import RoomService from "@/services/api/RoomService";
import { RoomErrors } from "@/validation/api-errors/RoomErrors";
import {
  Flex,
  Heading,
  VStack,
  Button,
  Icon,
  Text,
  Badge,
  Separator,
} from "@chakra-ui/react";
import { useCallback, useState } from "react";
import { BiWorld } from "react-icons/bi";
import { FaPaintBrush } from "react-icons/fa";
import { FaEyeSlash } from "react-icons/fa6";
import { RiBuilding3Line } from "react-icons/ri";
import { Link } from "react-router-dom";

const RoomDetailPage = () => {
  const {
    entity: room,
    loading,
    error,
    setEntity,
  } = useEntityDetail<Room>({
    fetchFunction: (id: string) => RoomService.getDetail(id),
    notFoundError: "No se ha encontrado la sala",
  });

  const { confirm } = useConfirmDialog();

  const [updateStatus, setUpdateStatus] = useState({
    loading: false,
  });

  const handleStatusChange = useCallback(
    async (status: EntityStatus) => {
      if (!room) return;
      const friendly =
        status == EntityStatus.PUBLISHED ? "Publicar" : "Ocultar";
      const friendlyPast =
        status == EntityStatus.PUBLISHED ? "publicado" : "ocultado";

      const confirmed = await confirm({
        title: `${friendly} Cine`,
        message: `¿Estas seguro que deseas ${friendly.toLowerCase()} la sala?`,
        confirmText: `Sí, ${friendly.toLowerCase()}`,
        cancelText: "No, cancelar",
      });
      if (!confirmed) return;

      setUpdateStatus({ loading: true });
      const res = await RoomService.update(room.id, { status });

      if (res?.hasError) {
        toaster.error({
          description:
            RoomErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente.",
          duration: 3000,
        });
        setUpdateStatus({
          loading: false,
        });
        return;
      }
      if (res.data) {
        setEntity(res.data);
        toaster.success({
          description: `Se ha ${friendlyPast} la sala.`,
          duration: 3000,
        });
        setUpdateStatus({
          loading: false,
        });
      }
    },
    [room]
  );

  if (loading) return <PageLoader />;

  if (error || !room)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado el cine"}
      />
    );
  return (
    <PageContent>
      <Breadcrumb items={roomBreadcrumbs.detail(room.id, room.name)} />

      <VStack gap={5} align="start" width="100%">
        <Flex justifyContent="space-between" alignSelf="stretch">
          <VStack align='start'>
            <Badge fontSize="md" padding={2}>
              <RiBuilding3Line/> {room.cinema.name}
            </Badge>
            <Heading size="2xl">{room.name}</Heading>
          </VStack>
          <Flex gap={3} wrap="wrap">
            <EntityStatusBadge status={room.status} genre="femenine" />
            {[EntityStatus.DRAFT, EntityStatus.HIDDEN].includes(room.status) ? (
              <Button
                colorPalette="green"
                fontWeight="bold"
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

            <Link to={CmsRoutes.ROOM_UPDATE.replace(":id", room.id)}>
              <Button as="span" colorPalette="brand" fontWeight="bold">
                <Icon as={FaPaintBrush} boxSize="1em" /> Editar
              </Button>
            </Link>
          </Flex>
        </Flex>
        <Text color="text.subtle">{room.description}</Text>
      </VStack>
      <Separator width="full" />

      <Heading>Layout</Heading>
      {room.layout ? (
        <LayoutVisualizer layout={room.layout} />
      ) : (
        <Text color="text.subtle">No se ha agregado un layout</Text>
      )}
    </PageContent>
  );
};

export default RoomDetailPage;
