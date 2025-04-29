import { Icon, Tag } from "@chakra-ui/react";
import { FaCheck } from "react-icons/fa6";

const CheckBadge = () => {
  return (
    <Tag.Root
      colorPalette="green"
      padding={2}
      borderRadius={50}
      width="2em"
      height="2em"
      display="flex"
      alignItems="center"
      justifyContent="center"
      border="none"
      boxShadow="none"
      aria-hidden
    >
      <Tag.Label>
        <Icon as={FaCheck} boxSize="1.4em" />
      </Tag.Label>
    </Tag.Root>
  );
};

export default CheckBadge;
