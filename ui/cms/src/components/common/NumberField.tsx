import {
  Field,
  NumberInput,
  NumberInputInputProps,
  NumberInputRootProps,
  Text,
} from "@chakra-ui/react";

interface NumberFieldProps extends NumberInputRootProps {
  label: string;
  placeholder?: string;
  error?: string;
  inputProps?: NumberInputInputProps;
  required?: boolean;
}

export default function NumberField({
  label,
  placeholder,
  error,
  required,
  inputProps,
  ...props
}: NumberFieldProps) {
  return (
    <Field.Root invalid={!!error}>
      <Field.Label>
        {label}{" "}
        {required && (
          <Text as="span" color="fg.error">
            *
          </Text>
        )}
      </Field.Label>
      <NumberInput.Root
        invalid={!!error}
        width="full"
        {...props}
      >
        <NumberInput.Control>
          <NumberInput.IncrementTrigger />
          <NumberInput.DecrementTrigger />
        </NumberInput.Control>
        <NumberInput.Scrubber />
        <NumberInput.Input {...inputProps} placeholder={placeholder} />
      </NumberInput.Root>
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
