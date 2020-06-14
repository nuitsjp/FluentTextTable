namespace FluentTextTable
{
    public class ColumnConfig<TItem> : IColumnConfig
    {
        private string _name;
        private HorizontalAlignment _horizontalAlignment= HorizontalAlignment.Default;
        private VerticalAlignment _verticalAlignment = VerticalAlignment.Top;
        private string _format;

        private readonly MemberAccessor<TItem> _accessor;

        internal ColumnConfig(MemberAccessor<TItem> accessor)
        {
            _accessor = accessor;
            HasName(_accessor.Name);
        }

        public IColumnConfig HasName(string name)
        {
            _name = name;
            return this;
        }

        public IColumnConfig AlignHorizontal(HorizontalAlignment horizontalAlignment)
        {
            _horizontalAlignment = horizontalAlignment;
            return this;
        }

        public IColumnConfig AlignVertical(VerticalAlignment verticalAlignment)
        {
            _verticalAlignment = verticalAlignment;
            return this;
        }

        public IColumnConfig HasFormat(string format)
        {
            _format = format;
            return this;
        }

        internal IColumn<TItem> Build() 
            => new Column<TItem>(_name, _horizontalAlignment,  _verticalAlignment, _format, _accessor);
    }
}