import {
  createListCollection,
  Select as ChakraSelect,
  Portal,
} from "@chakra-ui/react";
import { useMemo } from "react";

export interface SelectOption {
  value: string;
  label: string;
  icon?: React.ReactNode;
}

interface SelectProps {
  options: SelectOption[];
  value?: string;
  onValueChange: (value: string) => void;
  placeholder?: string;
  width?: string;
  disabled?: boolean;
}

export const Select = ({
  options,
  onValueChange,
  value,
  placeholder = "Seleccionar...",
  width = "250px",
  disabled = false,
}: SelectProps) => {
  const collection = useMemo(
    () =>
      createListCollection({
        items: options,
      }),
    [options]
  );

  return (
    <ChakraSelect.Root
      value={[value ?? ""]}
      collection={collection}
      onValueChange={(details) => onValueChange(details.value[0])}
      width={width}
      disabled={disabled}
    >
      <ChakraSelect.HiddenSelect />
      <ChakraSelect.Control>
        <ChakraSelect.Trigger>
          <ChakraSelect.ValueText placeholder={placeholder} />
        </ChakraSelect.Trigger>
        <ChakraSelect.IndicatorGroup>
          <ChakraSelect.Indicator />
        </ChakraSelect.IndicatorGroup>
      </ChakraSelect.Control>
      <Portal>
        <ChakraSelect.Positioner>
          <ChakraSelect.Content>
            {collection.items.map((item) => (
              <ChakraSelect.Item item={item} key={item.value}>
                {item.label}
                <ChakraSelect.ItemIndicator />
              </ChakraSelect.Item>
            ))}
          </ChakraSelect.Content>
        </ChakraSelect.Positioner>
      </Portal>
    </ChakraSelect.Root>
  );
};
