import { PropsWithChildren, useContext } from 'react';
import PrivateLayout from './PrivateLayout';
import { useLocation } from 'react-router-dom';
import SessionContext from '../context/SessionContext';
import GuestLayout from './GuestLayout';

const Layout = (props: PropsWithChildren) => {
  const { isLoggedIn } = useContext(SessionContext);
  const location = useLocation();

  if (location.pathname == '/mantenimiento') return <>{props.children}</>;
  
  if (isLoggedIn) return <PrivateLayout>{props.children}</PrivateLayout>;
  return <GuestLayout>{props.children}</GuestLayout>;
};

export default Layout;