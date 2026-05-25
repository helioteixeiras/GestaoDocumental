using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Colaborador : BaseEntity
{
    public string Nome { get; set; } = null!;

    public int TipoDocumentoColaboradorId { get; set; }

    public string NumDocumento { get; set; } = null!;

    public string? NumMecanografo { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string? Endereco { get; set; }

    public int? NacionalidadeId { get; set; }

    public string? Email { get; set; }

    public int PostoTrabalhoId { get; set; }

    public int? GeneroId { get; set; }

    public string? Cargo { get; set; }

    public int EstadoId { get; set; }

    public int PerfilId { get; set; }

    public virtual ICollection<Documento> DocumentoColaboradorCriadors { get; set; } = new List<Documento>();

    public virtual ICollection<DocumentoHistorico> DocumentoHistoricos { get; set; } = new List<DocumentoHistorico>();

    public virtual ICollection<Documento> DocumentoUtilizadorAtualizacaos { get; set; } = new List<Documento>();

    public virtual EstadoColaborador Estado { get; set; } = null!;

    public virtual Genero? Genero { get; set; }

    public virtual Pais? Nacionalidade { get; set; }

    public virtual Perfil Perfil { get; set; } = null!;

    public virtual PostoTrabalho PostoTrabalho { get; set; } = null!;

    public virtual TipoDocumentoColaborador TipoDocumentoColaborador { get; set; } = null!;

    public virtual ICollection<TramitacaoDocumento> TramitacaoDocumentoColaboradorDestinos { get; set; } = new List<TramitacaoDocumento>();

    public virtual ICollection<TramitacaoDocumento> TramitacaoDocumentoColaboradorOrigems { get; set; } = new List<TramitacaoDocumento>();

    public virtual UsuarioSistema? UsuarioSistema { get; set; }
}
