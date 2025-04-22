import { Field, Text, Textarea, TextareaProps } from "@chakra-ui/react";
import { UseFormRegisterReturn } from "react-hook-form";

interface TextFieldProps {
  label: string;
  placeholder?: string;
  error?: string;
  register: UseFormRegisterReturn;
  required?: boolean;
  innerProps?: TextareaProps;
}

export default function TextAreaField({
  label,
  placeholder,
  error,
  register,
  required,
  innerProps,
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
      <Textarea {...innerProps} placeholder={placeholder} {...register} />
      <Field.ErrorText>{error}</Field.ErrorText>
    </Field.Root>
  );
}
