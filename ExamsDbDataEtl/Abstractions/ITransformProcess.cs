namespace ExamsDbDataEtl.Abstractions;

public interface ITransformProcess<TIn, TOut>
{
    IList<TOut> Transform(IList<TIn> values);
}

