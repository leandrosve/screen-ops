import Alert from "@/components/common/Alert";
import { useConfirmDialog } from "@/components/common/ConfirmationDialog";
import CountrySelect from "@/components/common/CountrySelect";
import NumberField from "@/components/common/NumberField";
import RequiredIndicator from "@/components/common/RequiredIndicator";
import TextAreaField from "@/components/common/TextAreaField";
import TextField from "@/components/common/TextField";
import YouTubePlayer from "@/components/common/YouTubePlayer";
import MovieImageSelector from "@/components/features/movies/MovieImageSelector";
import { toaster } from "@/components/ui/toaster";
import { useYoutubeEmbedUrl } from "@/hooks/movies/useYoutubeEmbedUrl";
import { MovieCreateDto } from "@/model/movies/MovieCreateDto";
import MoviesService from "@/services/api/MoviesService";
import { genreOptions } from "@/static/genres";
import languages, { LanguageOption } from "@/static/languages";
import { MovieCreateErrors } from "@/validation/api-errors/MovieCreateErrors";
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
import { useCallback, useState } from "react";
import { useForm } from "react-hook-form";
import { FaCheck } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";


const MovieCreatePage = () => {
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    trigger,
    formState: { errors },
  } = useForm<MovieCreateDto>({
    resolver: yupResolver(movieCreateSchema),
  });

  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const [extraImageUrls, setExtraImageUrls] = useState<string[]>([]);

  const navigate = useNavigate();
  const {confirm} = useConfirmDialog();

  const submitAsync = useCallback(async (data: MovieCreateDto, forceCreate: boolean) => {
    setLoading(true);
    setError("");
    const res = await MoviesService.create({...data, extraImageUrls}, forceCreate);
    setLoading(false);
    if (res.hasError) {
      if (res.error == "original_title_repeated_use_force_create") {
        const confirmed = await confirm({
          title: "Advertencia",
          message: `Ya existe una pelicula con título "${data.originalTitle} ¿Quieres añadirla de todos modos?`,
          confirmText: "Sí, añadir",
          cancelText: "No, cancelar",
        })
        if (confirmed) {
          setLoading(true);
          submitAsync(data, true);
          return;
        }
      }
      setError(MovieCreateErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente.")
    }

    if (res.data){
      toaster.create({
        title: "Se ha añadido la película",
        type: 'success',
        duration: 3000
      })
  
      setTimeout(() => {
        navigate("/movies/" + res.data?.id)
      }, 3000);
      
    }
  }, [navigate, confirm])

  const posterUrlPreview = watch("posterUrl");
  const trailerUrlPreview = watch("trailerUrl");

  const embedUrl = useYoutubeEmbedUrl(trailerUrlPreview);

  return (
    <Flex
      as="form"
      onSubmit={handleSubmit((data) => submitAsync(data, false))}
      direction="column"
      gap={3}
      width='fit-content'
    >
      <Heading size="2xl">Añadir Pelicula</Heading>
      <SimpleGrid columns={{ base: 1, xl: 2 }} columnGap={10} rowGap={3} w="100%">
        <VStack alignSelf="stretch" align="stretch" width={500}>
         {error && <Alert title={error} status='error' variant='subtle'/>}
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
                  <Image
                    src={posterUrlPreview || "/placeholder.svg"}
                    width="full"
                    height="full"
                    alt={`Poster`}
                    onError={(e) => {
                      (e.target as HTMLImageElement).src =
                        "/placeholder.svg?height=64&width=64";
                    }}
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
          <MovieImageSelector onChange={(urls) => setExtraImageUrls(urls)} value={extraImageUrls}/>
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
          loading={loading}
        >
          Guardar
        </Button>
      </Flex>
    </Flex>
  );
};

export default MovieCreatePage;
