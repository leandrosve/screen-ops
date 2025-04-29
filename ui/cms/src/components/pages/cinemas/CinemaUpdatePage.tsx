import Alert from "@/components/common/Alert";
import { Breadcrumb } from "@/components/common/Breadcrumb";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import PageLoader from "@/components/common/PageLoader";
import CinemaForm from "@/components/features/cinema/CinemaForm";
import { toaster } from "@/components/ui/toaster";
import useEntityDetail from "@/hooks/useEntityDetail";
import PageContent from "@/layout/PageContent";
import { Cinema, CinemaCreateDto } from "@/model/cinema/Cinema";
import { cinemaBreadcrumbs } from "@/router/breadcrumbs";
import { CmsRoutes } from "@/router/routes";
import CinemaService from "@/services/api/CinemaService";
import DtoUtils from "@/utils/DtoUtils";
import { CinemaErrors } from "@/validation/api-errors/CinemaErrors";

import { Flex, Heading } from "@chakra-ui/react";
import { useCallback, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

const CinemaUpdatePage = () => {
  const {
    entity: cinema,
    loading,
    error,
  } = useEntityDetail<Cinema>({
    fetchFunction: (id: string) => CinemaService.getDetail(id),
  });

  if (loading) return <PageLoader />;

  if (error || !cinema)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado la película"}
      />
    );

  if (!loading && cinema) {
    return <CinemaUpdateForm cinema={cinema} />;
  }
  return <></>;
};

const mapMovieToMovieCreateDto = (cinema: Cinema): CinemaCreateDto => {
  return {
    name: cinema.name,
    location: cinema.location,
    description: cinema.description,
    imageUrl: cinema.imageUrl,
    capacity: cinema.capacity,
  };
};

const CinemaUpdateForm = ({ cinema }: { cinema: Cinema }) => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const { confirm } = useConfirmDialog();

  const initialValues = useMemo(() => {
    return mapMovieToMovieCreateDto(cinema);
  }, [cinema]);

  const submitAsync = useCallback(
    async (data: CinemaCreateDto) => {
      const modifiedFields = DtoUtils.getModifiedFields(initialValues, data);

      console.log({ modifiedFields });

      setLoading(true);
      setError("");

      const res = await CinemaService.update(cinema.id, modifiedFields);
      setLoading(false);

      if (res.hasError) {
        setError(
          CinemaErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
        );
      }

      if (res.data) {
        toaster.create({
          title: "Se ha actualizado el cine",
          type: "success",
          duration: 1500,
        });

        navigate(CmsRoutes.CINEMA_DETAIL.replace(":id", res.data?.id ?? ""));
      }
    },
    [navigate, confirm]
  );

  return (
    <PageContent direction="column" gap={3} width="fit-content">
      <Breadcrumb items={cinemaBreadcrumbs.update(cinema.id, cinema.name)}/>
      <Heading size="2xl">Editar Cine</Heading>

      <CinemaForm
        onSubmit={(data) => submitAsync(data)}
        initialValues={initialValues}
        isSubmitting={loading}
        error={error}
      />
    </PageContent>
  );
};

export default CinemaUpdatePage;
