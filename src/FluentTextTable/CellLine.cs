using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class CellLine
    {
        internal static readonly CellLine BlankCellLine = new CellLine(); 
        
        private const int Margin = 2;

        public string Value { get; }

        public int Width { get; }

        internal CellLine(IColumn column, object value)
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