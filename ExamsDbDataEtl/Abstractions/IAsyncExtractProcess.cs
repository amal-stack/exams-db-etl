namespace ExamsDbDataEtl.Abstractions;

public interface IAsyncExtractProcess<TEntity>
{
    Task<IList<TEntity>> ExtractAsync();
}
