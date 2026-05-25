namespace GestaoDocumental.Domain.Entities.Legacy;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public DateTime? DataAtualizacao { get; set; }

    public bool Ativo { get; set; } = true;
}