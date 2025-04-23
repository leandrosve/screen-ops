import Alert from "@/components/common/Alert";
import CountrySelect from "@/components/common/CountrySelect";
import NumberField from "@/components/common/NumberField";
import RequiredIndicator from "@/components/common/RequiredIndicator";
import SafeImage from "@/components/common/SafeImage";
import TextAreaField from "@/components/common/TextAreaField";
import TextField from "@/components/common/TextField";
import YouTubePlayer from "@/components/common/YouTubePlayer";
import MovieImageSelector from "@/components/features/movies/MovieImageSelector";
import { useYoutubeEmbedUrl } from "@/hooks/movies/useYoutubeEmbedUrl";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import { genreOptions } from "@/static/genres";
import languages, { LanguageOption } from "@/static/languages";
import { movieCreateSchema } from "@/validation/schemas/movieCreateSchema";

import {
  Box,
  Button,
  Field,
  Flex,
  Heading,
  HStack,
  VStack,
  Image,
  Icon,
  Card,
  Text,
  Tag,
  SimpleGrid,
} from "@chakra-ui/react";
import { yupResolver } from "@hookform/resolvers/yup";
import { Select } from "chakra-react-select";
import { useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { FaCheck } from "react-icons/fa6";

interface Props {
  onSubmit: (dto: MovieCreateDto) => void;
  isSubmitting?: boolean;
  error?: string;
  initialValues?: MovieCreateDto;
}
const MovieForm = ({ isSubmitting, onSubmit, error, initialValues }: Props) => {
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    trigger,
    formState: { errors },
  } = useForm<MovieCreateDto>({
    resolver: yupResolver(movieCreateSchema),
    defaultValues: {genreIds: [], ...initialValues},
  });
  const [extraImageUrls, setExtraImageUrls] = useState<string[]>(
    initialValues?.extraImageUrls ?? []
  );
  const posterUrlPreview = watch("posterUrl");
  const trailerUrlPreview = watch("trailerUrl");
  const countryCode = watch("countryCode");
  const originalLanguageCode = watch("originalLanguageCode");
  const genreIds = watch("genreIds");

  const embedUrl = useYoutubeEmbedUrl(trailerUrlPreview);
  const languageOption = useMemo(() => {
    return languages.find((v) => v.value == originalLanguageCode) ?? null;
  }, [originalLanguageCode]);

  const genreIdOptions = useMemo(() => {
    return genreOptions.filter((v) => genreIds.includes(v.value)) ?? [];
  }, [genreIds]);


  return (
    <Flex
      as="form"
      onSubmit={handleSubmit((data) => onSubmit({ ...data, extraImageUrls }))}
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
            label="Título original"
            placeholder="Título original"
            required
            error={errors.originalTitle?.message}
            {...register("originalTitle")}
          />
          <TextField
            label="Título en castellano"
            placeholder="Título en castellano"
            required
            error={errors.localizedTitle?.message}
            {...register("localizedTitle")}
          />
          <Field.Root invalid={!!errors.genreIds}>
            <Field.Label>
              Géneros <RequiredIndicator />
            </Field.Label>
            <Select
              options={genreOptions}
              value={genreIdOptions}
              name="genres"
              isMulti
              placeholder="Selecciona uno o más géneros"
              onChange={(e) =>
                setValue(
                  "genreIds",
                  e.map((v) => v?.value).filter((v) => v !== undefined),
                  {
                    shouldTouch: true,
                    shouldDirty: true,
                    shouldValidate: true,
                  }
                )
              }
            />
            <Field.ErrorText>{errors.genreIds?.message}</Field.ErrorText>
          </Field.Root>
          <TextAreaField
            label="Descripción"
            placeholder="Descripción"
            required
            error={errors.description?.message}
            register={register("description")}
            innerProps={{ autoresize: true, minHeight: 200 }}
          />
          <TextField
            label="Director"
            placeholder="Director"
            required
            error={errors.director?.message}
            {...register("director")}
          />
          <TextField
            label="Actores Principales"
            placeholder="Actores Principales"
            required
            error={errors.mainActors?.message}
            {...register("mainActors")}
          />
          <HStack alignItems="start">
            <NumberField
              label="Duración (minutos)"
              placeholder="Duración"
              required
              error={errors.duration?.message}
              register={register("duration")}
            />

            <NumberField
              label="Año de estreno"
              placeholder="Año de estreno"
              required
              error={errors.originalReleaseYear?.message}
              inputProps={{ defaultValue: new Date().getFullYear() }}
              register={register("originalReleaseYear")}
            />
          </HStack>
          <Field.Root invalid={!!errors.countryCode}>
            <Field.Label>
              País de origen <RequiredIndicator />
            </Field.Label>
            <CountrySelect
              value={countryCode}
              onChange={(v) =>
                setValue("countryCode", v?.value ?? "", {
                  shouldTouch: true,
                  shouldDirty: true,
                  shouldValidate: true,
                })
              }
            />
            <Field.ErrorText>{errors.countryCode?.message}</Field.ErrorText>
          </Field.Root>
          <Field.Root invalid={!!errors.originalLanguageCode}>
            <Field.Label>
              Idioma original <RequiredIndicator />
            </Field.Label>
            <Select<LanguageOption>
              options={languages}
              placeholder="Selecciona un idioma"
              value={languageOption}
              onChange={(v) => {
                setValue("originalLanguageCode", v?.value ?? "", {
                  shouldTouch: true,
                  shouldDirty: true,
                  shouldValidate: true,
                });
              }}
            />
            <Field.ErrorText>
              {errors.originalLanguageCode?.message}
            </Field.ErrorText>
          </Field.Root>
        </VStack>
        <VStack alignItems="start" width={500} alignSelf="stretch">
          <Heading>Multimedia</Heading>
          <TextField
            label="URL del trailer"
            placeholder="https://www.youtube.com/watch?v=YoHD9XEInc0"
            {...register("trailerUrl", {
              onChange: async () => {
                await trigger("trailerUrl");
              },
            })}
            error={errors.trailerUrl?.message ?? embedUrl.error}
            required
          />

          {embedUrl.embedUrl && (
            <YouTubePlayer url={embedUrl.embedUrl} width="500" height="281" />
          )}

          <TextField
            label="URL del poster"
            placeholder="https://example.com/image.jpg"
            {...register("posterUrl", {
              onChange: async () => {
                await trigger("posterUrl");
              },
            })}
            error={errors.posterUrl?.message}
            required
          />
          {posterUrlPreview && !errors.posterUrl?.message && (
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
                    src={posterUrlPreview}
                    placeholder="/placeholder-poster.jpg"
                    width="full"
                    height="full"
                    alt={`Poster`}
                  />
                </Box>

                <Text flex={1} truncate fontSize="xs" color="text.subtle">
                  {posterUrlPreview}
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
          <MovieImageSelector
            onChange={(urls) => setExtraImageUrls(urls)}
            value={extraImageUrls}
          />
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

export default MovieForm;
