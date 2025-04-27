import Alert from "@/components/common/Alert";
import { Cinema } from "@/model/cinema/Cinema";
import CinemaService from "@/services/api/CinemaService";
import { Button, Flex, Heading, Icon, SegmentGroup } from "@chakra-ui/react";
import { useCallback, useEffect, useMemo, useState } from "react";
import CinemaCardItem from "../../features/cinema/CinemaCardItem";
import { EntityStatus } from "@/model/common/EntityStatus";
import { Link } from "react-router-dom";
import { FaPlus } from "react-icons/fa6";
import { CmsRoutes } from "@/router/routes";
import PageContent from "@/layout/PageContent";

const CinemasPage = () => {
  const [cinemas, setCinemas] = useState<Cinema[]>([]);

  const [status, setStatus] = useState({ loading: true, error: "" });

  const [filter, setFilter] = useState<EntityStatus | null>(
    EntityStatus.PUBLISHED
  );

  const fetchCinemas = useCallback(async () => {
    setStatus({ loading: true, error: "" });
    const res = await CinemaService.getCinemas();
    if (res.hasError) {
      setStatus({
        loading: false,
        error: "Lo sentimos, ha ocurrido un problema. Intentalo más tarde.",
      });
      return;
    }
    if (res.data) {
      setCinemas(res.data);
      setStatus({ loading: false, error: "" });
    }
  }, []);

  const onFilterChange = (value: string | null) => {
    if (value === null || value == "Todos") {
      setFilter(null);
      return;
    }
    if (value == "Publicados") {
      setFilter(EntityStatus.PUBLISHED);
      return;
    }
    if (value == "Borrador") {
      setFilter(EntityStatus.DRAFT);
      return;
    }
  };

  const filtered = useMemo(() => {
    return cinemas
      .filter((c) => (filter !== null ? c.status === filter : true))
      .sort((a, b) => (a.createdAt < b.createdAt ? 1 : 0));
  }, [cinemas, filter]);

  useEffect(() => {
    fetchCinemas();
  }, []);

  return (
    <PageContent
      direction="column"
      gap={3}
      flex={1}
      alignSelf="stretch"
      maxWidth={1000}
    >
      <Heading size="2xl">Cines</Heading>

      {status.error && <Alert description={status.error} status="error" />}

      {status.loading &&
        [1, 2, 3, 4, 5].map((i) => <CinemaCardItem.Skeleton key={i} />)}
      <Flex justifyContent="space-between">
        <SegmentGroup.Root
          defaultValue="Publicados"
          alignSelf="start"
          colorPalette="orange"
          onValueChange={(e) => onFilterChange(e.value)}
        >
          <SegmentGroup.Indicator
            _light={{ background: "brand.400" }}
            background="brand.900"
            boxShadow="none"
            border="1px solid"
            borderColor="border"
          />
          <SegmentGroup.Items items={["Publicados", "Borrador", "Todos"]} />
        </SegmentGroup.Root>

        <Link to={CmsRoutes.CINEMA_CREATE}>
          <Button
            as="span"
            type="submit"
            colorPalette="brand"
            fontWeight="bold"
          >
            <Icon as={FaPlus} boxSize="1em" /> Añadir Cine
          </Button>
        </Link>
      </Flex>

      {!status.loading && filtered.length == 0 && (
        <Heading>No se encontraron resultados</Heading>
      )}
      {filtered.map((c) => (
        <CinemaCardItem.Card cinema={c} />
      ))}
    </PageContent>
  );
};

export default CinemasPage;
