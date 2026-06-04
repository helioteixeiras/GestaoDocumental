import { Routes } from '@angular/router';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { PlaceholderPage } from './shared/components/placeholder-page/placeholder-page';

const placeholder = (path: string, title: string): Routes[number] => ({
  path,
  component: PlaceholderPage,
  data: { title },
});

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        component: Dashboard,
        data: { title: 'Visão Geral' },
      },
      placeholder('documentos', 'Documentos'),
      placeholder('categorias-documento', 'Categorias de Documento'),
      placeholder('tipos-documento', 'Tipos de Documento'),
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
