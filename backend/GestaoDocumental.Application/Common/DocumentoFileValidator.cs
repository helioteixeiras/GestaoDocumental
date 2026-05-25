using FluentValidation;
using FluentValidation.Results;
using GestaoDocumental.Shared.Settings;

namespace GestaoDocumental.Application.Common;

public static class DocumentoFileValidator
{
    public const string FileFieldName = "file";

    public static void Validate(string? fileName, long length, StorageSettings settings)
    {
        var failures = new List<ValidationFailure>();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            failures.Add(new ValidationFailure(FileFieldName, "Ficheiro é obrigatório."));
        }

        if (length <= 0)
        {
            failures.Add(new ValidationFailure(FileFieldName, "Ficheiro vazio não é permitido."));
        }

        var maxBytes = settings.MaxFileSizeMb * 1024L * 1024L;
        if (length > maxBytes)
        {
            failures.Add(new ValidationFailure(
                FileFieldName,
                $"Ficheiro excede o tamanho máximo de {settings.MaxFileSizeMb} MB."));
        }

        if (!string.IsNullOrWhiteSpace(fileName))
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var allowed = settings.AllowedExtensions
                .Select(item => item.ToLowerInvariant())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(extension) || !allowed.Contains(extension))
            {
                failures.Add(new ValidationFailure(
                    FileFieldName,
                    "Extensão de ficheiro não permitida."));
            }
        }

        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }
    }
}
