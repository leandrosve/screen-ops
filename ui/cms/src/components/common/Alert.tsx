import {
  Alert as ChakraAlert,
  AlertIndicator,
  AlertRootProps,
  Box,
  CloseButton,
} from "@chakra-ui/react";
import {
  ReactNode,
  useEffect,
  useLayoutEffect,
  useMemo,
  useRef,
  useState,
} from "react";

interface Props extends AlertRootProps {
  hasIcon?: boolean;
  closable?: boolean;
  autoFocus?: boolean;
  description?: string | ReactNode;
  title?: string;
  size?: "sm" | "md";
  onClose?: () => void;
}

const Alert = ({
  hasIcon = true,
  closable,
  autoFocus,
  title,
  description,
  onClose,
  size,
  ...rest
}: Props) => {
  const ref = useRef<HTMLDivElement>(null);
  const [hidden, setHidden] = useState(true);

  const empty = useMemo(() => !title && !description, [title, description]);

  const handleClose = () => {
    setHidden(true);
    onClose?.();
  };

  useEffect(() => {
    setHidden(empty);
  }, [empty]);

  useLayoutEffect(() => {
    if (!hidden && autoFocus) {
      ref.current?.focus();
    }
  }, [hidden, autoFocus]);

  if (hidden) return null;

  return (
    <Box
      data-state="open"
      _open={{
        animation: "scale-in 300ms ease-out",
      }}
      _closed={{
        animation: "scale-out 300ms ease-in",
      }}
    >
      <ChakraAlert.Root
        {...rest}
        ref={ref}
        tabIndex={-1}
        paddingY={size == "sm" ? "3px" : "20px"}
        paddingRight={closable ? "40px" : undefined}
        borderRadius="md"
      >
        {hasIcon && <AlertIndicator boxSize={size == "sm" ? "20px" : "20px"} />}
        <ChakraAlert.Content>
          {title && <ChakraAlert.Title>{title}</ChakraAlert.Title>}
          {description && (
            <ChakraAlert.Description>{description}</ChakraAlert.Description>
          )}
        </ChakraAlert.Content>
        {closable && (
          <CloseButton
            size={size}
            position="absolute"
            right={"4px"}
            top="4px"
            onClick={handleClose}
          />
        )}
      </ChakraAlert.Root>
    </Box>
  );
};

export default Alert;
