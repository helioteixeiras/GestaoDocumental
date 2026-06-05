import { Routes } from '@angular/router';
import { authGuard, guestGuard } from './core/auth/auth.guard';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { Login } from './features/auth/pages/login/login';
import { PlaceholderPage } from './shared/components/placeholder-page/placeholder-page';
import { CategoriaDocumentoList } from './features/categorias-documento/pages/categoria-documento-list/categoria-documento-list';
import { TipoDocumentoList } from './features/tipos-documento/pages/tipo-documento-list/tipo-documento-list';

const placeholder = (path: string, title: string): Routes[number] => ({
  path,
  component: PlaceholderPage,
  canActivate: [authGuard],
  data: { title },
});

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
    canActivate: [guestGuard],
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        component: Dashboard,
        canActivate: [authGuard],
        data: { title: 'Visão Geral' },
      },
      placeholder('documentos', 'Documentos'),
      {
        path: 'categorias-documento',
        component: CategoriaDocumentoList,
        canActivate: [authGuard],
        data: { title: 'Categorias de Documento' },
      },
      {
        path: 'tipos-documento',
        component: TipoDocumentoList,
        canActivate: [authGuard],
        data: { title: 'Tipos de Documento' },
      },
      placeholder('classificacoes', 'Classificações'),
      placeholder('estados-documento', 'Estados do Documento'),
      placeholder('direcoes', 'Direções'),
      placeholder('departamentos', 'Departamentos'),
      placeholder('colaboradores', 'Colaboradores'),
      placeholder('paises', 'Países'),
      placeholder('provincias', 'Províncias'),
      placeholder('municipios', 'Municípios'),
      placeholder('utilizadores', 'Utilizadores'),
      placeholder('perfis', 'Perfis'),
    ],
  },
];
