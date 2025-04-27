import { useCallback } from "react";
import LayoutEditor from "./LayoutEditor";
import {
  Box,
  Button,
  Flex,
  Heading,
  Icon,
  Kbd,
  Text,
  VStack,
} from "@chakra-ui/react";
import TextField from "@/components/common/TextField";
import useLayoutEditor, { BasicLayout, BasicLayoutElement } from "./useLayoutForm";
import PageContent from "@/layout/PageContent";
import { useForm } from "react-hook-form";
import { layoutCreateSchema } from "@/validation/schemas/layoutCreateSchema";
import { yupResolver } from "@hookform/resolvers/yup";
import { LayoutCreateDto } from "@/model/layout/Layout";
import Alert from "@/components/common/Alert";
import { FaInfoCircle } from "react-icons/fa";

interface Props {
  onSubmit: (dto: LayoutCreateDto) => void;
  isSubmitting?: boolean;
  error?: string;
  initialValues?: LayoutCreateDto | null;
  title?: string;
}


const LayoutForm =({
  isSubmitting,
  onSubmit,
  error,
  initialValues,
  title,
}: Props)  => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(layoutCreateSchema),
    defaultValues: {name: initialValues?.name ?? ""}
  });

  const {
    layout,
    selectedElements,
    selectedElementKeys,
    handleDeselectAll,
    handleResize,
    handleSelectElement,
    handleTypeChange,
    handleInsertRow,
    handleInsertColumn,
  } = useLayoutEditor(initialValues);

  const innerSubmit = useCallback(async ({ name }: { name: string }) => {
    let dto: LayoutCreateDto = {
      name: name,
      rows: layout.rows,
      columns: layout.columns,
      elements: layout.elements.map((e, index) => ({
        label: e.label,
        index: index,
        type: e.type,
      })),
    };
    onSubmit(dto)
  }, [layout]);

  return (
    <Flex
      width="100%"
      flex={1}
      as="form"
      direction='column'
      onSubmit={handleSubmit((data) => innerSubmit(data))}
    >
      <PageContent
        direction="column"
        gap={5}
        alignItems="start"
        position="relative"
        alignSelf='stretch'
      >
        <Heading size="2xl">{title}</Heading>
        {error && <Alert title={error} status="error" variant="subtle" />}
        <TextField
          maxWidth={500}
          label="Nombre"
          placeholder="Ej: Layout Común 15 Filas x 20 Columnas"
          helperText="Te ayudará a poder indentificarlo luego"
          required
          error={errors.name?.message}
          {...register("name")}
        />
        <Heading>Distribución de butacas</Heading>
        <Text>
          Seleccionar varios: <Kbd>Shift</Kbd> + <Kbd>Click</Kbd>
        </Text>
        <Flex gap={5} alignItems="start" maxWidth="100%">
          <VStack alignItems="start">
            <LayoutEditor.Controls
              rows={layout.rows}
              columns={layout.columns}
              onResize={handleResize}
              onInsertRow={handleInsertRow}
              onInsertColumn={handleInsertColumn}
            />
            <Flex gap={5} alignItems="start">
              <LayoutEditor.Visualizer
                handleSelectElement={handleSelectElement}
                selectedElementKeys={selectedElementKeys}
                layout={layout}
              />
              <VStack alignItems='start'>
              {!!selectedElements.length && (
                <LayoutEditor.ElementDetails
                  elements={selectedElements}
                  onTypeChange={(t) => handleTypeChange(selectedElements, t)}
                  onClose={() => handleDeselectAll()}
                />
              )}
              <Text color='text.subtle'><Icon as={FaInfoCircle} marginTop='-0.2em' marginRight={1}/> Cantidad total de butacas: {layout.totalSeats}</Text>
              </VStack>
            </Flex>
          </VStack>
        </Flex>
      </PageContent>
      <Flex
        borderTop="1px solid"
        borderColor="border"
        gap={5}
        alignItems="end"
        alignSelf="stretch"
        justifyContent="end"
        position="sticky"
        bottom={0}
        left={0}
        width="100%"
        padding={5}
        bg="bg.panel"
      >
        <Button
          type="submit"
          colorPalette="brand"
          minWidth={200}
          size="xl"
          fontWeight="bold"
          alignSelf="end"
          loading={isSubmitting}
        >
          Guardar
        </Button>
      </Flex>
    </Flex>
  );
};

export default LayoutForm;
