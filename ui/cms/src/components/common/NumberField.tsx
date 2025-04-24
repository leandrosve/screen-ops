import { Field, NumberInput, NumberInputInputProps, Text } from "@chakra-ui/react";

interface NumberFieldProps extends NumberInputInputProps{
  label: string;
  placeholder?: string;
  error?: string;
  inputProps?: NumberInputInputProps;
  required?: boolean;
  defaultValue?: number;
}

export default function NumberField({
  label,
  placeholder,
  error,
  required,
  ...inputProps

}: NumberFieldProps) {
  return (
    <Field.Root invalid={!!error} >
      <Field.Label>
        {label} {required && <Text as='span' color='fg.error'>*</Text>}
      </Field.Label>
      <NumberInput.Root invalid={!!error} width="full">
        <NumberInput.Control>
          <NumberInput.IncrementTrigger />
          <NumberInput.DecrementTrigger />
        </NumberInput.Control>
        <NumberInput.Scrubber />
        <NumberInput.Input
          {...inputProps}
          placeholder={placeholder}
        />
      </NumberInput.Root>
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
