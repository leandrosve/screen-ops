import { Field, Input, InputProps } from "@chakra-ui/react";
import RequiredIndicator from "./RequiredIndicator";

interface TextFieldProps extends InputProps {
  label: string;
  placeholder?: string;
  error?: string | null;
  required?: boolean;
  type?: string;
}

export default function TextField({
  label,
  placeholder,
  error,
  required,
  type = "text",
  ...rest
}: TextFieldProps) {
  return (
    <Field.Root invalid={!!error}>
      <Field.Label>
        {label} {required && <RequiredIndicator />}
      </Field.Label>
      <Input placeholder={placeholder} {...rest} type={type} />
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
