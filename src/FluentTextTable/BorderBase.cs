namespace FluentTextTable
{
    public class BorderBase
    {
        internal BorderBase(bool isEnable, char lineStyle)
        {
            IsEnable = isEnable;
            LineStyle = lineStyle;
        }

        internal bool IsEnable { get; }
        protected char LineStyle { get; }
    }
}