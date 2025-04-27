import { Flex } from "@chakra-ui/react";
import { ReactNode, useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import Navbar from "./navbar/Navbar";
import Sidebar from "./sidebar/Sidebar";
//import Sidebar from './sidebar/Sidebar'; <Sidebar open={displaySidebar} onClose={() => setDisplaySidebar(false)} />

interface Props {
  children?: ReactNode;
}
const PrivateLayout = ({ children }: Props) => {
  return (
    <Flex
      className="private-layout background-tone"
      grow={1}
      align="center"
      justify="space-between"
      direction={"column"}
      maxWidth="100%"
      height='100vh'
      bg="bg"
    >
      <Navbar   />

      <Flex grow={1} alignSelf="stretch" overflow='hidden'>
        <Sidebar />

        <Flex
          id="main"
          tabIndex={-1}
          grow={1}
          alignSelf="stretch"
          direction="column"
          position='relative'
          alignItems="start"
          as="main"
          overflowY='auto'
          
        >
          <Flex id="breadcrumb-container" />
          
          <Outlet  />
          
        </Flex>
      </Flex>
    </Flex>
  );
};

export default PrivateLayout;
