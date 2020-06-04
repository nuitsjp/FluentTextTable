using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class Column<TItem>
    {
        internal string Header { get; }
        internal int HeaderWidth { get; }
        internal HorizontalAlignment HorizontalAlignment { get; }
        internal VerticalAlignment VerticalAlignment { get; }
        internal string Format { get; }

        private readonly MemberAccessor<TItem> _accessor;

        public Column(string header, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, string format, MemberAccessor<TItem> accessor)
        {
            Header = header;
            HeaderWidth = header.GetWidth() + 2;
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Format = format;
            _accessor = accessor;
        }

        internal object GetValue(TItem item) => _accessor.GetValue(item);

        //internal int GetWidth(Body<TItem> body) => Math.Max(HeaderWidth, body.GetWidth(this));
        
        internal void WriteHeader(TextWriter writer, ITextTable<TItem> table)
        {
            writer.Write(" ");
            writer.Write(Header);
            writer.Write(new string(' ', table.GetColumnWidth(this) - HeaderWidth));
        }

    }
}