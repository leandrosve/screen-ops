import { Flex, FlexProps } from "@chakra-ui/react";

const FooterActionBar = ({ children, ...props }: FlexProps) => {
  return (
    <Flex
      borderTop="1px solid"
      borderColor="border"
      gap={5}
      alignItems="end"
      alignSelf="stretch"
      justifyContent="end"
      position="sticky"
      bottom={0}
      left={0}
      width="100%"
      padding={5}
      bg="bg.panel"
      {...props}
    >
      {children}
    </Flex>
  );
};

export default FooterActionBar;
