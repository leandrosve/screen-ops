import Alert from "@/components/common/Alert";
import PageLoader from "@/components/common/PageLoader";
import useEntityDetail from "@/hooks/useEntityDetail";
import PageContent from "@/layout/PageContent";
import { Layout, LayoutElementType } from "@/model/layout/Layout";
import { CmsRoutes } from "@/router/routes";
import LayoutService from "@/services/api/LayoutService";
import * as LayoutUtils from "@/components/features/layouts/LayoutUtils";
import {
  VStack,
  Flex,
  Heading,
  Button,
  Icon,
  Badge,
  HStack,
} from "@chakra-ui/react";
import { useMemo } from "react";
import { FaPaintBrush } from "react-icons/fa";
import { FaEyeSlash, FaTrash } from "react-icons/fa6";
import { TbDimensions } from "react-icons/tb";
import { Link } from "react-router-dom";
import LayoutVisualizer from "@/components/features/layouts/LayoutVisualizer";

const LayoutDetailPage = () => {
  const {
    entity: layout,
    loading,
    error,
  } = useEntityDetail<Layout>({
    fetchFunction: (id: string) => LayoutService.getDetail(id),
  });

  const total = useMemo(() => {
    if (!layout) return 0;
    return (
      layout.standardSeats +
      layout.accesibleSeats +
      layout.disabledSeats +
      layout.vipSeats
    );
  }, [layout]);

  if (loading) return <PageLoader />;

  if (error || !layout)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado la película"}
      />
    );

  return (
    <PageContent>
      <VStack gap={5} align="start" width="100%">
        <Flex justifyContent="space-between" alignSelf="stretch">
          <Heading size="2xl">{layout.name}</Heading>
          <Flex gap={3} wrap="wrap">
           
            <Link to={CmsRoutes.LAYOUT_UPDATE.replace(":id", layout.id)}>
              <Button as="span" colorPalette="brand" fontWeight="bold">
                <Icon as={FaPaintBrush} boxSize="1em" /> Editar
              </Button>
            </Link>
          </Flex>
        </Flex>
        <HStack gap={2}>
          <Badge
            size="lg"
            border="1px solid border"
            padding={3}
            colorPalette="orange"
            variant="subtle"
          >
            <Icon as={TbDimensions} transform="scaleX(-1)" /> Dimensiones:{" "}
            {layout.rows} filas x {layout.columns} columnas
          </Badge>
          <Badge
            padding={3}
            size="lg"
            variant="outline"
            border="1px solid border"
          >
            Cantidad total de butacas: {total}
          </Badge>
        </HStack>
        <HStack gap={2}>
          <Badge padding={3} size="lg">
            {LayoutUtils.getIcon(LayoutElementType.STANDARD)}
            Estandar: {layout.standardSeats}
          </Badge>
          <Badge padding={3} size="lg">
            {LayoutUtils.getIcon(LayoutElementType.ACCESSIBLE)}
            Adaptados: {layout.accesibleSeats}
          </Badge>
          <Badge padding={3} size="lg">
            {LayoutUtils.getIcon(LayoutElementType.VIP, "1.5em")}
            Vip: {layout.vipSeats}
          </Badge>
          <Badge padding={3} size="lg">
            {LayoutUtils.getIcon(LayoutElementType.DISABLED, "1.25em")}
            Deshabilitados: {layout.disabledSeats}
          </Badge>
        </HStack>
      </VStack>
      <Heading >Distribución de butacas</Heading>
      <LayoutVisualizer layout={layout}/>
    </PageContent>
  );
};

export default LayoutDetailPage;
