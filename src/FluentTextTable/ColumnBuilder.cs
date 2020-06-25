using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public class ColumnBuilder<TItem> : CompositeTextTableBuilder<TItem>, IColumnBuilder<TItem>
    {
        private string _name;
        private HorizontalAlignment _horizontalAlignment= HorizontalAlignment.Default;
        private VerticalAlignment _verticalAlignment = VerticalAlignment.Top;
        private string _format;

        private readonly MemberAccessor<TItem> _accessor;

        internal ColumnBuilder(ITextTableBuilder<TItem> textTableBuilder, MemberAccessor<TItem> accessor) : base(textTableBuilder)
        {
            _accessor = accessor;
            NameAs(_accessor.Name);
        }

        public IColumnBuilder<TItem> NameAs(string name)
        {
            _name = name;
            return this;
        }

        public IColumnBuilder<TItem> HorizontalAlignmentAs(HorizontalAlignment horizontalAlignment)
        {
            _horizontalAlignment = horizontalAlignment;
            return this;
        }

        public IColumnBuilder<TItem> VerticalAlignmentAs(VerticalAlignment verticalAlignment)
        {
            _verticalAlignment = verticalAlignment;
            return this;
        }

        public IColumnBuilder<TItem> FormatAs(string format)
        {
            _format = format;
            return this;
        }

        internal IColumn<TItem> Build() 
            => new Column<TItem>(_name, _horizontalAlignment,  _verticalAlignment, _format, _accessor);
    }
}