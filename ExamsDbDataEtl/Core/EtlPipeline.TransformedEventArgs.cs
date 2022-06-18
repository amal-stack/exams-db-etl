namespace ExamsDbDataEtl.Core;


public partial class EtlPipeline<TExtracted, TTransformed>
{
    public class TransformedEventArgs : EventArgs
    {
        public IList<TTransformed> Result { get; }

        public TransformedEventArgs(IList<TTransformed> result)
        {
            Result = result;
        }
    }
}
