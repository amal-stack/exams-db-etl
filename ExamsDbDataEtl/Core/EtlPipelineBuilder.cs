using ExamsDbDataEtl.Abstractions;

namespace ExamsDbDataEtl.Core;

public class EtlPipelineBuilder<TExtracted, TTransformed>
{
    private IAsyncExtractProcess<TExtracted>? Extractor { get; set; }
    private IAsyncTransformProcess<TExtracted, TTransformed>? Transformer { get; set; }
    private IAsyncLoadProcess<TTransformed>? Loader { get; set; }

    public EtlPipelineBuilder<TExtracted, TTransformed> AddExtractor(IAsyncExtractProcess<TExtracted> extractor)
    {
        Extractor = extractor;
        return this;
    }

    public EtlPipelineBuilder<TExtracted, TTransformed> AddTransformer(IAsyncTransformProcess<TExtracted, TTransformed> transformer)
    {
        Transformer = transformer;
        return this;
    }

    public EtlPipelineBuilder<TExtracted, TTransformed> AddLoader(IAsyncLoadProcess<TTransformed> loader)
    {
        Loader = loader;
        return this;
    }

    public EtlPipeline<TExtracted, TTransformed> Build()
    {
        return new EtlPipeline<TExtracted, TTransformed>(
            Extractor ?? throw new InvalidOperationException("Extractor is null"),
            Transformer ?? throw new InvalidOperationException("Transformer is null"),
            Loader ?? throw new InvalidOperationException("Loader is null"));
    }
}
