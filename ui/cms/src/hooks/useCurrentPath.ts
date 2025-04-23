import { useLocation } from 'react-router-dom';
import { useMemo } from 'react';

const useCurrentPath = () => {
  const location = useLocation();

  const item = useMemo(() => {
    const pathname = location.pathname;

    return pathname;
  }, [location]);

  return item;
};

export default useCurrentPath;