import { Pagination, ButtonGroup, IconButton, Flex } from "@chakra-ui/react";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa";
import { Select, SelectOption as SelectOption } from "./Select";

interface PaginatorProps {
  totalCount?: number;
  page: number;
  pageSize: number;
  onPageChange: (page: number) => void;
  onPageSizeChange?: (pageSize: number) => void;
  disabled?: boolean;
}

const pageSizes: SelectOption[] = [10, 20, 50].map((v) => ({
  value: `${v}`,
  label: `${v}`,
}));

export const Paginator = ({
  totalCount = 0,
  page,
  pageSize,
  onPageChange,
  onPageSizeChange,
  disabled = false,
}: PaginatorProps) => {
  return (
    <Flex alignItems="center" justifyContent="end" paddingX={5} gap={10}>
      <Pagination.Root
        count={totalCount}
        pageSize={pageSize}
        page={page}
        onPageChange={(e) => onPageChange(e.page)}
        onPageSizeChange={(e) => onPageSizeChange?.(e.pageSize)}
      >
        <ButtonGroup variant="ghost" size="sm">
          <Pagination.PrevTrigger asChild>
            <IconButton
              disabled={disabled || page <= 1}
              aria-label="Página anterior"
            >
              <FaChevronLeft />
            </IconButton>
          </Pagination.PrevTrigger>

          <Pagination.Items
            render={(pageItem) => (
              <IconButton
                variant={{ base: "ghost", _selected: "outline" }}
                aria-label={`Ir a página ${pageItem.value}`}
              >
                {pageItem.value}
              </IconButton>
            )}
          />

          <Pagination.NextTrigger asChild>
            <IconButton
              disabled={disabled || page * pageSize >= totalCount}
              aria-label="Página siguiente"
            >
              <FaChevronRight />
            </IconButton>
          </Pagination.NextTrigger>
        </ButtonGroup>
      </Pagination.Root>
      <Flex alignItems="baseline" gap={3} justifyContent="end">
        Resultados por página
        <Select
          options={pageSizes}
          value={`${pageSize}`}
          onValueChange={(v) => onPageSizeChange?.(parseInt(v))}
          placeholder=""
          width="4em"
        />
      </Flex>
    </Flex>
  );
};
