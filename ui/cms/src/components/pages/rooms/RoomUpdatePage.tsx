import Alert from "@/components/common/Alert";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import PageLoader from "@/components/common/PageLoader";
import RoomForm from "@/components/features/rooms/RoomForm";
import { toaster } from "@/components/ui/toaster";
import useEntityDetail from "@/hooks/useEntityDetail";
import { Room, RoomCreateDto } from "@/model/room/Room";
import { roomBreadcrumbs } from "@/router/breadcrumbs";
import { CmsRoutes } from "@/router/routes";
import RoomService from "@/services/api/RoomService";
import DtoUtils from "@/utils/DtoUtils";
import { RoomErrors } from "@/validation/api-errors/RoomErrors";

import { useCallback, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

const RoomUpdatePage = () => {
  const {
    entity: room,
    loading,
    error,
  } = useEntityDetail<Room>({
    fetchFunction: (id: string) => RoomService.getDetail(id),
  });

  if (loading) return <PageLoader />;

  if (error || !room)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado la sala"}
      />
    );

  if (!loading && room) {
    return <RoomUpdateForm room={room} />;
  }
  return <></>;
};

const mapToCreateDto = (room: Room): RoomCreateDto => {
  return {
    name: room.name,
    description: room.description,
    cinemaId: room.cinema?.id,
    layoutId: room.layout?.id,
  };
};

const RoomUpdateForm = ({ room }: { room: Room }) => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const { confirm } = useConfirmDialog();

  const initialValues = useMemo(() => {
    return mapToCreateDto(room);
  }, [room]);

  const submitAsync = useCallback(
    async (data: RoomCreateDto) => {
      const modifiedFields = DtoUtils.getModifiedFields(initialValues, data);

      console.log({ modifiedFields });

      setLoading(true);
      setError("");

      const res = await RoomService.update(room.id, modifiedFields);
      setLoading(false);

      if (res.hasError) {
        setError(
          RoomErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
        );
      }

      if (res.data) {
        toaster.create({
          title: "Se ha actualizado la sala",
          type: "success",
          duration: 1500,
        });

        navigate(CmsRoutes.ROOM_DETAIL.replace(":id", res.data?.id ?? ""));
      }
    },
    [navigate, confirm]
  );

  return (
    <RoomForm
      onSubmit={(data) => submitAsync(data)}
      initialValues={initialValues}
      isSubmitting={loading}
      title="Editar Sala"
      breadcrumbs={<Breadcrumb items={roomBreadcrumbs.create} />}
      lockCinema={true}
      error={error}
    />
  );
};

export default RoomUpdatePage;
