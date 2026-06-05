import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NotificationSnackbar } from '../ui/notification-snackbar/notification-snackbar';
import { NotificationType } from '../ui/notification-snackbar/notification-snackbar.models';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private readonly snackBar = inject(MatSnackBar);

  success(message: string, durationMs = 4000): void {
    this.show('success', message, durationMs);
  }

  error(message: string, durationMs = 6000): void {
    this.show('error', message, durationMs);
  }

  info(message: string, durationMs = 4000): void {
    this.show('info', message, durationMs);
  }

  warning(message: string, durationMs = 5000): void {
    this.show('warning', message, durationMs);
  }

  private show(type: NotificationType, message: string, durationMs: number): void {
    this.snackBar.openFromComponent(NotificationSnackbar, {
      duration: durationMs,
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: ['app-notification-panel', `app-notification-panel--${type}`],
      data: { type, message },
    });
  }
}
