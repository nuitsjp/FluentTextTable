using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class Column<TItem>
    {
        internal string Name { get; }
        internal int HeaderWidth { get; }
        internal HorizontalAlignment HorizontalAlignment { get; }
        internal VerticalAlignment VerticalAlignment { get; }
        internal string Format { get; }

        private readonly MemberAccessor<TItem> _accessor;

        public Column(
            string name,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            string format,
            MemberAccessor<TItem> accessor)
        {
            Name = name;
            HeaderWidth = name.GetWidth() + 2;
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Format = format;
            _accessor = accessor;
        }

        internal object GetValue(TItem item) => _accessor.GetValue(item);
        
        internal void WriteHeader(TextWriter writer, ITextTable<TItem> table)
        {
            writer.Write(" ");
            writer.Write(Name);
            writer.Write(new string(' ', table.GetColumnWidth(this) - HeaderWidth));
        }

    }
}