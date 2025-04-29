import { Breadcrumb } from "@/components/common/Breadcrumb";
import RoomForm from "@/components/features/rooms/RoomForm";
import { toaster } from "@/components/ui/toaster";
import PageContent from "@/layout/PageContent";
import { RoomCreateDto } from "@/model/room/Room";
import { roomBreadcrumbs } from "@/router/breadcrumbs";
import { CmsRoutes } from "@/router/routes";
import RoomService from "@/services/api/RoomService";
import { RoomErrors } from "@/validation/api-errors/RoomErrors";
import { Heading } from "@chakra-ui/react";
import React, { useCallback, useMemo, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

const RoomCreatePage = () => {
  const [searchParams] = useSearchParams();

  const cinemaId = useMemo(
    () => searchParams.get("cinema") ?? undefined,
    [searchParams]
  );

  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const submitAsync = useCallback(async (data: RoomCreateDto) => {
    setLoading(true);
    const res = await RoomService.create(data);
    setLoading(false);
    if (res.hasError) {
      setError(
        RoomErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
      );
    }

    if (res.data) {
      toaster.create({
        title: "Se ha creado el layout",
        type: "success",
        duration: 1500,
      });

      navigate(CmsRoutes.ROOM_DETAIL.replace(":id", res.data?.id ?? ""));
    }
  }, []);
  return (
    <RoomForm
      onSubmit={submitAsync}
      initialValues={{ cinemaId }}
      error={error}
      isSubmitting={loading}
      title="Añadir Sala"
      breadcrumbs={<Breadcrumb items={roomBreadcrumbs.create} />}
    />
  );
};

export default RoomCreatePage;
