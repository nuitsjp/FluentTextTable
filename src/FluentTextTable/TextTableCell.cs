using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTableCell
    {
        private const int Margin = 2;
        public TextTableCell(ITextTableColumn column, string value)
        {
            Column = column;
            Value = value;
            Width = Value.GetWidth() + Margin;
        }

        internal ITextTableColumn Column { get; }
        internal string Value { get; }

        internal int Width { get; }
    }
}