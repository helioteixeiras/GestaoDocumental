import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AUTH_STORAGE_KEY,
  AuthSession,
  LoginRequest,
  LoginResponse,
} from './auth.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  private readonly loginUrl = `${environment.apiUrl}/Auth/login`;

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, credentials).pipe(
      tap((response) => this.persistSession(response)),
    );
  }

  isAuthenticated(): boolean {
    const session = this.getSession();
    if (!session?.token) {
      return false;
    }

    const expiresAt = new Date(session.expiresAt);
    if (Number.isNaN(expiresAt.getTime()) || expiresAt <= new Date()) {
      this.clearSession();
      return false;
    }

    return true;
  }

  getToken(): string | null {
    return this.getSession()?.token ?? null;
  }

  getSession(): AuthSession | null {
    const raw = localStorage.getItem(AUTH_STORAGE_KEY);
    if (!raw) {
      return null;
    }

    try {
      return JSON.parse(raw) as AuthSession;
    } catch {
      this.clearSession();
      return null;
    }
  }

  logout(navigateToLogin = true): void {
    this.clearSession();

    if (navigateToLogin) {
      void this.router.navigate(['/login']);
    }
  }

  private persistSession(response: LoginResponse): void {
    const session: AuthSession = {
      token: response.token,
      expiresAt: response.expiresAt,
      username: response.username,
      email: response.email,
      role: response.role,
    };

    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(session));
  }

  private clearSession(): void {
    localStorage.removeItem(AUTH_STORAGE_KEY);
  }
}
