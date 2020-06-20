using System.Collections.Generic;
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

        public IEnumerable<object> GetValues(TItem item)
        {
            var value = _accessor.GetValue(item);
            
            IEnumerable<object> objects;
            if (value is string stringValue)
            {
                return stringValue.SplitOnNewLine();
            }
            
            if (value is IEnumerable<object> enumerable)
            {
                return enumerable;
            }

            return new[] {value};
        }
        
        public void WriteHeader(TextWriter writer, ITextTableLayout textTableLayout)
        {
            writer.Write(new string(' ', textTableLayout.Padding));
            writer.Write(Name);
            writer.Write(new string(' ', textTableLayout.GetWidthOf(this) - HeaderWidth - textTableLayout.Padding));
        }

    }
}