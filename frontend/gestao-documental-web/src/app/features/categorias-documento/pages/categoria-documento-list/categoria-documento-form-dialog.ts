import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CategoriaDocumentoListItem } from '../../models/categoria-documento.model';

export type CategoriaDocumentoFormDialogData = {
  mode: 'create' | 'edit';
  item?: CategoriaDocumentoListItem;
};

export type CategoriaDocumentoFormDialogResult = {
  codigo: string;
  nome: string;
};

@Component({
  selector: 'app-categoria-documento-form-dialog',
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  template: `
    <h2 mat-dialog-title>
      {{ data.mode === 'create' ? 'Nova categoria' : 'Editar categoria' }}
    </h2>

    <form [formGroup]="form" (ngSubmit)="submit()">
      <mat-dialog-content class="dialog-content">
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Código</mat-label>
          <input matInput formControlName="codigo" maxlength="60" />
          @if (form.controls.codigo.touched && form.controls.codigo.hasError('required')) {
            <mat-error>O código é obrigatório.</mat-error>
          }
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nome</mat-label>
          <input matInput formControlName="nome" maxlength="200" />
          @if (form.controls.nome.touched && form.controls.nome.hasError('required')) {
            <mat-error>O nome é obrigatório.</mat-error>
          }
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" (click)="dialogRef.close()">Cancelar</button>
        <button mat-flat-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data.mode === 'create' ? 'Criar' : 'Guardar' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: `
    .dialog-content {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
      min-width: 320px;
      padding-top: 0.5rem;
    }

    .full-width {
      width: 100%;
    }
  `,
})
export class CategoriaDocumentoFormDialog {
  readonly data = inject<CategoriaDocumentoFormDialogData>(MAT_DIALOG_DATA);
  readonly dialogRef = inject(MatDialogRef<CategoriaDocumentoFormDialog, CategoriaDocumentoFormDialogResult>);

  private readonly fb = inject(FormBuilder);

  readonly form = this.fb.nonNullable.group({
    codigo: [this.data.item?.codigo ?? '', Validators.required],
    nome: [this.data.item?.nome ?? '', Validators.required],
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.dialogRef.close(this.form.getRawValue());
  }
}
