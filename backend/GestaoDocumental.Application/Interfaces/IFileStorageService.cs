using GestaoDocumental.Application.Models;

namespace GestaoDocumental.Application.Interfaces;

public interface IFileStorageService
{
    Task<StoredFileInfo> SaveAsync(
        int documentoId,
        string originalFileName,
        Stream content,
        CancellationToken cancellationToken = default);

    Task<FileDownloadContent?> OpenReadAsync(
        string relativePath,
        string downloadFileName,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(string relativePath, CancellationToken cancellationToken = default);

    Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default);
}
