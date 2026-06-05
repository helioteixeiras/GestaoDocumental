export const AUTH_STORAGE_KEY = 'gestao_documental_auth';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiresAt: string;
  username: string;
  email: string;
  role: string;
}

export interface AuthSession {
  token: string;
  expiresAt: string;
  username: string;
  email: string;
  role: string;
}
