namespace ExamsDbDataEtl.Abstractions;

public interface IAsyncTransformProcess<TIn, TOut>
{
    Task<IList<TOut>> TransformAsync(IList<TIn> values);
}

