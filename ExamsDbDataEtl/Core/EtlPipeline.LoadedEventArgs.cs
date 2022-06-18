namespace ExamsDbDataEtl.Core;


public partial class EtlPipeline<TExtracted, TTransformed>
{
    public class LoadedEventArgs : EventArgs
    {
        public int Count { get; }

        public LoadedEventArgs(int count)
        {
            Count = count;
        }
    }
}
