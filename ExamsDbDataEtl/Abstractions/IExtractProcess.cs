namespace ExamsDbDataEtl.Abstractions;

public interface IExtractProcess<TEntity>
{
    IList<TEntity> Extract();
}
