namespace FluentTextTable
{
    public class BorderBase
    {
        protected readonly char _lineStyle;

        internal BorderBase(bool isEnable, char lineStyle)
        {
            IsEnable = isEnable;
            _lineStyle = lineStyle;
        }

        internal bool IsEnable { get; }
    }
}