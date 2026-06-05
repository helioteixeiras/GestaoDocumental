import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MAT_SNACK_BAR_DATA, MatSnackBarRef } from '@angular/material/snack-bar';
import {
  NotificationSnackbarData,
  NotificationType,
} from './notification-snackbar.models';

const TYPE_ICONS: Record<NotificationType, string> = {
  success: 'check_circle',
  error: 'error',
  info: 'info',
  warning: 'warning_amber',
};

@Component({
  selector: 'app-notification-snackbar',
  imports: [MatIconModule, MatButtonModule],
  templateUrl: './notification-snackbar.html',
  styleUrl: './notification-snackbar.css',
})
export class NotificationSnackbar {
  readonly data = inject<NotificationSnackbarData>(MAT_SNACK_BAR_DATA);
  private readonly snackBarRef = inject(MatSnackBarRef<NotificationSnackbar>);

  readonly icon = TYPE_ICONS[this.data.type];

  dismiss(): void {
    this.snackBarRef.dismiss();
  }
}
