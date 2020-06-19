using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class Column<TItem> : IColumn<TItem>
    {
        private readonly MemberAccessor<TItem> _accessor;

        internal Column(
            string name,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            string format,
            MemberAccessor<TItem> accessor)
        {
            Name = name;
            HeaderWidth = name.GetWidth();
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Format = format;
            _accessor = accessor;
        }

        public string Name { get; }
        public int HeaderWidth { get; }
        public HorizontalAlignment HorizontalAlignment { get; }
        public VerticalAlignment VerticalAlignment { get; }
        public string Format { get; }

        public object GetValue(TItem item) => _accessor.GetValue(item);
        
        public void WriteHeader(TextWriter writer, ITableLayout tableLayout)
        {
            writer.Write(new string(' ', tableLayout.Padding));
            writer.Write(Name);
            writer.Write(new string(' ', tableLayout.GetColumnWidth(this) - HeaderWidth - tableLayout.Padding));
        }

    }
}