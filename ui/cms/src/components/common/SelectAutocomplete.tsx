import { useElementWidth } from "@/hooks/useElementWidth";
import {
  Box,
  Button,
  CloseButton,
  Field,
  Flex,
  Icon,
  Input,
  InputGroup,
  Menu,
  Text,
} from "@chakra-ui/react";
import { ReactNode, useCallback, useMemo, useRef, useState } from "react";
import { FaSearch } from "react-icons/fa";
import { FaChevronDown } from "react-icons/fa6";
import CheckBadge from "./CheckBadge";

interface Option {
  value: string;
  label: string;
}

interface Props {
  options: Option[];
  loading?: boolean;
  value?: string | null;
  onSelect?: (option: Option | null) => void;
  placeholder: string;
  label?: string;
  error?: string | null;
  searchPlaceholder?: string;
  hasSelectedCheckIcon?: boolean;
  disabled?: boolean;
  contentEndElement?: ReactNode;
}

const SelectAutocomplete = ({
  options,
  value,
  loading,
  placeholder,
  label,
  disabled,
  searchPlaceholder = "Buscar",
  error,
  onSelect,
  contentEndElement,
}: Props) => {
  const triggerRef = useRef<HTMLButtonElement>(null);

  const selectedItem = useMemo(() => {
    return options.find((o) => o.value == value);
  }, [value, options]);

  const [searchTerm, setSearchTerm] = useState("");

  const filtered = useMemo(() => {
    return options.filter((i) =>
      i.label.toLowerCase().includes(searchTerm.toLocaleLowerCase().trim())
    );
  }, [searchTerm, options]);

  const handleSelect = useCallback((option: Option | null) => {
    onSelect?.(option);
  }, []);

  const contentWidth = useElementWidth(triggerRef, 200);
  return (
    <Flex direction="column" alignItems="stretch" gap={1}>
      {label && <Text fontSize="sm">{label}</Text>}
      <Menu.Root positioning={{ placement: "bottom-start" }}>
        <Menu.Trigger asChild>
          <Button
            ref={triggerRef}
            variant="outline"
            as="span"
            size="sm"
            justifyContent="space-between"
            border="1px solid"
            borderColor={error ? "border.error" : "border"}
            loading={loading}
            disabled={loading || disabled}
          >
            {selectedItem ? (
              selectedItem.label
            ) : (
              <Text as="span" color="text.subtle">
                {placeholder}
              </Text>
            )}
            <Flex alignItems="center" gap={2}>
              <Icon as={FaChevronDown} boxSize="1em" />
            </Flex>
          </Button>
        </Menu.Trigger>
        {error && (
          <Text fontSize="xs" color="fg.error">
            {error}
          </Text>
        )}
        <Menu.Positioner>
          <Menu.Content width={contentWidth}>
            <Box>
              <InputGroup endElement={<FaSearch />}>
                <Input
                  value={searchTerm}
                  placeholder={searchPlaceholder}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
              </InputGroup>
            </Box>
            <Menu.Separator />
            <Menu.Arrow />

            {filtered.map((item) => {
              let isSelected = selectedItem?.value == item.value;
              return (
                <Menu.Item
                  key={item.value}
                  value={item.value}
                  onClick={() => handleSelect(item)}
                  bg={isSelected ? "bg.emphasized" : "none"}
                  minHeight="2.5em"
                >
                  <Flex
                    justifyContent="space-between"
                    flex={1}
                    alignItems="center"
                  >
                    {item.label}
                    {isSelected && <CheckBadge />}
                  </Flex>
                </Menu.Item>
              );
            })}
            {!!contentEndElement && (
              <Box width="full" position="sticky" bottom={0} left={0}>
                {contentEndElement}
              </Box>
            )}
          </Menu.Content>
        </Menu.Positioner>
      </Menu.Root>
    </Flex>
  );
};

export default SelectAutocomplete;
