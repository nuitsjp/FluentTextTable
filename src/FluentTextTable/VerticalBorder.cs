using System.IO;

namespace FluentTextTable
{
    public class VerticalBorder
    {
        private readonly string _lineStyle;
        
        internal VerticalBorder(bool isEnable, string lineStyle)
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