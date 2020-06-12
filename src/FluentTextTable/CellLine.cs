using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class CellLine<TItem>
    {
        private const int Margin = 2;

        private readonly Column<TItem> _column;

        internal string Value { get; }

        internal int Width { get; }

        internal CellLine(Column<TItem> column, object value)
        {
            _column = column;
            Value = column?.Format is null
                ? value?.ToString() ?? string.Empty
                : string.Format(column.Format, value);
            Width = Value.GetWidth() + Margin;
        }

        internal CellLine(Column<TItem> column)
        {
            _column = column;
            Value = string.Empty;
            Width = Margin;
        }
   }
}