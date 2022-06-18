namespace ExamsDbDataEtl.Core;


public partial class EtlPipeline<TExtracted, TTransformed>
{
    public class ExtractedEventArgs : EventArgs
    {
        public IList<TExtracted> Result { get; }

        public ExtractedEventArgs(IList<TExtracted> result)
        {
            Result = result;
        }
    }
}
