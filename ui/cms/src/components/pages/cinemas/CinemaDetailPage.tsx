import Alert from "@/components/common/Alert";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import EntityStatusBadge from "@/components/common/EntityStatusBadge";
import PageLoader from "@/components/common/PageLoader";
import SafeImage from "@/components/common/SafeImage";
import { toaster } from "@/components/ui/toaster";
import useEntityDetail from "@/hooks/useEntityDetail";
import PageContent from "@/layout/PageContent";
import { Cinema } from "@/model/cinema/Cinema";
import { EntityStatus } from "@/model/common/EntityStatus";
import Movie from "@/model/movies/Movie";
import { CmsRoutes } from "@/router/routes";
import CinemaService from "@/services/api/CinemaService";
import { MovieCreateErrors } from "@/validation/api-errors/MovieErrors";
import {
  Flex,
  Heading,
  VStack,
  Tag,
  Button,
  Icon,
  Text,
  Badge,
} from "@chakra-ui/react";
import { useCallback, useState } from "react";
import { BiWorld } from "react-icons/bi";
import { FaPaintBrush } from "react-icons/fa";
import { FaEyeSlash } from "react-icons/fa6";
import { LuMapPin } from "react-icons/lu";
import { MdPeople } from "react-icons/md";
import { Link } from "react-router-dom";

const CinemaDetailPage = () => {
  const {
    entity: cinema,
    loading,
    error,
    setEntity,
  } = useEntityDetail<Cinema>({
    fetchFunction: (id: string) => CinemaService.getDetail(id),
    notFoundError: "No se ha encontrado el cine",
  });

  const { confirm } = useConfirmDialog();

  const [updateStatus, setUpdateStatus] = useState({
    loading: false,
  });

  const handleStatusChange = useCallback(
    async (status: EntityStatus) => {
      if (!cinema) return;
      const friendly =
        status == EntityStatus.PUBLISHED ? "Publicar" : "Ocultar";
      const friendlyPast =
        status == EntityStatus.PUBLISHED ? "publicado" : "ocultado";

      const confirmed = await confirm({
        title: `${friendly} Cine`,
        message: `¿Estas seguro que deseas ${friendly.toLowerCase()} el cine?`,
        confirmText: `Sí, ${friendly.toLowerCase()}`,
        cancelText: "No, cancelar",
      });
      if (!confirmed) return;

      setUpdateStatus({ loading: true });
      const res = await CinemaService.update(cinema.id, { status });

      if (res?.hasError) {
        toaster.error({
          description:
            MovieCreateErrors[res.error] ??
            "Algo salió mal! Intentalo nuevamente.",
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
          description: `Se ha ${friendlyPast} el cine.`,
          duration: 3000,
        });
        setUpdateStatus({
          loading: false,
        });
      }
    },
    [cinema]
  );

  if (loading) return <PageLoader />;

  if (error || !cinema)
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
      <VStack gap={5} align="start" width="100%">
        <Flex justifyContent="space-between" alignSelf="stretch">
          <Heading size="2xl">{cinema.name}</Heading>
          <Flex gap={3} wrap="wrap">
            <EntityStatusBadge status={cinema.status} genre="masculine" />
            {[EntityStatus.DRAFT, EntityStatus.HIDDEN].includes(
              cinema.status
            ) ? (
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

            <Link to={CmsRoutes.CINEMA_UPDATE.replace(":id", cinema.id)}>
              <Button as="span" colorPalette="brand" fontWeight="bold">
                <Icon as={FaPaintBrush} boxSize="1em" /> Editar
              </Button>
            </Link>
          </Flex>
        </Flex>
        <Flex alignItems="center" wrap="wrap" gap={3}>
          <Badge variant="outline" size="lg" padding={2}>
            <LuMapPin />
            Dirección: {cinema.location}
          </Badge>
          <Badge variant="outline" size="lg" padding={2} fontWeight="bold">
            <MdPeople />
            Capacidad: {cinema.capacity}
          </Badge>
        </Flex>
        <Text color="text.subtle">{cinema.description}</Text>
        {!!cinema.imageUrl && (
          <SafeImage
            height="auto"
            maxWidth={600}
            objectFit="cover"
            objectPosition="top"
            borderRadius="md"
            src={cinema.imageUrl}
            fill="border.subtle"
          />
        )}
      </VStack>
    </PageContent>
  );
};

export default CinemaDetailPage;
