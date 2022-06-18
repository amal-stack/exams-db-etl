using ExamsDbDataEtl.Abstractions;

namespace ExamsDbDataEtl.Core;

/// <summary>
/// A pipeline that asynchronously and sequentially executes an <see cref="IAsyncExtractProcess{TExtracted}"/>, <see cref="IAsyncTransformProcess{TIn, TOut}"/> and an <see cref="IAsyncLoadProcess{TEntity}"/>.
/// </summary>
/// <typeparam name="TExtracted">The type of the extracted entities.</typeparam>
/// <typeparam name="TTransformed">The type of the transformed entities.</typeparam>
public partial class EtlPipeline<TExtracted, TTransformed>
{
    /// <summary>
    /// Occurs when the extraction process completes.
    /// </summary>
    public event EventHandler<ExtractedEventArgs>? Extracted;

    /// <summary>
    /// Occurs when the transformation process completes.
    /// </summary>
    public event EventHandler<TransformedEventArgs>? Transformed;

    /// <summary>
    /// Occurs when the load process completes.
    /// </summary>
    public event EventHandler<LoadedEventArgs>? Loaded;

    public EtlPipeline(
        IAsyncExtractProcess<TExtracted> extractor,
        IAsyncTransformProcess<TExtracted, TTransformed> transformer,
        IAsyncLoadProcess<TTransformed> loader)
    {
        Extractor = extractor;
        Transformer = transformer;
        Loader = loader;
    }

    public IAsyncExtractProcess<TExtracted> Extractor { get; }
    public IAsyncTransformProcess<TExtracted, TTransformed> Transformer { get; }
    public IAsyncLoadProcess<TTransformed> Loader { get; }

    public async Task ExecuteAsync()
    {
        var extracted = await Extractor.ExtractAsync();
        Extracted?.Invoke(this, new(extracted));

        var transformed = await Transformer.TransformAsync(extracted);
        Transformed?.Invoke(this, new(transformed));

        int count = await Loader.LoadAsync(transformed);
        Loaded?.Invoke(this, new LoadedEventArgs(count));
    }
}
