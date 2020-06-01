using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class TextTableWriter<TItem> : ITextTableWriter
    {
        private readonly TextWriter _textWriter;

        private readonly IList<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Body<TItem> _body;
        private readonly Borders _borders;

        internal TextTableWriter(TextWriter textWriter, IList<Column<TItem>> columns, Headers<TItem> headers, Body<TItem> body, Borders borders)
        {
            _textWriter = textWriter;
            _columns = columns;
            _headers = headers;
            _body = body;
            _borders = borders;
        }

        internal int GetColumnWidth(Column<TItem> column) => column.GetWidth(_body);

        public void Write(char c) => _textWriter.Write(c);
        public void Write(string s) => _textWriter.Write(s);
        public void WriteLine(string s) => _textWriter.Write(s);
        public void WriteLine() => _textWriter.WriteLine();
        
        internal void WritePlanText()
        {

            // Write top border.
            _borders.Top.Write(this, _columns);

            // Write header.
            _headers.Write(this, _borders, _body);
            
            // Write Header and table separator.
            _borders.HeaderHorizontal.Write(this, _columns);

            // Write table.
            _body.WritePlaneText(this, _borders);

            // Write bottom border.
            _borders.Bottom.Write(this, _columns);
        }

        internal void WriteMarkdown()
        {
            // Write header and separator.
            var headerSeparator = new StringBuilder();
            _textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                _textWriter.Write(" ");

                _textWriter.Write(column.Header);
                _textWriter.Write(new string(' ', GetColumnWidth(column) - column.Header.GetWidth() - 2)); // TODO Fix -> column.Header.GetWidth() - 2

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

                _textWriter.Write(" |");
                headerSeparator.Append("|");
            }
            _textWriter.WriteLine();
            _textWriter.WriteLine(headerSeparator.ToString());

            // Write table.
            _body.WriteMarkdown(this);
        }

    }
}