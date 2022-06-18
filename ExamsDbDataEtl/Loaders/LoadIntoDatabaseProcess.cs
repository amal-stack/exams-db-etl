using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Data;

namespace ExamsDbDataEtl.Loaders;

public class LoadIntoExamDatabaseProcess<TEntity> : IAsyncLoadProcess<TEntity>
    where TEntity : class
{
    public async Task<int> LoadAsync(IEnumerable<TEntity> values)
    {
        using var context = new ExamContext();
        context.Set<TEntity>().AddRange(values);
        return await context.SaveChangesAsync();
    }
}
