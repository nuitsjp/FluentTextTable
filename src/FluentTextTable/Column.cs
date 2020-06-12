using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class Column<TItem>
    {
        public string Name { get; }
        public int HeaderWidth { get; }
        public HorizontalAlignment HorizontalAlignment { get; }
        public VerticalAlignment VerticalAlignment { get; }
        public string Format { get; }

        private readonly MemberAccessor<TItem> _accessor;

        internal Column(
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

        public object GetValue(TItem item) => _accessor.GetValue(item);
    }
}