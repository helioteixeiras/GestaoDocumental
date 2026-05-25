using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IPostoTrabalhoService
{
    Task<IReadOnlyList<PostoTrabalho>> GetAllAsync();
    Task<PostoTrabalho?> GetByIdAsync(int id);
    Task<PostoTrabalho> CreateAsync(PostoTrabalho entity);
    Task<bool> UpdateAsync(int id, PostoTrabalho entity);
    Task<bool> DeleteAsync(int id);
}
