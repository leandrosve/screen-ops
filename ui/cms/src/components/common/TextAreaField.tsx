import { Field, Text, Textarea, TextareaProps } from "@chakra-ui/react";
import { UseFormRegisterReturn } from "react-hook-form";

interface TextFieldProps extends TextareaProps{
  label: string;
  placeholder?: string;
  error?: string;
  required?: boolean;
}

export default function TextAreaField({
  label,
  placeholder,
  error,
  required,
  ...props
}: TextFieldProps) {
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
      <Textarea resize="none" placeholder={placeholder} {...props} />
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
