import { Component, computed, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ConfirmDialogData, ConfirmDialogTone } from './confirm-dialog.models';

const TONE_ICONS: Record<ConfirmDialogTone, string> = {
  default: 'help_outline',
  warn: 'warning_amber',
  danger: 'error_outline',
};

@Component({
  selector: 'app-confirm-dialog',
  imports: [MatDialogModule, MatButtonModule, MatIconModule],
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.css',
})
export class ConfirmDialog {
  readonly data = inject<ConfirmDialogData>(MAT_DIALOG_DATA);
  private readonly dialogRef = inject(MatDialogRef<ConfirmDialog, boolean>);

  readonly tone = computed(() => this.data.tone ?? 'default');
  readonly icon = computed(() => this.data.icon ?? TONE_ICONS[this.tone()]);
  readonly confirmLabel = computed(() => this.data.confirmLabel ?? 'Confirmar');
  readonly cancelLabel = computed(() => this.data.cancelLabel ?? 'Cancelar');

  confirm(): void {
    this.dialogRef.close(true);
  }

  cancel(): void {
    this.dialogRef.close(false);
  }
}
