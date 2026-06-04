namespace GestaoDocumental.Api.DTOs.Provincia;

public class ProvinciaDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int PaisId { get; set; }

    public string? Sigla { get; set; }

    public string? CodigoINE { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}
