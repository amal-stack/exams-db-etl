using ExamsDbDataEtl.Abstractions;

namespace ExamsDbDataEtl.Transformers;

public static class TransformProcess
{
    public static IAsyncTransformProcess<T, T> Null<T>() => new NullTransformProcess<T>();

    private class NullTransformProcess<T> : IAsyncTransformProcess<T, T>
    {
        public Task<IList<T>> TransformAsync(IList<T> values) => Task.FromResult(values);
    }
}
