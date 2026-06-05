import { DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { ConfirmService } from '../../../../shared/services/confirm.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { CategoriaDocumentoListItem } from '../../../categorias-documento/models/categoria-documento.model';
import { CategoriaDocumentoService } from '../../../categorias-documento/services/categoria-documento.service';
import {
  TipoDocumentoFormDialog,
  TipoDocumentoFormDialogData,
  TipoDocumentoFormDialogResult,
} from '../../components/tipo-documento-form-dialog/tipo-documento-form-dialog';
import { TipoDocumentoListItem } from '../../models/tipo-documento.model';
import { TipoDocumentoService } from '../../services/tipo-documento.service';

@Component({
  selector: 'app-tipo-documento-list',
  imports: [
    DatePipe,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    MatTooltipModule,
  ],
  templateUrl: './tipo-documento-list.html',
  styleUrl: './tipo-documento-list.css',
})
export class TipoDocumentoList implements OnInit {
  private readonly tipoService = inject(TipoDocumentoService);
  private readonly categoriaService = inject(CategoriaDocumentoService);
  private readonly dialog = inject(MatDialog);
  private readonly confirmService = inject(ConfirmService);
  private readonly notification = inject(NotificationService);

  readonly loading = signal(false);
  readonly tipos = signal<TipoDocumentoListItem[]>([]);
  readonly filteredTipos = signal<TipoDocumentoListItem[]>([]);
  readonly categoriasAtivas = signal<CategoriaDocumentoListItem[]>([]);
  readonly categoriasFiltro = signal<CategoriaDocumentoListItem[]>([]);

  readonly searchControl = new FormControl('', { nonNullable: true });
  readonly categoriaFilterControl = new FormControl<number | null>(null);

  readonly displayedColumns = [
    'codigo',
    'nome',
    'categoria',
    'dataCriacao',
    'acoes',
    'ativo',
  ];

  ngOnInit(): void {
    this.load();

    this.searchControl.valueChanges
      .pipe(debounceTime(250), distinctUntilChanged())
      .subscribe(() => this.applyFilters());

    this.categoriaFilterControl.valueChanges
      .pipe(distinctUntilChanged())
      .subscribe(() => this.applyFilters());
  }

  hasCategoriasAtivas(): boolean {
    return this.categoriasAtivas().length > 0;
  }

  load(): void {
    this.loading.set(true);

    this.categoriaService.getAll().subscribe({
      next: (categorias) => {
        this.categoriasAtivas.set(categorias.filter((c) => c.ativo));
        this.categoriasFiltro.set(
          [...categorias].sort((a, b) => a.nome.localeCompare(b.nome, 'pt')),
        );

        this.tipoService.getAll().subscribe({
          next: (tipos) => {
            this.loading.set(false);
            this.tipos.set(this.enrichTiposWithCategoria(tipos, categorias));
            this.applyFilters();
          },
          error: (error) => {
            this.loading.set(false);
            this.showError(this.resolveApiError(error));
          },
        });
      },
      error: (error) => {
        this.loading.set(false);
        this.showError(this.resolveApiError(error));
      },
    });
  }

  private enrichTiposWithCategoria(
    tipos: TipoDocumentoListItem[],
    categorias: CategoriaDocumentoListItem[],
  ): TipoDocumentoListItem[] {
    const categoriaNomePorId = new Map(categorias.map((c) => [c.id, c.nome]));

    return tipos.map((tipo) => ({
      ...tipo,
      categoriaDocumentoNome:
        tipo.categoriaDocumentoNome?.trim() ||
        categoriaNomePorId.get(tipo.categoriaDocumentoId) ||
        '—',
      ativo: tipo.ativo ?? true,
      dataCriacao: tipo.dataCriacao ?? '',
      dataAtualizacao: tipo.dataAtualizacao ?? null,
    }));
  }

  openCreateDialog(): void {
    if (!this.hasCategoriasAtivas()) {
      this.notification.warning(
        'Não existem categorias activas. Active ou crie uma categoria primeiro.',
      );
      return;
    }

    this.openFormDialog({ mode: 'create' });
  }

  openEditDialog(item: TipoDocumentoListItem): void {
    this.openFormDialog({ mode: 'edit', item });
  }

  onToggleAtivo(item: TipoDocumentoListItem, ativo: boolean): void {
    if (item.ativo === ativo) {
      return;
    }

    const action = ativo ? 'activar' : 'desativar';

    this.confirmService
      .open({
        title: ativo ? 'Activar tipo de documento' : 'Desativar tipo de documento',
        message: `Deseja ${action} o tipo "${item.nome}"?`,
        confirmLabel: ativo ? 'Activar' : 'Desativar',
        cancelLabel: 'Cancelar',
        tone: ativo ? 'default' : 'warn',
      })
      .subscribe((confirmed) => {
        if (!confirmed) {
          this.load();
          return;
        }

        this.loading.set(true);

        this.tipoService
          .update(item.id, {
            codigo: item.codigo,
            nome: item.nome,
            categoriaDocumentoId: item.categoriaDocumentoId,
            ativo,
          })
          .subscribe({
            next: () => {
              this.loading.set(false);
              this.notification.success(
                ativo
                  ? 'Tipo de documento activado com sucesso.'
                  : 'Tipo de documento desactivado com sucesso.',
              );
              this.load();
            },
            error: (error) => {
              this.loading.set(false);
              this.load();
              this.showError(this.resolveApiError(error));
            },
          });
      });
  }

  private openFormDialog(data: Omit<TipoDocumentoFormDialogData, 'categorias'>): void {
    const dialogRef = this.dialog.open<
      TipoDocumentoFormDialog,
      TipoDocumentoFormDialogData,
      TipoDocumentoFormDialogResult
    >(TipoDocumentoFormDialog, {
      width: '440px',
      data: {
        ...data,
        categorias: this.categoriasAtivas(),
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }

      if (data.mode === 'create') {
        this.create(result);
      } else if (data.item) {
        this.persistUpdate(data.item, {
          ...result,
          ativo: data.item.ativo,
        });
      }
    });
  }

  private create(payload: TipoDocumentoFormDialogResult): void {
    this.loading.set(true);

    this.tipoService.create(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.notification.success('Tipo de documento criado com sucesso.');
        this.load();
      },
      error: (error) => {
        this.loading.set(false);
        this.showError(this.resolveApiError(error));
      },
    });
  }

  private persistUpdate(
    item: TipoDocumentoListItem,
    payload: TipoDocumentoFormDialogResult & { ativo: boolean },
  ): void {
    this.loading.set(true);

    this.tipoService.update(item.id, payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.notification.success('Tipo de documento actualizado com sucesso.');
        this.load();
      },
      error: (error) => {
        this.loading.set(false);
        this.load();
        this.showError(this.resolveApiError(error));
      },
    });
  }

  private applyFilters(): void {
    const normalized = this.searchControl.value.trim().toLowerCase();
    const categoriaId = this.categoriaFilterControl.value;

    this.filteredTipos.set(
      this.tipos().filter((item) => {
        const matchesCategoria =
          categoriaId === null || item.categoriaDocumentoId === categoriaId;

        if (!normalized) {
          return matchesCategoria;
        }

        return (
          matchesCategoria &&
          (item.codigo.toLowerCase().includes(normalized) ||
            item.nome.toLowerCase().includes(normalized))
        );
      }),
    );
  }

  private showError(message: string): void {
    this.notification.error(message);
  }

  private resolveApiError(error: unknown): string {
    if (!(error instanceof HttpErrorResponse)) {
      return 'Ocorreu um erro inesperado.';
    }

    if (error.status === 0) {
      return 'Não foi possível contactar a API. Verifique se o backend está a correr.';
    }

    if (error.status === 401) {
      return 'Sessão expirada. Inicie sessão novamente.';
    }

    if (error.status === 403) {
      return 'Não tem permissão para esta operação.';
    }

    if (error.status === 404) {
      return 'Registo não encontrado.';
    }

    const body = error.error as { message?: string; errors?: string[] } | null;
    const combined = [body?.message, ...(body?.errors ?? []), JSON.stringify(body ?? '')]
      .filter(Boolean)
      .join(' ');

    if (combined.includes('UQ_TipoDocumento_Codigo')) {
      return 'Já existe um tipo de documento com este código.';
    }

    if (combined.includes('UQ_TipoDocumento_Nome_Categoria')) {
      return 'Já existe um tipo de documento com este nome nesta categoria.';
    }

    if (
      combined.toLowerCase().includes('categoria') &&
      combined.toLowerCase().includes('obrigat')
    ) {
      return 'A categoria é obrigatória.';
    }

    if (body?.errors?.length) {
      return body.errors.join(' ');
    }

    if (body?.message) {
      return body.message;
    }

    return 'Ocorreu um erro ao processar o pedido.';
  }
}
