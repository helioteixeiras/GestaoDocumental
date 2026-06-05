import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CategoriaDocumentoListItem } from '../../../categorias-documento/models/categoria-documento.model';
import { TipoDocumentoListItem } from '../../models/tipo-documento.model';

export type TipoDocumentoFormDialogData = {
  mode: 'create' | 'edit';
  item?: TipoDocumentoListItem;
  categorias: CategoriaDocumentoListItem[];
};

export type TipoDocumentoFormDialogResult = {
  codigo: string;
  nome: string;
  categoriaDocumentoId: number;
};

@Component({
  selector: 'app-tipo-documento-form-dialog',
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
  ],
  templateUrl: './tipo-documento-form-dialog.html',
  styleUrl: './tipo-documento-form-dialog.css',
})
export class TipoDocumentoFormDialog {
  readonly data = inject<TipoDocumentoFormDialogData>(MAT_DIALOG_DATA);
  readonly dialogRef = inject(MatDialogRef<TipoDocumentoFormDialog, TipoDocumentoFormDialogResult>);

  private readonly fb = inject(FormBuilder);

  readonly categorias = this.data.categorias;

  readonly form = this.fb.nonNullable.group({
    codigo: [this.data.item?.codigo ?? '', Validators.required],
    nome: [this.data.item?.nome ?? '', Validators.required],
    categoriaDocumentoId: [this.data.item?.categoriaDocumentoId ?? 0, Validators.min(1)],
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.dialogRef.close(this.form.getRawValue());
  }
}
