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
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { ConfirmService } from '../../../../shared/services/confirm.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { CategoriaDocumentoListItem } from '../../models/categoria-documento.model';
import { CategoriaDocumentoService } from '../../services/categoria-documento.service';
import {
  CategoriaDocumentoFormDialog,
  CategoriaDocumentoFormDialogData,
  CategoriaDocumentoFormDialogResult,
} from './categoria-documento-form-dialog';

@Component({
  selector: 'app-categoria-documento-list',
  imports: [
    DatePipe,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    MatTooltipModule,
  ],
  templateUrl: './categoria-documento-list.html',
  styleUrl: './categoria-documento-list.css',
})
export class CategoriaDocumentoList implements OnInit {
  private readonly service = inject(CategoriaDocumentoService);
  private readonly dialog = inject(MatDialog);
  private readonly confirmService = inject(ConfirmService);
  private readonly notification = inject(NotificationService);

  readonly loading = signal(false);
  readonly categorias = signal<CategoriaDocumentoListItem[]>([]);
  readonly filteredCategorias = signal<CategoriaDocumentoListItem[]>([]);

  readonly searchControl = new FormControl('', { nonNullable: true });

  readonly displayedColumns = [
    'codigo',
    'nome',
    'dataCriacao',
    'dataAtualizacao',
    'acoes',
    'ativo',
  ];

  ngOnInit(): void {
    this.load();

    this.searchControl.valueChanges
      .pipe(debounceTime(250), distinctUntilChanged())
      .subscribe((term) => this.applyFilter(term));
  }

  load(): void {
    this.loading.set(true);

    this.service.getAll().subscribe({
        next: (items) => {
          this.loading.set(false);
          this.categorias.set(items);
          this.applyFilter(this.searchControl.value);
        },
        error: (error) => {
          this.loading.set(false);
          this.showError(this.resolveApiError(error));
        },
      });
  }

  openCreateDialog(): void {
    this.openFormDialog({ mode: 'create' });
  }

  openEditDialog(item: CategoriaDocumentoListItem): void {
    this.openFormDialog({ mode: 'edit', item });
  }

  onToggleAtivo(item: CategoriaDocumentoListItem, ativo: boolean): void {
    if (item.ativo === ativo) {
      return;
    }

    const action = ativo ? 'activar' : 'desativar';

    this.confirmService
      .open({
        title: ativo ? 'Activar categoria' : 'Desativar categoria',
        message: `Deseja ${action} a categoria "${item.nome}"?`,
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

        this.service
          .update(item.id, { codigo: item.codigo, nome: item.nome, ativo })
          .subscribe({
            next: () => {
              this.loading.set(false);
              this.notification.success(
                ativo ? 'Categoria activada com sucesso.' : 'Categoria desativada com sucesso.',
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

  private openFormDialog(data: CategoriaDocumentoFormDialogData): void {
    const dialogRef = this.dialog.open<
      CategoriaDocumentoFormDialog,
      CategoriaDocumentoFormDialogData,
      CategoriaDocumentoFormDialogResult
    >(CategoriaDocumentoFormDialog, {
      width: '420px',
      data,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }

      if (data.mode === 'create') {
        this.create(result);
      } else if (data.item) {
        this.update(data.item.id, result, data.item.ativo);
      }
    });
  }

  private create(payload: CategoriaDocumentoFormDialogResult): void {
    this.loading.set(true);

    this.service.create(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.notification.success('Categoria criada com sucesso.');
        this.load();
      },
      error: (error) => {
        this.loading.set(false);
        this.showError(this.resolveApiError(error));
      },
    });
  }

  private update(
    id: number,
    payload: CategoriaDocumentoFormDialogResult,
    ativo: boolean,
  ): void {
    this.loading.set(true);

    this.service.update(id, { ...payload, ativo }).subscribe({
      next: () => {
        this.loading.set(false);
        this.notification.success('Categoria actualizada com sucesso.');
        this.load();
      },
      error: (error) => {
        this.loading.set(false);
        this.showError(this.resolveApiError(error));
      },
    });
  }

  private applyFilter(term: string): void {
    const normalized = term.trim().toLowerCase();

    if (!normalized) {
      this.filteredCategorias.set(this.categorias());
      return;
    }

    this.filteredCategorias.set(
      this.categorias().filter(
        (item) =>
          item.codigo.toLowerCase().includes(normalized) ||
          item.nome.toLowerCase().includes(normalized),
      ),
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

    if (body?.errors?.length) {
      return body.errors.join(' ');
    }

    if (body?.message) {
      return body.message;
    }

    return 'Ocorreu um erro ao processar o pedido.';
  }
}
