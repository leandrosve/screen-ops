import { Field, Icon, Input, InputProps } from "@chakra-ui/react";
import RequiredIndicator from "./RequiredIndicator";
import { FaInfoCircle } from "react-icons/fa";

interface TextFieldProps extends InputProps {
  label: string;
  placeholder?: string;
  error?: string | null;
  required?: boolean;
  type?: string;
  helperText?: string;
}

export default function TextField({
  label,
  placeholder,
  error,
  required,
  type = "text",
  helperText,
  ...rest
}: TextFieldProps) {
  return (
    <Field.Root invalid={!!error}>
      <Field.Label>
        {label} {required && <RequiredIndicator />}
      </Field.Label>
      <Input placeholder={placeholder} {...rest} type={type} />
      <Field.ErrorText>{error}</Field.ErrorText> 
      {!!helperText && <Field.HelperText display='inline-block' gap={1}><Icon as={FaInfoCircle} marginTop='-0.25em'/> {helperText}</Field.HelperText>}
    </Field.Root>
  );
}
