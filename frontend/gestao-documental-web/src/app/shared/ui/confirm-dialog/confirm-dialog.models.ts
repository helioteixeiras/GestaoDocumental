export type ConfirmDialogTone = 'default' | 'warn' | 'danger';

export interface ConfirmDialogData {
  title: string;
  message: string;
  confirmLabel?: string;
  cancelLabel?: string;
  tone?: ConfirmDialogTone;
  icon?: string;
}
