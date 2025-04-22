import {
  Box,
  Button,
  Flex,
  Heading,
  Icon,
  Menu,
  Tag,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";
import SessionService from "../../services/SessionService";
import { useContext } from "react";
import { FaPen, FaRightFromBracket } from "react-icons/fa6";
import SessionContext from "../../context/SessionContext";
import DynamicAvatar from "../../components/common/DynamicAvatar";
import { CmsRoutes } from "../../router/routes";
import { ColorModeButton } from "@/components/ui/color-mode";
const Navbar = () => {
  return (
    <Flex
      as="header"
      align="center"
      justify="space-between"
      alignSelf="stretch"
      paddingX={5}
      background="bg.300"
      paddingY={2}
      borderBottom="1px solid"
      borderColor="borders.subtle"
    >
      <Link to={CmsRoutes.HOME}>
        <Flex alignItems="flex-end" gap={3} position='relative'>
          <Heading
            as="span"
            fontWeight="light"
            fontSize={30}
            color="brand.400"
          >
            ScreenOps
          </Heading>
          <Box as='span' fontWeight="bold" fontSize='md' position='absolute' top={0} left={"100%"}
            color="brand.500">CMS</Box>
        </Flex>
      </Link>
   
      <Flex  as="nav" alignItems='center' gap={4} paddingRight={3}>
      <ColorModeButton/>
      <NavbarDropdown />
      </Flex>
    </Flex>
  );
};

const NavbarDropdown = () => {
  const { session } = useContext(SessionContext);
  const { user } = session ?? {};
  return (
    <Menu.Root>
      <Menu.Trigger asChild>
        <Button
          aria-label="User Options"
          variant="ghost"
          rounded="full"
          padding={0}
          children={
            <DynamicAvatar name={user?.firstName + " " + user?.lastName} />
          }
        />
      </Menu.Trigger>
      <Menu.Positioner>
        <Menu.Content>
          <Menu.ItemGroup>
            <Flex direction="column" padding={2}>
              <Flex>{user?.firstName} {user?.lastName}</Flex>
              <Flex justifyContent="center" alignItems="end" gap={3}>
                <Flex gap={4}>
                  <Flex fontSize="sm" color="text.subtle">
                    {user?.email}
                  </Flex>{" "}
                  <Tag.Root>
                    <Tag.Label>{user?.role}</Tag.Label>
                  </Tag.Root>
                </Flex>
              </Flex>
              <Flex>
              </Flex>
            </Flex>
          </Menu.ItemGroup>
          <Menu.Separator />
          <Menu.ItemGroup>
            <Menu.Item
              value="Editar perfil"
              justifyContent="space-between"
              gap={5}
            >
              Editar perfil <Icon as={FaPen} />
            </Menu.Item>

            <Menu.Item
              value="Cerrar sesión"
              justifyContent="space-between"
              gap={5}
              onClick={SessionService.destroyLocalSession}
            >
              Cerrar sesión <Icon as={FaRightFromBracket} boxSize={5} />{" "}
            </Menu.Item>
          </Menu.ItemGroup>
        </Menu.Content>{" "}
      </Menu.Positioner>
    </Menu.Root>
  );
};

export default Navbar;
