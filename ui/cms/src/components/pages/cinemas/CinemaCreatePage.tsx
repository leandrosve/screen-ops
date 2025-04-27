import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import CinemaForm from "@/components/features/cinema/CinemaForm";
import { toaster } from "@/components/ui/toaster";
import PageContent from "@/layout/PageContent";
import { CinemaCreateDto } from "@/model/cinema/Cinema";
import { CmsRoutes } from "@/router/routes";
import CinemaService from "@/services/api/CinemaService";
import { CinemaErrors } from "@/validation/api-errors/CinemaErrors";

import { Flex, Heading } from "@chakra-ui/react";
import { useCallback, useState } from "react";
import { useNavigate } from "react-router-dom";

const CinemaCreatePage = () => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const { confirm } = useConfirmDialog();

  const submitAsync = useCallback(
    async (data: CinemaCreateDto) => {
      setLoading(true);
      setError("");
      const res = await CinemaService.create(data);
      setLoading(false);
      if (res.hasError) {
        setError(
          CinemaErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
        );
      }

      if (res.data) {
        toaster.create({
          title: "Se ha añadido el cine",
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
      <Heading size="2xl">Añadir Cine</Heading>
      <CinemaForm
        onSubmit={(data) => submitAsync(data)}
        isSubmitting={loading}
        error={error}
      />
    </PageContent>
  );
};

export default CinemaCreatePage;
