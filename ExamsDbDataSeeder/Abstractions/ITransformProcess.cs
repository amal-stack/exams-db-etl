namespace ExamsDbDataEtl.Abstractions;

public interface ITransformProcess<TIn, TOut>
{
    Task<IList<TOut>> TransformAsync(IList<TIn> values);
}
