using System.IO;

namespace FluentTextTable
{
    public class VerticalBorder : IVerticalBorder
    {
        private readonly string _lineStyle;
        
        internal VerticalBorder(bool isEnable, string lineStyle)
        {
            IsEnable = isEnable;
            _lineStyle = lineStyle;
        }

        public bool IsEnable { get; }

        public void Write(TextWriter textWriter)
        {
            if(IsEnable) textWriter.Write(_lineStyle);
        }
    }
}