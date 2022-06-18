namespace ExamsDbDataEtl.Core;

public static class EtlPipelineConsoleHandlerExtensions
{
    public static void RegisterConsoleLoggers<TExtracted, TTransformed>(
        this EtlPipeline<TExtracted, TTransformed> pipeline)
    {
        pipeline.Extracted += ConsoleEtlHandlers.EtlPipeline_Extracted;
        pipeline.Transformed += ConsoleEtlHandlers.EtlPipeline_Transformed;
        pipeline.Loaded += ConsoleEtlHandlers.EtlPipeline_Loaded;
    }
}
