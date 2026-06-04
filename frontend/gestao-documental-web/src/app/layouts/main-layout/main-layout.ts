import { Component, ViewChild } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';

export interface NavItem {
  label: string;
  route: string;
  icon: string;
}

export interface NavGroup {
  title: string;
  items: NavItem[];
}

@Component({
  selector: 'app-main-layout',
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {
  @ViewChild('drawer') drawer!: MatSidenav;

  readonly appTitle = 'Gestão Documental';

  readonly navGroups: NavGroup[] = [
    {
      title: 'Dashboard',
      items: [{ label: 'Visão Geral', route: '/dashboard', icon: 'dashboard' }],
    },
    {
      title: 'Gestão Documental',
      items: [
        { label: 'Documentos', route: '/documentos', icon: 'description' },
        {
          label: 'Categorias de Documento',
          route: '/categorias-documento',
          icon: 'category',
        },
        { label: 'Tipos de Documento', route: '/tipos-documento', icon: 'topic' },
        { label: 'Classificações', route: '/classificacoes', icon: 'class' },
        {
          label: 'Estados do Documento',
          route: '/estados-documento',
          icon: 'flag',
        },
      ],
    },
    {
      title: 'Organização',
      items: [
        { label: 'Direções', route: '/direcoes', icon: 'account_tree' },
        { label: 'Departamentos', route: '/departamentos', icon: 'business' },
        { label: 'Colaboradores', route: '/colaboradores', icon: 'groups' },
      ],
    },
    {
      title: 'Localização',
      items: [
        { label: 'Países', route: '/paises', icon: 'public' },
        { label: 'Províncias', route: '/provincias', icon: 'map' },
        { label: 'Municípios', route: '/municipios', icon: 'location_city' },
      ],
    },
    {
      title: 'Administração',
      items: [
        { label: 'Utilizadores', route: '/utilizadores', icon: 'manage_accounts' },
        { label: 'Perfis', route: '/perfis', icon: 'admin_panel_settings' },
      ],
    },
  ];

  toggleDrawer(): void {
    this.drawer.toggle();
  }
}
