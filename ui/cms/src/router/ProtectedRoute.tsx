import { Navigate, Outlet } from 'react-router-dom';

interface ProtectedRouteProps {
  isAllowed: boolean;
  redirectPath?: string;
  children?: React.ReactNode;
}

export const ProtectedRoute = ({
  isAllowed,
  redirectPath = '/login',
  children,
}: ProtectedRouteProps) => {
  if (!isAllowed) {
    return <Navigate to={redirectPath} replace />;
  }

  return children ? children : <Outlet />;
};