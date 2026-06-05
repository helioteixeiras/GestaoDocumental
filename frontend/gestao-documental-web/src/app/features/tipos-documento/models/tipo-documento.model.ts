export interface TipoDocumentoListItem {
  id: number;
  codigo: string;
  nome: string;
  categoriaDocumentoId: number;
  categoriaDocumentoNome: string;
  ativo: boolean;
  dataCriacao: string;
  dataAtualizacao: string | null;
}

export interface TipoDocumento {
  id: number;
  codigo: string;
  nome: string;
  categoriaDocumentoId: number;
  ativo: boolean;
  dataCriacao: string;
  dataAtualizacao: string | null;
}

export interface TipoDocumentoCreate {
  codigo: string;
  nome: string;
  categoriaDocumentoId: number;
}

export interface TipoDocumentoUpdate {
  codigo: string;
  nome: string;
  categoriaDocumentoId: number;
  ativo: boolean;
}
