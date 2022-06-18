using ExamsDbDataEtl.Abstractions;

namespace ExamsDbDataEtl.Core.Extractors;

public class CsvFileExtractProcess<TEntity> : IAsyncExtractProcess<TEntity>
{
    public string FilePath { get; }

    public Func<string[], TEntity> Mapper { get; }

    public char Separator { get; }

    public bool SkipHeaders { get; }

    public CsvFileExtractProcess(
        string filePath,
        Func<string[], TEntity> mapper,
        bool skipHeaders = true,
        char separator = ','
        )
    {
        FilePath = filePath;
        Mapper = mapper;
        Separator = separator;
        SkipHeaders = skipHeaders;
    }

    public async Task<IList<TEntity>> ExtractAsync()
    {
        // Read CSV file
        string[] lines = await File.ReadAllLinesAsync(FilePath);

        Range range = (SkipHeaders ? 1 : 0)..;
        List<TEntity> elements = new();
        foreach (var line in lines[range])
        {
            string[] row = line.Split(
                ',',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);

            elements.Add(Mapper(row));
        }
        return elements;
    }
}
