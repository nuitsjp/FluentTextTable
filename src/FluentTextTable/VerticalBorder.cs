using System.IO;

namespace FluentTextTable
{
    public class VerticalBorder
    {
        internal VerticalBorder(bool isEnable, char lineStyle)
        {
            IsEnable = isEnable;
            LineStyle = lineStyle;
        }

        public bool IsEnable { get; }
        public char LineStyle { get; }

        internal void Write(TextWriter writer)
        {
            if(IsEnable) writer.Write(LineStyle);
        }
    }
}