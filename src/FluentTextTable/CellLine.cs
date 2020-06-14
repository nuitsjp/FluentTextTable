using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class CellLine
    {
        internal static readonly CellLine BlankCellLine = new CellLine(null, null); 
        
        private const int Margin = 2;

        public string Value { get; }

        public int Width { get; }

        internal CellLine(object value, string format)
        {
            Value = format is null
                ? value is null
                    ? string.Empty
                    : value.ToString()
                : string.Format(format, value);
            Width = Value.GetWidth() + Margin;
        }
   }
}