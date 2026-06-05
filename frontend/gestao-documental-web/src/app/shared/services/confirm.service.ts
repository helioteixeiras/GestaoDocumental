import { Injectable, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map } from 'rxjs';
import { ConfirmDialog } from '../ui/confirm-dialog/confirm-dialog';
import { ConfirmDialogData } from '../ui/confirm-dialog/confirm-dialog.models';

@Injectable({
  providedIn: 'root',
})
export class ConfirmService {
  private readonly dialog = inject(MatDialog);

  open(data: ConfirmDialogData): Observable<boolean> {
    return this.dialog
      .open(ConfirmDialog, {
        width: '420px',
        maxWidth: '95vw',
        panelClass: 'app-confirm-dialog-panel',
        autoFocus: 'first-tabbable',
        disableClose: true,
        data,
      })
      .afterClosed()
      .pipe(map((result) => result === true));
  }
}
