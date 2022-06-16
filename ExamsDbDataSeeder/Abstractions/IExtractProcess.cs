namespace ExamsDbDataEtl.Abstractions;

public interface IExtractProcess<TEntity>
{
    Task<IList<TEntity>> ExtractAsync();
}
