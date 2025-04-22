import { Accordion, Box, Flex, Icon, Tag, Text } from "@chakra-ui/react";
import { SidebarSubItem } from "./Sidebar";
import { Link } from "react-router-dom";
import { useState } from "react";

interface Props {
  item: SidebarSubItem;
  level?: number;
  currentPath?: string;
}

const SidebarItem = ({ item, level = 0, currentPath }: Props) => {
  const [expanded, setExpanded] = useState(currentPath?.startsWith(item.path + "/") ? ["expanded"] : []);
  if (item.subItems)
    return (
      <Accordion.Root collapsible variant="plain" value={expanded} onValueChange={(e) => setExpanded(e.value)}>
        <Accordion.Item value={"expanded"} >
          <Accordion.ItemTrigger
            flex={1}
            justifyContent="space-between"
            padding={2}
            fontWeight="bold"
            bg={!expanded[0] && (currentPath == item.path) ? "borders.subtle/50" : "none"}
          >
            <Flex gap={3} alignItems="center">
              {item.icon ? (
                <Tag.Root
                  colorScheme="primary"
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
                    <Icon
                      as={item.icon.type}
                      fill="brand.400"
                      stroke="brand.400"
                      boxSize={item.icon.boxSize ?? "1.5em"}
                    />
                  </Tag.Label>
                </Tag.Root>
              ) : null}
              <Link to={item.path} onClick={(e) => e.stopPropagation()}>
                {item.label}
              </Link>
            </Flex>
            <Accordion.ItemIndicator />
          </Accordion.ItemTrigger>
          <Accordion.ItemContent>
            <Flex paddingLeft={"1.5em"} alignItems='stretch'>
              <Box
                as="span"
                width="1px"
                alignSelf='stretch'
                bgGradient="to-b"
                gradientFrom="borders.base/25"
                gradientTo="borders.subtle"
              />
              <Flex direction="column" flex={1}>
                {item.subItems.map((i, index) => (
                  <SidebarItem
                    item={i}
                    key={index}
                    level={level + 1}
                    currentPath={currentPath}
                  />
                ))}
              </Flex>
            </Flex>
          </Accordion.ItemContent>
        </Accordion.Item>
      </Accordion.Root>
    );
  return (
    <Link to={item.path}>
      <Flex
        padding={2}
        color="text.300"
        gap={3}
        fontWeight={level == 0 ? "bold" : "normal"}
        bg={currentPath == item.path ? "borders.subtle/50" : "none"}
        transition="background 300ms ease"
        alignItems="center"
        borderRightRadius={5}
      >
        {!!item.icon && (
          <Tag.Root
            colorScheme="primary"
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
              <Icon
                as={item.icon.type}
                fill="brand.400"
                boxSize={item.icon.boxSize ?? "1.5em"}
              />
            </Tag.Label>
          </Tag.Root>
        )}
        {!!item.label && <Text as="h1"  fontWeight={(level == 0  || currentPath == item.path) ? "bold" : "normal"}>{item.label}</Text>}
      </Flex>
    </Link>
  );
};

export default SidebarItem;
