import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from './auth.service';

const LOGIN_PATH_SEGMENT = '/Auth/login';

function isLoginRequest(url: string): boolean {
  return url.includes(LOGIN_PATH_SEGMENT);
}

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  let request = req;

  if (!isLoginRequest(req.url)) {
    const token = authService.getToken();

    if (token) {
      request = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }
  }

  return next(request).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !isLoginRequest(req.url)) {
        authService.logout(false);
        void router.navigate(['/login']);
      }

      return throwError(() => error);
    }),
  );
};
