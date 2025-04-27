import { useMovies } from "@/hooks/movies/useMovies";
import {
  Box,
  Button,
  Flex,
  Heading,
  Input,
  Grid,
  Icon,
} from "@chakra-ui/react";
import { useState } from "react";
import { FaPlus } from "react-icons/fa6";
import { MovieStatus } from "@/model/movies/Movie";
import { MultiSelect } from "@/components/common/MultiSelect";
import { toMovieStatus } from "@/utils/EnumUtils";
import { Link } from "react-router-dom";
import { Paginator } from "@/components/common/Pagination";
import MovieCardItem from "@/components/features/movies/MovieCardItem";
import PageContent from "@/layout/PageContent";

const statusOptions = [
  {
    value: MovieStatus.DRAFT.toString(),
    label: "Borrador",
    colorScheme: "gray",
  },
  {
    value: MovieStatus.PUBLISHED.toString(),
    label: "Publicada",
    colorScheme: "green",
  },
  { value: MovieStatus.HIDDEN.toString(), label: "Oculta", colorScheme: "red" },
];

const MoviesPage = () => {
  const { data, loading, error, updateFilters, filters } = useMovies();

  const [searchTerm, setSearchTerm] = useState(filters.searchTerm);
  const [status, setStatus] = useState<string[]>(filters.status?.map(s => s.toString()) ?? []);
  if (error) return <div>{error}</div>;

  return (
    <PageContent>
      <Heading size="2xl">Películas</Heading>
      <Flex justifyContent="space-between">
        <form
          onSubmit={(e) => {
            e.preventDefault();
            updateFilters({
              searchTerm,
              page: 1,
              status: toMovieStatus(status),
            });
          }}
        >
          <Flex gap={3}>
            <Input
              id="search-term"
              width={300}
              value={searchTerm}
              placeholder="Buscar películas por nombre, año, etc..."
              onChange={(e) => setSearchTerm(e.target.value)}
            />

            <MultiSelect
              options={statusOptions}
              value={status}
              onValueChange={setStatus}
              placeholder="Todos los estados"
              width="300px"
            />

            <Button type="submit">Buscar</Button>
          </Flex>
        </form>
        <Link to="/movies/create">
          <Button
            as="span"
            type="submit"
            colorPalette="brand"
            fontWeight="bold"
          >
            <Icon as={FaPlus} boxSize="1em" /> Añadir Pelicula
          </Button>
        </Link>
      </Flex>

      {!loading &&
        (!!data?.totalCount ? (
          <span>
            {data.totalCount} resultado{data.totalCount > 1 ? "s" : ""}
          </span>
        ) : (
          !loading && <Heading>No se encontraron resultados</Heading>
        ))}
      <Box>
        <Grid
          templateColumns="repeat(auto-fit, minmax(260px, 5fr))"
          gridGap={5}
        >
          {loading
            ? [...Array(filters.pageSize)].map((_, index) => (
                <MovieCardItem.Skeleton key={index} />
              ))
            : data?.items.map((movie) => (
                <MovieCardItem.Card movie={movie} key={movie.id} />
              ))}
        </Grid>
      </Box>
      <Paginator
        totalCount={data?.totalCount ?? 0}
        pageSize={filters.pageSize}
        page={filters.page}
        onPageChange={(p) => updateFilters({ page: p })}
        onPageSizeChange={(ps) => updateFilters({ pageSize: ps })}
      />
    </PageContent>
  );
};

export default MoviesPage;
