import { Box, Flex, Heading, List, ListItem, Text } from "@chakra-ui/react";
import { useContext, useMemo } from "react";
import SessionContext from "@/context/SessionContext";
import Role from "@/model/user/Role";
import SidebarItem from "./SidebarItem";
import useCurrentPath from "@/hooks/useCurrentPath";
import { IconType } from "react-icons";
import { FaFilm } from "react-icons/fa6";
import { RiBuilding3Line } from "react-icons/ri";
import sidebarItems from "./sidebarItems";

export interface SidebarSubItem {
  path: string;
  label: string;
  roles?: Role[];
  icon?: { type: IconType; boxSize?: string };
  subItems?: SidebarSubItem[];
}

export interface SidebarItemGroup {
  group: string; // nombre del grupo (ej: "Contenidos", "Admin")
  items: (SidebarSubItem | (SidebarSubItem & { subItems: SidebarSubItem[] }))[];
}

const Sidebar = () => {
  const { session } = useContext(SessionContext);
  const currentPath = useCurrentPath();
  const allowedItems = useMemo(() => {
    return sidebarItems
      .map((group) => ({
        ...group,
        items: group.items
          .filter((item) => {
            const allowed =
              !item.roles ||
              (session?.user && item.roles.includes(session.user.role));
            return allowed;
          })
          .map((item) => {
            if (item.subItems) {
              const allowedSubItems = item.subItems.filter((sub) => {
                return (
                  !sub.roles ||
                  (session?.user && sub.roles.includes(session.user.role))
                );
              });
              return { ...item, subItems: allowedSubItems };
            }
            return item;
          })
          .filter((item) => {
            // en caso de que un item se quede sin subItems, no mostrarlo
            if (item.subItems) {
              return item.subItems.length > 0;
            }
            return true;
          }),
      }))
      .filter((group) => group.items.length > 0); // eliminar grupos vacíos
  }, [sidebarItems, session]);

  return (
    <Flex
      alignSelf="stretch"
      background="bg.300"
      minWidth={"15em"}
      padding={2}
      as="nav"
      borderRight="1px solid"
      borderColor="borders.subtle"
    >
      <List.Root
        display="flex"
        flexDirection="column"
        flexGrow={1}
        gap={1}
        paddingLeft={5}
      >
        {allowedItems.map((group) => (
          <Box key={group.group} mb={4}>
            <Heading mb={1} size="lg" color="text.subtle">
              {group.group}
            </Heading>
            {group.items.map((item, index) => (
              <SidebarItem item={item} key={index} currentPath={currentPath}/>
            ))}
          </Box>
        ))}
      </List.Root>
    </Flex>
  );
};

export default Sidebar;
