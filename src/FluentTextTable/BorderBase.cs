namespace FluentTextTable
{
    public class BorderBase
    {
        internal BorderBase(bool isEnable)
        {
            IsEnable = isEnable;
        }

        internal bool IsEnable { get; }
    }
}