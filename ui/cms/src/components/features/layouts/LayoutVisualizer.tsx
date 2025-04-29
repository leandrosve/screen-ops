import { Layout, LayoutElementType } from "@/model/layout/Layout";
import { Flex, Grid, Box, Text } from "@chakra-ui/react";
import * as LayoutUtils from "./LayoutUtils";

const LayoutVisualizer = ({ layout }: { layout: Layout }) => {
  return (
    <Flex
      direction="column"
      gap={4}
      padding={6}
      border="1px solid"
      borderColor="border"
      background="bg.panel"
      borderRadius="md"
      overflow="hidden"
      width='fit-content'
      maxWidth='100%'
    >
      <Flex
        height="2em"
        bg="brand.600"
        alignItems="center"
        justifyContent="center"
        width="100%"
        overflowX='hidden'
      >
        Pantalla
      </Flex>
      <Grid
        templateColumns={`repeat(${layout.columns}, minmax(2em, 1fr))`}
        gapX={2}
        gapY={1}
        fontSize={"14px"}
        overflow='auto'
      >
        {layout.elements.map((item) => {
          let iconLabel = LayoutUtils.elementTypes.find(
            (e) => e.value == item.type
          );
          return (
            <Flex key={item.index} direction='column' alignItems='center'>
              <Box
                borderRadius="md"
                color={iconLabel?.icon?.color ?? "bg"}
                display="flex"
                alignItems="center"
                height="2em"
                width="2em"
                justifyContent="center"
                bg={LayoutUtils.getBackgroundColor(item.type)}
              >
                {item.type !== LayoutElementType.BLANK
                  ? LayoutUtils.getIcon(item.type)
                  : null}
              </Box>
              {[LayoutElementType.AISLE, LayoutElementType.BLANK].includes(
                item.type
              ) ? null : (
                <Text textAlign="center" color="text.subtle" fontSize="xs">
                  {item.label}
                </Text>
              )}
            </Flex>
          );
        })}
      </Grid>
    </Flex>
  );
};

export default LayoutVisualizer;
