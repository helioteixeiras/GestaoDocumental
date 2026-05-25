namespace GestaoDocumental.Api.DTOs.Colaborador;

public class ColaboradorUpdateDto
{
    public string Nome { get; set; } = string.Empty;

    public int TipoDocumentoColaboradorId { get; set; }

    public string NumDocumento { get; set; } = string.Empty;

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
}
