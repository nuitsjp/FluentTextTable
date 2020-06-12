using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class CellLine<TItem>
    {
        internal static readonly CellLine<TItem> BlankCellLine = new CellLine<TItem>(); 
        
        private const int Margin = 2;

        internal string Value { get; }

        internal int Width { get; }

        internal CellLine(Column<TItem> column, object value)
        {
            Value = column?.Format is null
                ? value?.ToString() ?? string.Empty
                : string.Format(column.Format, value);
            Width = Value.GetWidth() + Margin;
        }

        private CellLine()
        {
            Value = string.Empty;
            Width = Margin;
        }
   }
}