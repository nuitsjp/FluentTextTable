using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly IList<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        internal Body<TItem> Body { get; }
        private readonly Borders _borders;
        private readonly IDictionary<Column<TItem>, int> _columnWidths = new Dictionary<Column<TItem>, int>();

        internal TextTable(IList<Column<TItem>> columns, Headers<TItem> headers, Body<TItem> body, Borders borders)
        {
            _columns = columns;
            _headers = headers;
            Body = body;
            _borders = borders;

            foreach (var column in _columns)
            {
                _columnWidths[column] = Math.Max(column.HeaderWidth, Body.GetColumnWidth(column));
            }
        }

        int ITextTable<TItem>.GetColumnWidth(Column<TItem> column) => _columnWidths[column];
    }
}