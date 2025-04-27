import { LayoutElementType, LayoutSummary } from "@/model/layout/Layout";
import {
  Badge,
  Card,
  Flex,
  HStack,
  Icon,
  IconButton,
  Menu,
  Skeleton,
  Tag,
} from "@chakra-ui/react";
import { useMemo } from "react";
import { FaEllipsis, FaPen, FaRegRectangleList } from "react-icons/fa6";
import { Link } from "react-router-dom";
import * as LayoutUtils from "./LayoutUtils";
import { CmsRoutes } from "@/router/routes";
import { MdGridOn } from "react-icons/md";
import { TbDimensions } from "react-icons/tb";

const LayoutCardItem = ({ layout }: { layout: LayoutSummary }) => {
  const total = useMemo(
    () =>
      layout.standardSeats +
      layout.accesibleSeats +
      layout.disabledSeats +
      layout.vipSeats,
    []
  );
  return (
    <Card.Root>
      <Card.Body>
        <Flex justifyContent="space-between">
          <Link to={CmsRoutes.LAYOUT_DETAIL.replace(":id", layout.id)}>
            <Card.Title display="inline-flex" alignItems="center" gap={3}>
              <Tag.Root
                padding={2}
                borderRadius={50}
                width="1.75em"
                as="span"
                height="1.75em"
                display="inline-flex"
                alignItems="center"
                justifyContent="center"
                border="none"
                boxShadow="none"
                aria-hidden
              >
                <Tag.Label>
                  <Icon as={MdGridOn} boxSize={"1.25em"} />
                </Tag.Label>
              </Tag.Root>
              {layout.name}
            </Card.Title>
          </Link>{" "}
          {renderMenu(layout.id)}
        </Flex>
        <HStack gap={2}>
          <Badge size="lg" border="1px solid border" colorPalette='orange' variant='subtle'>
            <Icon as={TbDimensions} transform='scaleX(-1)'/> {layout.rows} x {layout.columns}
          </Badge>
          <Badge size="lg"  variant="outline" border="1px solid border">
            Total de butacas: {total}
          </Badge>
          <Badge size="lg">
            {LayoutUtils.getIcon(LayoutElementType.STANDARD)}
            {layout.standardSeats}
          </Badge>
          <Badge size="lg">
            {LayoutUtils.getIcon(LayoutElementType.ACCESSIBLE)}
            {layout.accesibleSeats}
          </Badge>
          <Badge size="lg">
            {LayoutUtils.getIcon(LayoutElementType.VIP)}
            {layout.vipSeats}
          </Badge>
        </HStack>
      </Card.Body>
    </Card.Root>
  );
};

const renderMenu = (id: string) => (
  <Menu.Root>
    <Menu.Trigger asChild>
      <IconButton variant="outline">
        <FaEllipsis />
      </IconButton>
    </Menu.Trigger>
    <Menu.Positioner>
      <Menu.Content>
        <Link to={CmsRoutes.LAYOUT_UPDATE.replace(":id", id)}>
          <Menu.Item value="Editar" justifyContent="space-between" gap={5}>
            Editar <Icon as={FaPen} />
          </Menu.Item>
        </Link>
        <Link to={CmsRoutes.LAYOUT_DETAIL.replace(":id", id)}>
          <Menu.Item value="Ver detalle" justifyContent="space-between" gap={5}>
            Ver detalle <Icon as={FaRegRectangleList} />
          </Menu.Item>
        </Link>
      </Menu.Content>
    </Menu.Positioner>
  </Menu.Root>
);
const LayoutCardItemSkeleton = () => {
  return <Skeleton height="10em" width="100%" />;
};

export default {
  Skeleton: LayoutCardItemSkeleton,
  Card: LayoutCardItem,
};
