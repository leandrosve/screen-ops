import { JSX, lazy, LazyExoticComponent, ReactElement, ReactNode } from 'react';
import Role from '../model/user/Role';

/*const LoginPage = async () => (await import('@/components/pages/LoginPage')).default;
const MoviesPage = async () =>  (await import('@/components/pages/movies/MoviesPage')).default;
const MovieCreatePage = async () =>  (await import('@/components/pages/movies/MovieCreatePage')).default;
const MovieUpdatePage = async () =>  (await import('@/components/pages/movies/MovieUpdatePage')).default;
const MovieDetailPage = async () =>  (await import('@/components/pages/movies/MovieDetailPage')).default;*/

const LoginPage = lazy( () => import('@/components/pages/LoginPage'));
const MoviesPage = lazy( () => import('@/components/pages/movies/MoviesPage'));
const MovieCreatePage = lazy( () => import('@/components/pages/movies/MovieCreatePage'));
const MovieUpdatePage = lazy( () => import('@/components/pages/movies/MovieUpdatePage'));
const MovieDetailPage = lazy( () => import('@/components/pages/movies/MovieDetailPage'));

export enum CmsRoutes {
  HOME = '/',
  SIGNUP = '/signup',
  LOGIN = '/login',
  MOVIES = '/movies',
  MOVIE_CREATE = '/movies/create',
  MOVIE_DETAIL = '/movies/detail/:id',
  MOVIE_UPDATE = '/movies/update/:id'
}

interface CmsRoute {
    path: string,
    type: 'private' | 'guest' | 'any',
    title: string,
    hasSubroutes?: boolean,
    subroutes?: {title: string, path: string}[],
    element?: ReactElement,
    lazy?: LazyExoticComponent<() => JSX.Element>
    roles?: Role[]
}

const routes: CmsRoute[] = [

  {
    path: CmsRoutes.MOVIE_CREATE,
    type: 'private',
    title: 'Añadir Película',
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieCreatePage,
    roles: [Role.MANAGER, Role.ADMIN]
  },
  {
    path: CmsRoutes.MOVIE_DETAIL,
    type: 'private',
    title: 'Detalle de Película',
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieDetailPage,
    roles: [Role.MANAGER, Role.ADMIN]
  },
  {
    path: CmsRoutes.MOVIE_UPDATE,
    type: 'private',
    title: 'Actualizar Película',
    hasSubroutes: false,
    subroutes: [],
    lazy: MovieUpdatePage,
    roles: [Role.MANAGER, Role.ADMIN]
  },
  {
    path: CmsRoutes.MOVIES,
    type: 'private',
    title: 'Películas',
    hasSubroutes: false,
    subroutes: [],
    lazy: MoviesPage,
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
    lazy: LoginPage,
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