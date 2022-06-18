namespace ExamsDbDataEtl.Abstractions;

public interface ILoadProcess<TEntity>
{
    int LoadAsync(IEnumerable<TEntity> entities);
}