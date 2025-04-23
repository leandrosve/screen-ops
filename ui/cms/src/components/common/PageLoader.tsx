import { Flex, Spinner } from "@chakra-ui/react";

const PageLoader = () => {
  return (
    <Flex grow={1} align="center" justify={"center"} alignSelf="stretch">
      <Spinner mt={5} size="xl" color="primary.400" boxSize={["50px", 100]} />
    </Flex>
  );
};

export default PageLoader;
