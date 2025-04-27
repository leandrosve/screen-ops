import { Flex, FlexProps } from "@chakra-ui/react";

const PageContent = ({ children, ...props }: FlexProps) => {
  return (
    <Flex direction="column" gap={3} flex={1} padding={5} paddingBottom='2em' alignSelf="stretch" {...props}>
      {children}
    </Flex>
  );
};

export default PageContent;
