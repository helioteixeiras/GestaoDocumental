export type NotificationType = 'success' | 'error' | 'info' | 'warning';

export interface NotificationSnackbarData {
  message: string;
  type: NotificationType;
}
