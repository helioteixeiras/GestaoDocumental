import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {
  TipoDocumento,
  TipoDocumentoCreate,
  TipoDocumentoListItem,
  TipoDocumentoUpdate,
} from '../models/tipo-documento.model';

@Injectable({
  providedIn: 'root',
})
export class TipoDocumentoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/TipoDocumento`;

  getAll(): Observable<TipoDocumentoListItem[]> {
    return this.http.get<TipoDocumentoListItem[]>(this.baseUrl);
  }

  getById(id: number): Observable<TipoDocumento> {
    return this.http.get<TipoDocumento>(`${this.baseUrl}/${id}`);
  }

  create(payload: TipoDocumentoCreate): Observable<TipoDocumento> {
    return this.http.post<TipoDocumento>(this.baseUrl, payload);
  }

  update(id: number, payload: TipoDocumentoUpdate): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
