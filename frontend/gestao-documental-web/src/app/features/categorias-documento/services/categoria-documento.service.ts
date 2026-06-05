import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {
  CategoriaDocumento,
  CategoriaDocumentoCreate,
  CategoriaDocumentoListItem,
  CategoriaDocumentoUpdate,
} from '../models/categoria-documento.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriaDocumentoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/CategoriaDocumento`;

  getAll(): Observable<CategoriaDocumentoListItem[]> {
    return this.http.get<CategoriaDocumentoListItem[]>(this.baseUrl);
  }

  getById(id: number): Observable<CategoriaDocumento> {
    return this.http.get<CategoriaDocumento>(`${this.baseUrl}/${id}`);
  }

  create(payload: CategoriaDocumentoCreate): Observable<CategoriaDocumento> {
    return this.http.post<CategoriaDocumento>(this.baseUrl, payload);
  }

  update(id: number, payload: CategoriaDocumentoUpdate): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
