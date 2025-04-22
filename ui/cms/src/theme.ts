import { createSystem, defaultConfig, defineConfig } from "@chakra-ui/react";

export const customConfig = defineConfig({
  theme: {
    tokens: {
      colors: {
        brand: {
          50: { value: "#fff7ed" },
          100: { value: "#ffedd5" },
          200: { value: "#fed7aa" },
          300: { value: "#fdba74" },
          400: { value: "#fb923c" },
          500: { value: "#f97316" },
          600: { value: "#ea580c" },
          700: { value: "#92310a" },
          800: { value: "#6c2710" },
          900: { value: "#3b1106" },
        },
        text: {
          subtle: { value: "#a1a1aa" },
          base: { value: "#4f4f4f" },
        },
        borders: {
          subtle: { value: "#18181b" },
          base: { value: "#a1a1aa" },
        },
      },
    },
    semanticTokens: {
      colors: {
        brand: {
          solid: { value: "{colors.brand.500}" },
          contrast: { value: "{colors.brand.100}" },
          fg: { value: "{colors.brand.700}" },
          muted: { value: "{colors.brand.100}" },
          subtle: {  value: "{colors.brand.200}" },
          emphasized: { value: "{colors.brand.300}" },
          focusRing: { value: "{colors.brand.500}" },
        },
        bg: {
          DEFAULT: {
            value: { _light: "{colors.white}", _dark: "rgb(12, 12, 12)" }, // Custom dark background
          },
          subtle: {
            value: { _light: "{colors.gray.50}", _dark: "red" }, // Custom dark subtle background
          },
          muted: {
            value: { _light: "{colors.gray.100}", _dark: "red" }, // Custom dark muted background
          },
        },
        borders: {
          subtle: {value:{ _light: "{colors.gray.200}", _dark: "#18181b" }},
        },
      },
    },
  },
});


export default createSystem(defaultConfig, customConfig);
