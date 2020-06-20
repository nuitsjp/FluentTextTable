using System.IO;

namespace FluentTextTable
{
    public class VerticalBorder
    {
        private readonly char _lineStyle;
        
        internal VerticalBorder(bool isEnable, char lineStyle)
        {
            IsEnable = isEnable;
            _lineStyle = lineStyle;
        }

        public bool IsEnable { get; }

        internal void Write(TextWriter writer)
        {
            if(IsEnable) writer.Write(_lineStyle);
        }
    }
}