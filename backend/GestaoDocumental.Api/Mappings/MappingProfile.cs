using AutoMapper;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.ClassificacaoDocumento;
using GestaoDocumental.Api.DTOs.Colaborador;
using GestaoDocumental.Api.DTOs.Departamento;
using GestaoDocumental.Api.DTOs.Direcao;
using GestaoDocumental.Api.DTOs.Documento;
using GestaoDocumental.Api.DTOs.DocumentoAnexo;
using GestaoDocumental.Api.DTOs.DocumentoHistorico;
using GestaoDocumental.Api.DTOs.EstadoColaborador;
using GestaoDocumental.Api.DTOs.EstadoDocumento;
using GestaoDocumental.Api.DTOs.EstadoLogin;
using GestaoDocumental.Api.DTOs.Fornecedor;
using GestaoDocumental.Api.DTOs.Genero;
using GestaoDocumental.Api.DTOs.Municipio;
using GestaoDocumental.Api.DTOs.Pais;
using GestaoDocumental.Api.DTOs.Perfil;
using GestaoDocumental.Api.DTOs.PostoTrabalho;
using GestaoDocumental.Api.DTOs.Provincia;
using GestaoDocumental.Api.DTOs.TipoDocumento;
using GestaoDocumental.Api.DTOs.TipoDocumentoColaborador;
using GestaoDocumental.Api.DTOs.TramitacaoDocumento;
using GestaoDocumental.Api.DTOs.UsuarioSistema;

namespace GestaoDocumental.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ConfigureClassificacaoDocumentoMaps();
        ConfigureColaboradorMaps();
        ConfigureDepartamentoMaps();
        ConfigureDirecaoMaps();
        ConfigureDocumentoMaps();
        ConfigureDocumentoAnexoMaps();
        ConfigureDocumentoHistoricoMaps();
        ConfigureEstadoColaboradorMaps();
        ConfigureEstadoDocumentoMaps();
        ConfigureEstadoLoginMaps();
        ConfigureFornecedorMaps();
        ConfigureGeneroMaps();
        ConfigureMunicipioMaps();
        ConfigurePaisMaps();
        ConfigurePerfilMaps();
        ConfigurePostoTrabalhoMaps();
        ConfigureProvinciaMaps();
        ConfigureTipoDocumentoMaps();
        ConfigureTipoDocumentoColaboradorMaps();
        ConfigureTramitacaoDocumentoMaps();
        ConfigureUsuarioSistemaMaps();
    }

    private void ConfigureClassificacaoDocumentoMaps()
    {
        CreateMap<ClassificacaoDocumento, ClassificacaoDocumentoListDto>();
        CreateMap<ClassificacaoDocumento, ClassificacaoDocumentoDetailsDto>();

        CreateMap<ClassificacaoDocumentoCreateDto, ClassificacaoDocumento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore());

        CreateMap<ClassificacaoDocumentoUpdateDto, ClassificacaoDocumento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureColaboradorMaps()
    {
        CreateMap<Colaborador, ColaboradorListDto>();
        CreateMap<Colaborador, ColaboradorDetailsDto>();

        CreateMap<ColaboradorCreateDto, Colaborador>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoColaboradorCriadors, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoHistoricos, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoUtilizadorAtualizacaos, opt => opt.Ignore())
            .ForMember(dest => dest.Estado, opt => opt.Ignore())
            .ForMember(dest => dest.Genero, opt => opt.Ignore())
            .ForMember(dest => dest.Nacionalidade, opt => opt.Ignore())
            .ForMember(dest => dest.Perfil, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalho, opt => opt.Ignore())
            .ForMember(dest => dest.TipoDocumentoColaborador, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoColaboradorDestinos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoColaboradorOrigems, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioSistema, opt => opt.Ignore());

        CreateMap<ColaboradorUpdateDto, Colaborador>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoColaboradorCriadors, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoHistoricos, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoUtilizadorAtualizacaos, opt => opt.Ignore())
            .ForMember(dest => dest.Estado, opt => opt.Ignore())
            .ForMember(dest => dest.Genero, opt => opt.Ignore())
            .ForMember(dest => dest.Nacionalidade, opt => opt.Ignore())
            .ForMember(dest => dest.Perfil, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalho, opt => opt.Ignore())
            .ForMember(dest => dest.TipoDocumentoColaborador, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoColaboradorDestinos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoColaboradorOrigems, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioSistema, opt => opt.Ignore());
    }

    private void ConfigureDepartamentoMaps()
    {
        CreateMap<Departamento, DepartamentoListDto>();
        CreateMap<Departamento, DepartamentoDetailsDto>();

        CreateMap<DepartamentoCreateDto, Departamento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Direcao, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalhos, opt => opt.Ignore());

        CreateMap<DepartamentoUpdateDto, Departamento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Direcao, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalhos, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureDirecaoMaps()
    {
        CreateMap<Direcao, DirecaoListDto>();
        CreateMap<Direcao, DirecaoDetailsDto>();

        CreateMap<DirecaoCreateDto, Direcao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Departamentos, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoDirecaoDestinos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoDirecaoOrigems, opt => opt.Ignore());

        CreateMap<DirecaoUpdateDto, Direcao>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Departamentos, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoDirecaoDestinos, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentoDirecaoOrigems, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureDocumentoMaps()
    {
        CreateMap<Documento, DocumentoListDto>();
        CreateMap<Documento, DocumentoDetailsDto>();

        CreateMap<DocumentoCreateDto, Documento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.VersaoAtual, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorAtualizacaoId, opt => opt.Ignore())
            .ForMember(dest => dest.Classificacao, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorCriador, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoAnexos, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoHistoricos, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoDocumento, opt => opt.Ignore())
            .ForMember(dest => dest.Fornecedor, opt => opt.Ignore())
            .ForMember(dest => dest.TipoDocumento, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentos, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorAtualizacao, opt => opt.Ignore());

        CreateMap<DocumentoUpdateDto, Documento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorCriadorId, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.VersaoAtual, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorAtualizacaoId, opt => opt.Ignore())
            .ForMember(dest => dest.Classificacao, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorCriador, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoAnexos, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoHistoricos, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoDocumento, opt => opt.Ignore())
            .ForMember(dest => dest.Fornecedor, opt => opt.Ignore())
            .ForMember(dest => dest.TipoDocumento, opt => opt.Ignore())
            .ForMember(dest => dest.TramitacaoDocumentos, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorAtualizacao, opt => opt.Ignore());
    }

    private void ConfigureDocumentoAnexoMaps()
    {
        CreateMap<DocumentoAnexo, DocumentoAnexoListDto>();
        CreateMap<DocumentoAnexo, DocumentoAnexoDetailsDto>();

        CreateMap<DocumentoAnexoCreateDto, DocumentoAnexo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GuidFicheiro, opt => opt.Ignore())
            .ForMember(dest => dest.NomeFisico, opt => opt.Ignore())
            .ForMember(dest => dest.Caminho, opt => opt.Ignore())
            .ForMember(dest => dest.HashSha256, opt => opt.Ignore())
            .ForMember(dest => dest.DataUpload, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore());

        CreateMap<DocumentoAnexoUpdateDto, DocumentoAnexo>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoId, opt => opt.Ignore())
            .ForMember(dest => dest.GuidFicheiro, opt => opt.Ignore())
            .ForMember(dest => dest.NomeFisico, opt => opt.Ignore())
            .ForMember(dest => dest.Caminho, opt => opt.Ignore())
            .ForMember(dest => dest.HashSha256, opt => opt.Ignore())
            .ForMember(dest => dest.Tamanho, opt => opt.Ignore())
            .ForMember(dest => dest.DataUpload, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore());
    }

    private void ConfigureDocumentoHistoricoMaps()
    {
        CreateMap<DocumentoHistorico, DocumentoHistoricoListDto>();
        CreateMap<DocumentoHistorico, DocumentoHistoricoDetailsDto>();

        CreateMap<DocumentoHistoricoCreateDto, DocumentoHistorico>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorId, opt => opt.Ignore())
            .ForMember(dest => dest.DataAcao, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore())
            .ForMember(dest => dest.Utilizador, opt => opt.Ignore());

        CreateMap<DocumentoHistoricoUpdateDto, DocumentoHistorico>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoId, opt => opt.Ignore())
            .ForMember(dest => dest.UtilizadorId, opt => opt.Ignore())
            .ForMember(dest => dest.DataAcao, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore())
            .ForMember(dest => dest.Utilizador, opt => opt.Ignore());
    }

    private void ConfigureEstadoColaboradorMaps()
    {
        CreateMap<EstadoColaborador, EstadoColaboradorListDto>();
        CreateMap<EstadoColaborador, EstadoColaboradorDetailsDto>();

        CreateMap<EstadoColaboradorCreateDto, EstadoColaborador>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore());

        CreateMap<EstadoColaboradorUpdateDto, EstadoColaborador>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureEstadoDocumentoMaps()
    {
        CreateMap<EstadoDocumento, EstadoDocumentoListDto>();
        CreateMap<EstadoDocumento, EstadoDocumentoDetailsDto>();

        CreateMap<EstadoDocumentoCreateDto, EstadoDocumento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore());

        CreateMap<EstadoDocumentoUpdateDto, EstadoDocumento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureEstadoLoginMaps()
    {
        CreateMap<EstadoLogin, EstadoLoginListDto>();
        CreateMap<EstadoLogin, EstadoLoginDetailsDto>();

        CreateMap<EstadoLoginCreateDto, EstadoLogin>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioSistemas, opt => opt.Ignore());

        CreateMap<EstadoLoginUpdateDto, EstadoLogin>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioSistemas, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureFornecedorMaps()
    {
        CreateMap<Fornecedor, FornecedorListDto>();
        CreateMap<Fornecedor, FornecedorDetailsDto>();

        CreateMap<FornecedorCreateDto, Fornecedor>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore());

        CreateMap<FornecedorUpdateDto, Fornecedor>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureGeneroMaps()
    {
        CreateMap<Genero, GeneroListDto>();
        CreateMap<Genero, GeneroDetailsDto>();

        CreateMap<GeneroCreateDto, Genero>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore());

        CreateMap<GeneroUpdateDto, Genero>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureMunicipioMaps()
    {
        CreateMap<Municipio, MunicipioListDto>();
        CreateMap<Municipio, MunicipioDetailsDto>();

        CreateMap<MunicipioCreateDto, Municipio>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalhos, opt => opt.Ignore())
            .ForMember(dest => dest.Provincia, opt => opt.Ignore());

        CreateMap<MunicipioUpdateDto, Municipio>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PostoTrabalhos, opt => opt.Ignore())
            .ForMember(dest => dest.Provincia, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigurePaisMaps()
    {
        CreateMap<Pais, PaisListDto>();
        CreateMap<Pais, PaisDetailsDto>();

        CreateMap<PaisCreateDto, Pais>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ForMember(dest => dest.Provincia, opt => opt.Ignore());

        CreateMap<PaisUpdateDto, Pais>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ForMember(dest => dest.Provincia, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigurePerfilMaps()
    {
        CreateMap<Perfil, PerfilListDto>();
        CreateMap<Perfil, PerfilDetailsDto>();

        CreateMap<PerfilCreateDto, Perfil>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore());

        CreateMap<PerfilUpdateDto, Perfil>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigurePostoTrabalhoMaps()
    {
        CreateMap<PostoTrabalho, PostoTrabalhoListDto>();
        CreateMap<PostoTrabalho, PostoTrabalhoDetailsDto>();

        CreateMap<PostoTrabalhoCreateDto, PostoTrabalho>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ForMember(dest => dest.Departamento, opt => opt.Ignore())
            .ForMember(dest => dest.Municipio, opt => opt.Ignore());

        CreateMap<PostoTrabalhoUpdateDto, PostoTrabalho>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ForMember(dest => dest.Departamento, opt => opt.Ignore())
            .ForMember(dest => dest.Municipio, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureProvinciaMaps()
    {
        CreateMap<Provincia, ProvinciaListDto>();
        CreateMap<Provincia, ProvinciaDetailsDto>();

        CreateMap<ProvinciaCreateDto, Provincia>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Municipios, opt => opt.Ignore())
            .ForMember(dest => dest.Pais, opt => opt.Ignore());

        CreateMap<ProvinciaUpdateDto, Provincia>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Municipios, opt => opt.Ignore())
            .ForMember(dest => dest.Pais, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureTipoDocumentoMaps()
    {
        CreateMap<TipoDocumento, TipoDocumentoListDto>();
        CreateMap<TipoDocumento, TipoDocumentoDetailsDto>();

        CreateMap<TipoDocumentoCreateDto, TipoDocumento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore());

        CreateMap<TipoDocumentoUpdateDto, TipoDocumento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Documentos, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureTipoDocumentoColaboradorMaps()
    {
        CreateMap<TipoDocumentoColaborador, TipoDocumentoColaboradorListDto>();
        CreateMap<TipoDocumentoColaborador, TipoDocumentoColaboradorDetailsDto>();

        CreateMap<TipoDocumentoColaboradorCreateDto, TipoDocumentoColaborador>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore());

        CreateMap<TipoDocumentoColaboradorUpdateDto, TipoDocumentoColaborador>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Colaboradors, opt => opt.Ignore())
            .ReverseMap();
    }

    private void ConfigureTramitacaoDocumentoMaps()
    {
        CreateMap<TramitacaoDocumento, TramitacaoDocumentoListDto>();
        CreateMap<TramitacaoDocumento, TramitacaoDocumentoDetailsDto>();

        CreateMap<TramitacaoDocumentoCreateDto, TramitacaoDocumento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataEnvio, opt => opt.Ignore())
            .ForMember(dest => dest.DataRececao, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorDestino, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoDestino, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore());

        CreateMap<TramitacaoDocumentoUpdateDto, TramitacaoDocumento>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentoId, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoOrigemId, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorOrigemId, opt => opt.Ignore())
            .ForMember(dest => dest.DataEnvio, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorDestino, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoDestino, opt => opt.Ignore())
            .ForMember(dest => dest.DirecaoOrigem, opt => opt.Ignore())
            .ForMember(dest => dest.Documento, opt => opt.Ignore());
    }

    private void ConfigureUsuarioSistemaMaps()
    {
        CreateMap<UsuarioSistema, UsuarioSistemaListDto>();
        CreateMap<UsuarioSistema, UsuarioSistemaDetailsDto>();

        CreateMap<UsuarioSistemaCreateDto, UsuarioSistema>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.TentativasFalhadas, opt => opt.Ignore())
            .ForMember(dest => dest.Bloqueado, opt => opt.Ignore())
            .ForMember(dest => dest.UltimoLogin, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.Colaborador, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoLogin, opt => opt.Ignore());

        CreateMap<UsuarioSistemaUpdateDto, UsuarioSistema>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ColaboradorId, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.TentativasFalhadas, opt => opt.Ignore())
            .ForMember(dest => dest.UltimoLogin, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.Colaborador, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoLogin, opt => opt.Ignore());
    }
}
