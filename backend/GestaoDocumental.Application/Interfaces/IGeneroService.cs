using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IGeneroService
{
    Task<IReadOnlyList<Genero>> GetAllAsync();
    Task<Genero?> GetByIdAsync(int id);
    Task<Genero> CreateAsync(Genero entity);
    Task<bool> UpdateAsync(int id, Genero entity);
    Task<bool> DeleteAsync(int id);
}
