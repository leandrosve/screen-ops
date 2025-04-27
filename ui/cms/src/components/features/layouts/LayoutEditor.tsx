import { LayoutElementType } from "@/model/layout/Layout";
import {
  Box,
  Button,
  Card,
  CloseButton,
  Field,
  Flex,
  Grid,
  Group,
  Heading,
  HStack,
  Icon,
  IconButton, Menu,
  NumberInput,
  NumberInputRootProps,
  Portal,
  Text
} from "@chakra-ui/react";
import { useMemo } from "react";
import {
  FaChevronDown
} from "react-icons/fa6";
import {
  RiInsertColumnLeft,
  RiInsertColumnRight,
  RiInsertRowBottom,
  RiInsertRowTop
} from "react-icons/ri";
import {
  BasicLayout,
  BasicLayoutElement,
} from "./useLayoutForm";
import { LuMinus, LuPlus } from "react-icons/lu";
import * as LayoutUtils from "./LayoutUtils";

interface LayoutVisualizerProps {
  layout: BasicLayout;
  handleSelectElement: (element: BasicLayoutElement, multiple: boolean) => void;
  selectedElementKeys: string[];
}
 const Visualizer = ({
  layout,
  handleSelectElement,
  selectedElementKeys,
}: LayoutVisualizerProps) => {
  return (
    <Flex
      direction="column"
      gap={4}
      padding={6}
      flex={1}
      border="1px solid"
      borderColor="border"
      background="bg.panel"
      borderRadius="md"
      overflow="hidden"
    >
      <Flex
        grow={1}
        height="2em"
        bg="brand.600"
        alignItems="center"
        justifyContent="center"
        userSelect="none"
        width="100%"
      >
        Pantalla
      </Flex>
      <Grid
        templateColumns={`repeat(${layout.columns}, minmax(2em, 1fr))`}
        gapX={2}
        gapY={1}
        fontSize={"14px"}
      >
        {layout.elements.map((item) => {
          let iconLabel = LayoutUtils.elementTypes.find((e) => e.value == item.type);
          return (
            <Box
              onClick={(e) => handleSelectElement(item, e.shiftKey)}
              userSelect="none"
              key={item.key}
              cursor="pointer"
            >
              <Box
                borderRadius="md"
                color={iconLabel?.icon?.color ?? "bg"}
                display="flex"
                alignItems="center"
                height="2em"
                width="2em"
                justifyContent="center"
                border="2px solid"
                bg={LayoutUtils.getBackgroundColor(item.type)}
                borderColor={
                  selectedElementKeys.includes(item.key)
                    ? "brand.400"
                    : "transparent"
                }
              >
                {item.type !== LayoutElementType.BLANK
                  ? LayoutUtils.getIcon(item.type)
                  : null}
              </Box>
              {[LayoutElementType.AISLE, LayoutElementType.BLANK].includes(
                item.type
              ) ? null : (
                <Text textAlign="center" color="text.subtle" fontSize="xs">
                  {item.label}
                </Text>
              )}
            </Box>
          );
        })}
      </Grid>
    </Flex>
  );
};

interface ElementDetailsProps {
  elements: BasicLayoutElement[];
  onTypeChange: (newType: LayoutElementType) => void;
  onClose: () => void;
}

const ElementDetails = ({
  elements,
  onTypeChange,
  onClose,
}: ElementDetailsProps) => {
  const {
    selectedType,
    labels,
    elementCountByType,
    seatButtons,
    extraButtons,
  } = useMemo(() => {
    // Estado inicial
    let firstType = elements[0]?.type;
    let allSameType = true;
    const labels: string[] = [];
    const counts = {
      blanks: 0,
      aisles: 0,
      seats: 0,
    };

    // Un solo recorrido por elements
    for (const element of elements) {
      // Verificar si todos los tipos son iguales
      if (allSameType && element.type !== firstType) {
        allSameType = false;
      }

      // Recolectar labels
      if (element.label) {
        labels.push(element.label);
      }

      // Contar por tipo
      if (element.type === LayoutElementType.AISLE) {
        counts.aisles++;
      } else if (element.type === LayoutElementType.BLANK) {
        counts.blanks++;
      } else {
        counts.seats++;
      }
    }

    // Ordenar labels solo una vez al final
    labels.sort((a, b) => (a > b ? 1 : -1));

    return {
      selectedType: allSameType ? firstType : null,
      labels: labels.join(","),
      elementCountByType: counts,
      seatButtons: LayoutUtils.elementTypes.slice(0, 4),
      extraButtons: LayoutUtils.elementTypes.slice(4),
    };
  }, [elements]);

  return (
    <Card.Root minWidth={200} maxWidth={600} flexShrink={0}>
      <Card.Body position="relative">
        <CloseButton
          position="absolute"
          top="1em"
          right="1em"
          onClick={onClose}
        />

        <Heading size="sm" color="text.subtle">
          Seleccionados
        </Heading>
        <Card.Title lineClamp={2}>
          {labels}
          {!!elementCountByType.aisles &&
            `${labels ? ", " : ""}Pasillos (${elementCountByType.aisles})`}
          {!!elementCountByType.blanks &&
            `${labels || elementCountByType.aisles ? ", " : ""}Vacio (${
              elementCountByType.blanks
            })`}
        </Card.Title>

        <Flex marginTop={4} direction="column" gap={2}>
          <Group attached>
            {seatButtons.map((t) => (
              <Button
                variant={t.value == selectedType ? "solid" : "outline"}
                colorPalette={t.value == selectedType ? "brand" : undefined}
                onClick={() => onTypeChange(t.value)}
              >
                {t.icon?.icon ? (
                  <Icon as={t.icon.icon} boxSize={t.icon.boxSize} />
                ) : null}
                {t.label}
              </Button>
            ))}
          </Group>
          <Group attached>
            {extraButtons.map((t) => (
              <Button
                variant={t.value == selectedType ? "solid" : "outline"}
                colorPalette={t.value == selectedType ? "brand" : undefined}
                onClick={() => onTypeChange(t.value)}
              >
                {t.icon?.icon ? (
                  <Icon as={t.icon.icon} boxSize={t.icon.boxSize} />
                ) : null}
                {t.label}
              </Button>
            ))}
          </Group>
        </Flex>
      </Card.Body>
    </Card.Root>
  );
};

const Controls = ({
  rows,
  columns,
  onResize,
  onInsertRow,
  onInsertColumn,
}: {
  rows: number;
  columns: number;
  onResize: (rows?: number | null, columns?: number | null) => void;
  onInsertRow: (position: "start" | "end") => void;
  onInsertColumn: (position: "start" | "end") => void;
}) => {
  return (
    <Flex gap={5} alignItems="end">
      <Field.Root>
        <Field.Label>Filas</Field.Label>
        <LayoutDimensionField
          value={`${rows}`}
          min={5}
          max={60}
          onValueChange={(e) => onResize(e.valueAsNumber)}
        />
      </Field.Root>
      <Field.Root>
        <Field.Label>Columnas</Field.Label>
        <LayoutDimensionField
          value={`${columns}`}
          min={5}
          max={60}
          onValueChange={(e) => onResize(null, e.valueAsNumber)}
        />
      </Field.Root>

      <Menu.Root>
        <Menu.Trigger asChild>
          <Button variant="outline" size="sm">
            Acciones
            <Icon as={FaChevronDown} color="text.subtle" />
          </Button>
        </Menu.Trigger>
        <Portal>
          <Menu.Positioner>
            <Menu.Content>
              <Menu.Item
                value="add-row-up"
                onClick={() => onInsertRow("start")}
              >
                <RiInsertRowBottom /> Insertar fila arriba
              </Menu.Item>
              <Menu.Item
                value="add-row-down"
                onClick={() => onInsertRow("end")}
              >
                <RiInsertRowTop /> Insertar fila debajo
              </Menu.Item>
              <Menu.Item
                value="add-col-right"
                onClick={() => onInsertColumn("end")}
              >
                <RiInsertColumnLeft /> Insertar columna a la derecha
              </Menu.Item>
              <Menu.Item
                value="add-col-left"
                onClick={() => onInsertColumn("start")}
              >
                <RiInsertColumnRight />
                Insertar columna a la izquierda
              </Menu.Item>
            </Menu.Content>
          </Menu.Positioner>
        </Portal>
      </Menu.Root>
    </Flex>
  );
};

const LayoutDimensionField = (props: NumberInputRootProps) => {
  return (
    <NumberInput.Root unstyled spinOnPress={false} {...props}>
      <HStack gap="2">
        <NumberInput.DecrementTrigger asChild>
          <IconButton variant="outline" size="xs">
            <LuMinus />
          </IconButton>
        </NumberInput.DecrementTrigger>
        <NumberInput.ValueText textAlign="center" fontSize="lg" minW="3ch" />
        <NumberInput.IncrementTrigger asChild>
          <IconButton variant="outline" size="xs">
            <LuPlus />
          </IconButton>
        </NumberInput.IncrementTrigger>
      </HStack>
    </NumberInput.Root>
  );
};
export default {
  Visualizer: Visualizer,
  ElementDetails: ElementDetails,
  Controls: Controls
};

