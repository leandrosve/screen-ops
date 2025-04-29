import {
  Box,
  Flex,
  VStack,
  Button,
  SimpleGrid,
  Card,
  Text,
  Tag,
  Icon,
  Separator,
  Heading,
  Skeleton,
  Grid,
} from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import TextField from "@/components/common/TextField";
import Alert from "@/components/common/Alert";
import TextAreaField from "@/components/common/TextAreaField";
import { roomCreateSchema } from "@/validation/schemas/roomCreateSchema";
import { RoomCreateDto } from "@/model/room/Room";
import { Cinema } from "@/model/cinema/Cinema";
import SafeImage from "@/components/common/SafeImage";
import { FaCheck, FaPlus } from "react-icons/fa6";
import useFetchAPI from "@/hooks/useFetchAPI";
import CinemaService from "@/services/api/CinemaService";
import { ReactNode, useCallback, useEffect, useMemo, useState } from "react";
import SelectAutocomplete from "@/components/common/SelectAutocomplete";
import LayoutService from "@/services/api/LayoutService";
import FooterActionBar from "@/components/common/FooterActionBar";
import PageContent from "@/layout/PageContent";
import LayoutVisualizer from "../layouts/LayoutVisualizer";
import { Link } from "react-router-dom";
import { CmsRoutes } from "@/router/routes";

interface Props {
  onSubmit: (dto: RoomCreateDto) => void;
  isSubmitting?: boolean;
  error?: string;
  title?: string;
  lockCinema?: boolean;
  initialValues?: Partial<RoomCreateDto>;
  breadcrumbs?: ReactNode;
}

const RoomForm = ({
  isSubmitting,
  onSubmit,
  error,
  initialValues,
  title,
  lockCinema,
  breadcrumbs,
}: Props) => {
  const {
    register,
    handleSubmit,
    watch,
    setValue,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(roomCreateSchema),
    defaultValues: {
      name: "",
      description: "",
      cinemaId: "",
      layoutId: "",
      ...initialValues,
    },
  });

  const cinemaId = watch("cinemaId");
  const layoutId = watch("layoutId");

  return (
    <Flex
      flex={1}
      width="full"
      as="form"
      direction="column"
      onSubmit={handleSubmit((data) => onSubmit(data))}
    >
      <PageContent
        direction="column"
        gap={5}
        alignItems="start"
        position="relative"
        alignSelf="stretch"
        width="fit-content"
      >
        {breadcrumbs}
        {title && <Heading size="2xl">{title}</Heading>}
        <Flex gap={3} w="100%" wrap="wrap">
          <VStack alignSelf="stretch" align="stretch" width={500}>
            {error && <Alert title={error} status="error" variant="subtle" />}

            <CinemaSelectField
              value={cinemaId}
              locked={lockCinema}
              onSelect={(value) =>
                setValue("cinemaId", value ?? "", {
                  shouldTouch: true,
                  shouldDirty: true,
                  shouldValidate: true,
                })
              }
              error={errors.cinemaId?.message}
            />

            <Separator width="full" my={4} />
            <TextField
              label="Nombre"
              placeholder="Nombre"
              required
              disabled={isSubmitting}
              autoFocus
              error={errors.name?.message}
              {...register("name")}
            />

            <TextAreaField
              label="Descripción"
              placeholder="Descripción"
              disabled={isSubmitting}
              required
              error={errors.description?.message}
              minHeight={200}
              {...register("description")}
            />
          </VStack>
          <VStack alignSelf="stretch" align="stretch" minWidth={500}>
            <LayoutSelectField
              value={layoutId}
              onSelect={(value) =>
                setValue("layoutId", value ?? "", {
                  shouldTouch: true,
                  shouldDirty: true,
                  shouldValidate: true,
                })
              }
              error={errors.layoutId?.message}
            />
          </VStack>
        </Flex>
      </PageContent>
      <FooterActionBar>
        <Button
          type="submit"
          colorPalette="brand"
          size="xl"
          fontWeight="bold"
          alignSelf="end"
          loading={isSubmitting}
        >
          Guardar
        </Button>
      </FooterActionBar>
    </Flex>
  );
};

const CinemaSelectField = ({
  value,
  onSelect,
  locked,
  error,
}: {
  value?: string | null;
  error?: string | null;
  locked?: boolean;
  onSelect: (value: string | null) => void;
}) => {
  const {
    entity: cinemas,
    loading,
    error: apiError,
  } = useFetchAPI({
    fetchFunction: () => CinemaService.getCinemas(),
    initialFetch: true,
  });

  const options = useMemo(() => {
    if (!cinemas) return [];
    return cinemas?.map((c) => ({ label: c.name, value: c.id }));
  }, [cinemas]);

  const handleSelect = useCallback(
    (option: { value: string } | null) => {
      onSelect(option?.value ?? null);
    },
    [cinemas]
  );

  const cinema = useMemo(() => {
    return cinemas?.find((c) => c.id == value) ?? null;
  }, [cinemas, value]);

  return (
    <Flex direction="column" gap={2}>
      {apiError && (
        <Alert
          status="warning"
          description="No se han podido cargar los cines"
        />
      )}
      <SelectAutocomplete
        options={options}
        disabled={locked}
        loading={loading}
        error={error}
        onSelect={handleSelect}
        value={value}
        placeholder="Selecciona un cine"
        searchPlaceholder="Buscar por nombre"
        hasSelectedCheckIcon
        label="Cine"
      />
      {cinema && <CinemaCard cinema={cinema} />}
    </Flex>
  );
};

const CinemaCard = ({ cinema }: { cinema: Cinema }) => {
  return (
    <Card.Root width="100%">
      <Card.Body
        display="flex"
        alignItems="center"
        flexDirection="row"
        padding={3}
        gap={3}
        minHeight="4em"
      >
        {!!cinema.imageUrl && (
          <Box
            position="relative"
            height={"3em"}
            width={"3em"}
            overflow="hidden"
            rounded="md"
            flexShrink={0}
            borderRadius="full"
            border="1px solid"
            borderColor="borders.subtle"
          >
            <SafeImage
              src={cinema.imageUrl}
              width="full"
              height="full"
              alt={`Cinema`}
            />
          </Box>
        )}
        <VStack alignItems="start" gap={0.5} flex={1}>
          <Text truncate fontSize="xs" color="text.subtle" fontWeight="bold">
            {cinema.name}
          </Text>
          <Text truncate lineClamp={1} fontSize="xs" color="text.subtle">
            {cinema.description}
          </Text>
        </VStack>

        <Tag.Root
          colorPalette="green"
          padding={2}
          borderRadius={50}
          width="2em"
          height="2em"
          display="flex"
          alignItems="center"
          justifyContent="center"
          border="none"
          boxShadow="none"
          aria-hidden
        >
          <Tag.Label>
            <Icon as={FaCheck} boxSize="1.4em" />
          </Tag.Label>
        </Tag.Root>
      </Card.Body>
    </Card.Root>
  );
};

const LayoutSelectField = ({
  value,
  onSelect,
  error,
}: {
  value?: string | null;
  error?: string | null;
  onSelect: (value: string | null) => void;
}) => {
  const {
    entity: layouts,
    loading,
    error: apiError,
  } = useFetchAPI({
    fetchFunction: () => LayoutService.getLayouts(),
    initialFetch: true,
  });

  const options = useMemo(() => {
    if (!layouts) return [];
    return layouts?.map((c) => ({ label: c.name, value: c.id }));
  }, [layouts]);

  const handleSelect = useCallback(
    (option: { value: string } | null) => {
      onSelect(option?.value ?? null);
    },
    [layouts]
  );

  const layout = useMemo(() => {
    return layouts?.find((c) => c.id == value) ?? null;
  }, [layouts, value]);

  return (
    <Flex direction="column" gap={2} overflow="hidden">
      {apiError && (
        <Alert
          status="warning"
          description="No se han podido cargar los cines"
        />
      )}
      <SelectAutocomplete
        options={options}
        loading={loading}
        error={error}
        onSelect={handleSelect}
        value={value}
        placeholder="Selecciona un layout"
        searchPlaceholder="Buscar por nombre"
        hasSelectedCheckIcon
        label="Layout"
        contentEndElement={
          <Button asChild variant="subtle" width="full">
            <Link to={CmsRoutes.LAYOUT_CREATE}>
              <FaPlus /> Agregar Layout
            </Link>
          </Button>
        }
      />
      {layout && <LayoutDetail layoutId={layout.id} />}
    </Flex>
  );
};

const LayoutDetail = ({ layoutId }: { layoutId: string }) => {
  const {
    entity: layout,
    loading,
    error: apiError,
    fetchOther,
  } = useFetchAPI({
    fetchFunction: () => LayoutService.getDetail(layoutId), // No deberia llamarse
    initialFetch: false,
  });

  useEffect(() => {
    fetchOther(() => LayoutService.getDetail(layoutId));
  }, [layoutId]);

  if (loading) return <Skeleton width="full" height={300} />;
  if (!layout) return <Alert status="warning" description={apiError} />;
  return <LayoutVisualizer layout={layout} />;
};

export default RoomForm;
