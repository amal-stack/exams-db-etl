namespace ExamsDbDataEtl.Core;

public static class ConsoleEtlHandlers
{
    public static void EtlPipeline_Extracted<TExtracted, TTransformed>(
        object? obj,
        EtlPipeline<TExtracted, TTransformed>.ExtractedEventArgs args)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{args.Result.Count} objects of type {typeof(TExtracted).Name} extracted.");
        Console.ForegroundColor = color;
    }

    public static void EtlPipeline_Transformed<TExtracted, TTransformed>(
        object? obj,
        EtlPipeline<TExtracted, TTransformed>.TransformedEventArgs args)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{args.Result.Count} objects of type {typeof(TTransformed).Name} transformed.");
        Console.ForegroundColor = color;
    }

    public static void EtlPipeline_Loaded<TExtracted, TTransformed>(
        object? obj,
        EtlPipeline<TExtracted, TTransformed>.LoadedEventArgs args)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Successfully Loaded {args.Count} entries. Type: {typeof(TTransformed).Name}.");
        Console.ForegroundColor = color;
    }


}
