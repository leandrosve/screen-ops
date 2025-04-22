import { createListCollection, Select, Portal } from "@chakra-ui/react";
import { useMemo } from "react";

interface SelectOption {
  value: string;
  label: string;
  icon?: React.ReactNode;
}

interface MultiSelectProps {
  options: SelectOption[];
  value: string[];
  onValueChange: (values: string[]) => void;
  placeholder?: string;
  width?: string;
  disabled?: boolean;
}

export const MultiSelect = ({
  options,
  onValueChange,
  value,
  placeholder = "Seleccionar...",
  width = "250px",
  disabled = false,
}: MultiSelectProps) => {
  const collection = useMemo(
    () =>
      createListCollection({
        items: options,
      }),
    [options]
  );

  return (
    <Select.Root
      multiple
      value={value.map((v) => v.toString())}
      collection={collection}
      onValueChange={(details) => onValueChange(details.value)}
      width={width}
      disabled={disabled}
    >
      <Select.HiddenSelect />
      <Select.Control>
        <Select.Trigger>
          <Select.ValueText placeholder={placeholder} />
        </Select.Trigger>
        <Select.IndicatorGroup>
          <Select.Indicator />
        </Select.IndicatorGroup>
      </Select.Control>
      <Portal>
        <Select.Positioner>
          <Select.Content>
            {collection.items.map((item) => (
              <Select.Item item={item} key={item.value}>
                {item.label}
                <Select.ItemIndicator />
              </Select.Item>
            ))}
          </Select.Content>
        </Select.Positioner>
      </Portal>
    </Select.Root>
  );
};
