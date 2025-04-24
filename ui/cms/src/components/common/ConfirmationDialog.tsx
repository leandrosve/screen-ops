import { Button, CloseButton, Dialog, Portal } from "@chakra-ui/react";
import { useState, useCallback, useEffect } from "react";
import { createPortal } from "react-dom";

type ConfirmOptions = {
  title?: string;
  message?: string;
  confirmText?: string;
  cancelText?: string;
  confirmColorScheme?: string;
};

let setConfirmState: ((state: ConfirmState) => void) | null = null;

type ConfirmState = {
  isOpen: boolean;
  options: ConfirmOptions;
  resolve: (value: boolean) => void;
};

export const useConfirmDialog = () => {
  const confirm = useCallback((options: ConfirmOptions) => {
    return new Promise<boolean>((resolve) => {
      setConfirmState?.({
        isOpen: true,
        options,
        resolve,
      });
    });
  }, []);

  return { confirm };
};

export const ConfirmationDialogProvider = () => {
  const [state, setState] = useState<ConfirmState>({
    isOpen: false,
    options: {},
    resolve: () => {},
  });

  useEffect(() => {
    setConfirmState = setState;
  }, []);

  const handleConfirm = () => {
    state.resolve(true);
    setState((prev) => ({ ...prev, isOpen: false }));
  };

  const handleCancel = () => {
    state.resolve(false);
    setState((prev) => ({ ...prev, isOpen: false }));
  };

  if (typeof window === "undefined") return null;

  return createPortal(
    <Dialog.Root
      open={state.isOpen}
      onOpenChange={(open) => !open && handleCancel()}
    >
      <Portal>
        <Dialog.Backdrop />
        <Dialog.Positioner>
          <Dialog.Content>
            <Dialog.Header>
              <Dialog.Title>
                {state.options.title || "Confirmar acción"}
              </Dialog.Title>
            </Dialog.Header>
            <Dialog.Body>
              <p>{state.options.message || "¿Estás seguro?"}</p>
            </Dialog.Body>
            <Dialog.Footer>
              <Button variant="outline" onClick={handleCancel}>
                {state.options.cancelText || "Cancelar"}
              </Button>
              <Button
                colorScheme={state.options.confirmColorScheme || "red"}
                onClick={handleConfirm}
              >
                {state.options.confirmText || "Confirmar"}
              </Button>
            </Dialog.Footer>
            <Dialog.CloseTrigger asChild onClick={handleCancel}>
              <CloseButton size="sm" />
            </Dialog.CloseTrigger>
          </Dialog.Content>
        </Dialog.Positioner>
      </Portal>
    </Dialog.Root>,
    document.body
  );
};
