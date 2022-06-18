namespace ExamsDbDataEtl.Abstractions;

public interface IAsyncLoadProcess<TEntity>
{
    Task<int> LoadAsync(IEnumerable<TEntity> entities);
}
