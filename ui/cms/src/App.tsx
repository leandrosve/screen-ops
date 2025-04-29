import "./App.css";
import { ChakraProvider } from "@chakra-ui/react";
import { SessionProvider } from "./context/SessionContext";
import customSystem from "./theme";
import { ColorModeProvider } from "@/components/ui/color-mode";
import { ConfirmationDialogProvider } from "./components/common/ConfirmationDialog";
import { Toaster } from "./components/ui/toaster";
import { Outlet, RouterProvider } from "react-router-dom";
import { routerV2 } from "./router/CmsRouter";

function App() {
  return (
    <ChakraProvider value={customSystem}>
      <ColorModeProvider defaultTheme="dark">
        <SessionProvider>
          <ConfirmationDialogProvider />
          <Toaster />
          <RouterProvider router={routerV2}>
          </RouterProvider>
        </SessionProvider>
      </ColorModeProvider>
    </ChakraProvider>
  );
}

export default App;
