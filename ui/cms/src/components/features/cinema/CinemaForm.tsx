import { CinemaCreateDto } from "@/model/cinema/Cinema";
import { cinemaCreateSchema } from "@/validation/schemas/cinemaCreateSchema";
import {
  Box,
  Card,
  Flex,
  Icon,
  Tag,
  VStack,
  Text,
  Button,
  SimpleGrid,
} from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import TextField from "@/components/common/TextField";
import Alert from "@/components/common/Alert";
import TextAreaField from "@/components/common/TextAreaField";
import NumberField from "@/components/common/NumberField";
import SafeImage from "@/components/common/SafeImage";
import { FaCheck } from "react-icons/fa6";

interface Props {
  onSubmit: (dto: CinemaCreateDto) => void;
  isSubmitting?: boolean;
  error?: string;
  initialValues?: CinemaCreateDto;
}

const CinemaForm = ({
  isSubmitting,
  onSubmit,
  error,
  initialValues,
}: Props) => {
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(cinemaCreateSchema),
    defaultValues: initialValues,
  });

  const imageUrl = watch("imageUrl");
  return (
    <Flex
      as="form"
      onSubmit={handleSubmit((data) => onSubmit(data))}
      direction="column"
      gap={3}
      width="fit-content"
    >
      <SimpleGrid
        columns={{ base: 1, xl: 2 }}
        columnGap={10}
        rowGap={3}
        w="100%"
      >
        <VStack alignSelf="stretch" align="stretch" width={500}>
          {error && <Alert title={error} status="error" variant="subtle" />}
          <TextField
            label="Nombre"
            placeholder="Nombre"
            required
            disabled={isSubmitting}
            autoFocus
            error={errors.name?.message}
            {...register("name")}
          />

          <TextField
            label="Dirección"
            placeholder="Dirección"
            disabled={isSubmitting}
            required
            error={errors.location?.message}
            {...register("location")}
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
        <VStack alignSelf="stretch" align="stretch" width={500}>
          <NumberField
            label="Capacidad"
            placeholder="Capacidad"
            disabled={isSubmitting}
            required
            error={errors.capacity?.message}
            inputProps={register("capacity")}
          />
          <TextField
            label="Imágen"
            disabled={isSubmitting}
            placeholder="https://example.com/image.jpg"
            {...register("imageUrl")}
            error={errors.imageUrl?.message}
            required
          />
          {imageUrl && !errors.imageUrl?.message && (
            <Card.Root width="100%">
              <Card.Body
                display="flex"
                alignItems="center"
                flexDirection="row"
                padding={3}
                gap={3}
              >
                <Box
                  position="relative"
                  height={14}
                  width={14}
                  overflow="hidden"
                  rounded="md"
                  border="1px solid"
                  borderColor="borders.subtle"
                >
                  <SafeImage
                    src={imageUrl}
                    width="full"
                    height="full"
                    alt={`Poster`}
                  />
                </Box>

                <Text flex={1} truncate fontSize="xs" color="text.subtle">
                  {imageUrl}
                </Text>

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
          )}
        </VStack>
      </SimpleGrid>
      <Flex gap={5} alignItems="end">
        <Box
          bg="borders.subtle"
          height="1px"
          borderRadius="lg"
          bgGradient="to-r"
          gradientFrom="borders.base/5"
          gradientTo="borders.subtle/25"
          flex={1}
        />

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
      </Flex>
    </Flex>
  );
};

export default CinemaForm;
