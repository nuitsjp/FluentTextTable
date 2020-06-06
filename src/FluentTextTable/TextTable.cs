using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly IList<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Body<TItem> _body;
        private readonly Borders _borders;
        private readonly IDictionary<Column<TItem>, int> _columnWidths = new Dictionary<Column<TItem>, int>();

        internal TextTable(IList<Column<TItem>> columns, Headers<TItem> headers, Body<TItem> body, Borders borders)
        {
            _columns = columns;
            _headers = headers;
            _body = body;
            _borders = borders;

            foreach (var column in _columns)
            {
                _columnWidths[column] = Math.Max(column.HeaderWidth, _body.GetColumnWidth(column));
            }
        }

        internal void WritePlanText(TextWriter writer)
        {
            _borders.Top.Write(writer, this, _columns);
            _headers.Write(writer, this);
            _borders.HeaderHorizontal.Write(writer, this, _columns);
            _body.WritePlaneText(writer, this);
            _borders.Bottom.Write(writer, this, _columns);
        }

        internal void WriteMarkdown(TextWriter textWriter)
        {
            var headerSeparator = new StringBuilder();
            textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                textWriter.Write(" ");

                textWriter.Write(column.Name);
                textWriter.Write(new string(' ', ((ITextTable<TItem>)this).GetColumnWidth(column) - column.Name.GetWidth() - 2)); // TODO Fix -> column.Header.GetWidth() - 2

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', _columnWidths[column]));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', _columnWidths[column] - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', _columnWidths[column] - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', _columnWidths[column] - 1));
                        headerSeparator.Append(':');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                textWriter.Write(" |");
                headerSeparator.Append("|");
            }
            textWriter.WriteLine();
            textWriter.WriteLine(headerSeparator.ToString());

            // Write table.
            _body.WriteMarkdown(textWriter, this);
        }

        int ITextTable<TItem>.GetColumnWidth(Column<TItem> column) => _columnWidths[column];
    }
}