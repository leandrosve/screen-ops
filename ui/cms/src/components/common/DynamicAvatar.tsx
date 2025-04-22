import {
  Avatar,
  AvatarGroup,
} from "@chakra-ui/react";
import { useMemo } from "react";

const COLORS = [
  "purple",
  "red",
  "orange",
  "yellow",
  "green",
  "teal",
  "blue",
  "cyan",
  "pink",
];

const DynamicAvatar = (props: {name: string}) => {
  const color = useMemo(() => {
    let hash = 0;
    if (!props.name) return "teal";
    for (var i = 0; i < props.name.length; i++) {
      hash = props.name.charCodeAt(i) + hash;
    }
    const position = hash % 9;
    return COLORS[position] + ".700";
  }, []);
  return (
    <AvatarGroup>
      <Avatar.Root fontWeight="bold" color="white" bg={color} {...props}>
        <Avatar.Fallback name = {props.name} />
      </Avatar.Root>
    </AvatarGroup>
  );
};

export default DynamicAvatar;
