using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTableWriter<TItem>
    {
        private readonly IList<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Body<TItem> _body;
        private readonly Borders _borders;

        internal TextTableWriter(IList<Column<TItem>> columns, Headers<TItem> headers, Body<TItem> body, Borders borders)
        {
            _columns = columns;
            _headers = headers;
            _body = body;
            _borders = borders;
        }

        internal int GetColumnWidth(Column<TItem> column) => column.GetWidth(_body);

        internal void WritePlanText(TextWriter writer)
        {

            // Write top border.
            _borders.Top.Write(writer, this, _columns);

            // Write header.
            _headers.Write(writer, _borders, _body);
            
            // Write Header and table separator.
            _borders.HeaderHorizontal.Write(writer, this, _columns);

            // Write table.
            _body.WritePlaneText(writer, this, _borders);

            // Write bottom border.
            _borders.Bottom.Write(writer, this, _columns);
        }

        internal void WriteMarkdown(TextWriter textWriter)
        {
            // Write header and separator.
            var headerSeparator = new StringBuilder();
            textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                textWriter.Write(" ");

                textWriter.Write(column.Header);
                textWriter.Write(new string(' ', GetColumnWidth(column) - column.Header.GetWidth() - 2)); // TODO Fix -> column.Header.GetWidth() - 2

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 1));
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

    }
}