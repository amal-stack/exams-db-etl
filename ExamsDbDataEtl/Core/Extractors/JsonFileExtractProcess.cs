using ExamsDbDataEtl.Abstractions;
using System.Text.Json;

namespace ExamsDbDataEtl.Core.Extractors;

public class JsonFileExtractProcess<TEntity> : IAsyncExtractProcess<TEntity>
{
    public string FilePath { get; }
    public JsonSerializerOptions Options { get; }

    public JsonFileExtractProcess(string filePath, JsonSerializerOptions options)
    {
        FilePath = filePath;
        Options = options;
    }

    public async Task<IList<TEntity>> ExtractAsync()
    {
        var json = await File.ReadAllTextAsync(FilePath);
        return JsonSerializer
            .Deserialize<List<TEntity>>(json, Options)
            ?.ToList()
            ?? throw new InvalidOperationException("Json serialization returned null");
    }
}
