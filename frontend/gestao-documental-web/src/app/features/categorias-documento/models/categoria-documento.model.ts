export interface CategoriaDocumentoListItem {
  id: number;
  codigo: string;
  nome: string;
  ativo: boolean;
  dataCriacao: string;
  dataAtualizacao: string | null;
}

export interface CategoriaDocumento {
  id: number;
  codigo: string;
  nome: string;
  ativo: boolean;
  dataCriacao: string;
  dataAtualizacao: string | null;
}

export interface CategoriaDocumentoCreate {
  codigo: string;
  nome: string;
}

export interface CategoriaDocumentoUpdate {
  codigo: string;
  nome: string;
  ativo: boolean;
}
