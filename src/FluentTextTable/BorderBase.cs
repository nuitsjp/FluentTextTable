namespace FluentTextTable
{
    public class BorderBase
    {
        protected BorderBase(bool isEnable, char lineStyle)
        {
            IsEnable = isEnable;
            LineStyle = lineStyle;
        }

        public bool IsEnable { get; }
        public char LineStyle { get; }
    }
}