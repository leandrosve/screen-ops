import { Navigate, Outlet } from 'react-router-dom';

interface GuestRouteProps {
  isAllowed: boolean;
  redirectPath?: string;
  children?: React.ReactNode;
}

export const GuestRoute = ({
  isAllowed,
  redirectPath = '/',
  children,
}: GuestRouteProps) => {
  if (!isAllowed) {
    return <Navigate to={redirectPath} replace />;
  }

  return children ? children : <Outlet />;
};