import { lazy, ReactNode } from 'react';
import Role from '../model/user/Role';

const LoginPage = lazy(() => import('@/components/pages/LoginPage'));
const MoviesPage = lazy(() => import('@/components/pages/movies/MoviesPage'));
const MovieCreatePage = lazy(() => import('@/components/pages/movies/MovieCreatePage'));

export enum CmsRoutes {
  HOME = '/',
  SIGNUP = '/signup',
  LOGIN = '/login',
  MOVIES = '/movies',
}

interface CmsRoute {
    path: string,
    type: 'private' | 'guest' | 'any',
    title: string,
    hasSubroutes?: boolean,
    subroutes?: {title: string, path: string}[],
    element: ReactNode,
    roles?: Role[]
}

const routes: CmsRoute[] = [
  {
    path: '/movies',
    type: 'private',
    title: 'Películas',
    hasSubroutes: false,
    subroutes: [],
    element: <MoviesPage/>,
    roles: [Role.MANAGER, Role.ADMIN]
  },
  {
    path: '/movies/create',
    type: 'private',
    title: 'Añadir Película',
    hasSubroutes: false,
    subroutes: [],
    element: <MovieCreatePage/>,
    roles: [Role.MANAGER, Role.ADMIN]
  },
  {
    type: 'guest',
    path: '/',
    title: 'Controla el progreso de tus alumnos',
    element: <div>Landing Page</div>,
  },
  {
    type: 'guest',
    path: '/signup',
    title: 'Sign Up',
    element: <div>Registrarse</div>,
  },
  {
    type: 'guest',
    path: '/login',
    title: 'Iniciar Sesión',
    element: <LoginPage/>,
  },
  {
    path: '/mantenimiento',
    type: 'any',
    title: 'Mantenimiento',
    element: <div>mantenimiento</div>,
  },
  { path: '*', type: 'any', title: 'Página no encontrada', element: <div>Pagina no encontrada</div> },
];

export default routes;