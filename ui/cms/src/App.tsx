import "./App.css";
import { ChakraProvider } from "@chakra-ui/react";
import { SessionProvider } from "./context/SessionContext";
import CmsRouter from "./router/CmsRouter";
import customSystem from "./theme";
import { ColorModeProvider } from "@/components/ui/color-mode";
import { ConfirmationDialogProvider } from "./components/common/ConfirmationDialog";
import { Toaster } from "./components/ui/toaster";

function App() {
  return (
    <ChakraProvider value={customSystem}>
      <ColorModeProvider>
        <SessionProvider>
            <ConfirmationDialogProvider/>
            <Toaster />
            <CmsRouter />
        </SessionProvider>
      </ColorModeProvider>
    </ChakraProvider>
  );
}

export default App;
