using System;
using System.Linq;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTableCell<TItem>
    {
        private const int Margin = 2;
        public TextTableCell(TextTableColumn<TItem> column, object value)
        {
            Column = column;

            var values =
                value is string stringValue
                    ? stringValue.Split(Environment.NewLine.ToCharArray())
                    : new [] {value};

            Values =
                column.Format is null
                    ? values.Select(x => x?.ToString() ?? string.Empty).ToArray()
                    : values.Select(x => string.Format(column.Format, x)).ToArray();
            Width = Values.Max(x =>x.GetWidth()) + Margin;
        }

        internal ITextTableColumn Column { get; }
        internal string[] Values { get; }

        internal int Width { get; }
        internal int Height => Values.Length;
    }
}