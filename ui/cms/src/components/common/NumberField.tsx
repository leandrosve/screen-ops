import { Field, NumberInput, NumberInputInputProps, Text } from "@chakra-ui/react";
import { UseFormRegisterReturn } from "react-hook-form";

interface NumberFieldProps {
  label: string;
  placeholder?: string;
  error?: string;
  register: UseFormRegisterReturn;
  inputProps?: NumberInputInputProps;
  required?: boolean;
  defaultValue?: number;
}

export default function NumberField({
  label,
  placeholder,
  error,
  register,
  required,
  inputProps,
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
          {...register}
        />
      </NumberInput.Root>
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
