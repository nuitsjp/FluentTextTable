using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public class Column<TItem> : IColumn<TItem>
    {
        private readonly string _name;
        private readonly MemberAccessor<TItem> _accessor;

        internal Column(
            string name,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            string format,
            MemberAccessor<TItem> accessor)
        {
            _name = name;
            Width = name.GetWidth();
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Format = format;
            _accessor = accessor;
        }

        public int Width { get; }
        public HorizontalAlignment HorizontalAlignment { get; }
        public VerticalAlignment VerticalAlignment { get; }
        public string Format { get; }

        public IEnumerable<object> GetValues(TItem item)
        {
            var value = _accessor.GetValue(item);

            return value switch
            {
                string stringValue => stringValue.SplitOnNewLine(),
                IEnumerable<object> enumerable => enumerable,
                _ => new[] {value}
            };
        }
        
        public void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            textWriter.Write(new string(' ', textTableLayout.Paddings.Left.Width));
            textWriter.Write(_name);
            textWriter.Write(new string(' ', textTableLayout.GetColumnWidth(this) - Width - textTableLayout.Paddings.Left.Width));
        }

    }
}