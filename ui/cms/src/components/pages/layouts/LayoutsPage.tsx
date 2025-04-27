import Alert from "@/components/common/Alert";
import {
  Badge,
  Button,
  Card,
  CardTitle,
  Flex,
  Heading,
  HStack,
  Icon,
  IconButton,
  Input,
  InputGroup,
  Menu,
  Skeleton,
} from "@chakra-ui/react";
import { useCallback, useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { FaEllipsis, FaPen, FaPlus, FaRegRectangleList } from "react-icons/fa6";
import { CmsRoutes } from "@/router/routes";
import PageContent from "@/layout/PageContent";
import LayoutService from "@/services/api/LayoutService";
import { LayoutSummary } from "@/model/layout/Layout";
import LayoutCardItem from "@/components/features/layouts/LayoutCardItem";
import { FaSearch } from "react-icons/fa";

const LayoutPage = () => {
  const [layouts, setLayouts] = useState<LayoutSummary[]>([]);

  const [status, setStatus] = useState({ loading: true, error: "" });

  const [filter, setFilter] = useState<string>("");

  const fetchLayouts = useCallback(async () => {
    setStatus({ loading: true, error: "" });
    const res = await LayoutService.getLayouts();
    if (res.hasError) {
      setStatus({
        loading: false,
        error: "Lo sentimos, ha ocurrido un problema. Intentalo más tarde.",
      });
      return;
    }
    if (res.data) {
      setLayouts(res.data);
      setStatus({ loading: false, error: "" });
    }
  }, []);

  const filtered = useMemo(() => {
    let trimmed = filter.trim().toLowerCase();
    if (!trimmed) return layouts;
    return layouts.filter((c) => c.name.toLowerCase().includes(trimmed));
  }, [layouts, filter]);

  useEffect(() => {
    fetchLayouts();
  }, []);

  return (
    <PageContent
      direction="column"
      gap={3}
      flex={1}
      alignSelf="stretch"
      maxWidth={1000}
    >
      <Heading size="2xl">Layouts</Heading>

      {status.error && <Alert description={status.error} status="error" />}

      <Flex justifyContent="space-between">
        <InputGroup  endElement={<FaSearch />} width={400}>
          <Input
            id="search-term"
            placeholder="Buscar por nombre"
            onChange={(e) => setFilter(e.target.value)}
          />
        </InputGroup>
        <Link to={CmsRoutes.LAYOUT_CREATE}>
          <Button
            as="span"
            type="submit"
            colorPalette="brand"
            fontWeight="bold"
          >
            <Icon as={FaPlus} boxSize="1em" /> Añadir Layout
          </Button>
        </Link>
      </Flex>
      {status.loading &&
        [1, 2, 3, 4, 5].map((i) => <LayoutCardItem.Skeleton key={i} />)}
      {!status.loading && filtered.length == 0 && (
        <Heading>No se encontraron resultados</Heading>
      )}
      {filtered.map((c) => (
        <LayoutCardItem.Card layout={c} key={c.id} />
      ))}
    </PageContent>
  );
};

export default LayoutPage;
