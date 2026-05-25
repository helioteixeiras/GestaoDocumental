using System.Security.Cryptography;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Application.Models;
using GestaoDocumental.Shared.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GestaoDocumental.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly StorageSettings _settings;
    private readonly string _rootPath;

    public LocalFileStorageService(IOptions<StorageSettings> settings, IHostEnvironment hostEnvironment)
    {
        _settings = settings.Value;
        _rootPath = Path.IsPathRooted(_settings.BasePath)
            ? _settings.BasePath
            : Path.Combine(hostEnvironment.ContentRootPath, _settings.BasePath);
    }

    public async Task<StoredFileInfo> SaveAsync(
        int documentoId,
        string originalFileName,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
        var guid = Guid.NewGuid();
        var physicalFileName = $"{guid}{extension}";
        var documentDirectory = Path.Combine(_rootPath, documentoId.ToString());
        Directory.CreateDirectory(documentDirectory);

        var fullPath = Path.Combine(documentDirectory, physicalFileName);
        var relativePath = Path.Combine(documentoId.ToString(), physicalFileName)
            .Replace('\\', '/');

        await using var fileStream = new FileStream(
            fullPath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None);

        using var sha256 = SHA256.Create();
        var buffer = new byte[81920];
        int bytesRead;
        long totalBytes = 0;

        while ((bytesRead = await content.ReadAsync(buffer, cancellationToken)) > 0)
        {
            sha256.TransformBlock(buffer, 0, bytesRead, null, 0);
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            totalBytes += bytesRead;
        }

        sha256.TransformFinalBlock([], 0, 0);

        return new StoredFileInfo
        {
            RelativePath = relativePath,
            PhysicalFileName = physicalFileName,
            HashSha256 = Convert.ToHexString(sha256.Hash!),
            Size = totalBytes
        };
    }

    public Task<FileDownloadContent?> OpenReadAsync(
        string relativePath,
        string downloadFileName,
        CancellationToken cancellationToken = default)
    {
        var fullPath = GetFullPath(relativePath);

        if (!File.Exists(fullPath))
        {
            return Task.FromResult<FileDownloadContent?>(null);
        }

        var fileInfo = new FileInfo(fullPath);
        var extension = Path.GetExtension(downloadFileName);

        var result = new FileDownloadContent
        {
            FileName = downloadFileName,
            ContentType = ResolveContentType(extension),
            Size = fileInfo.Length,
            Content = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read)
        };

        return Task.FromResult<FileDownloadContent?>(result);
    }

    public Task<bool> ExistsAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(File.Exists(GetFullPath(relativePath)));
    }

    public Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var fullPath = GetFullPath(relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    private string GetFullPath(string relativePath)
    {
        var normalized = relativePath.Replace('/', Path.DirectorySeparatorChar);
        return Path.GetFullPath(Path.Combine(_rootPath, normalized));
    }

    private static string ResolveContentType(string extension) =>
        extension.ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            _ => "application/octet-stream"
        };
}
