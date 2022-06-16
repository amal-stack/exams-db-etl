namespace ExamsDbDataEtl.Abstractions;

public interface ILoadProcess<TEntity>
{
    Task LoadAsync(IEnumerable<TEntity> entities);
}
