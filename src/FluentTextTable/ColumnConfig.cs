namespace FluentTextTable
{
    public class ColumnConfig<TItem> : IColumnConfig
    {
        private string Name { get; set; }
        private HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;
        private VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;
        private string Format { get; set; }

        private readonly MemberAccessor<TItem> _accessor;

        internal ColumnConfig(MemberAccessor<TItem> accessor)
        {
            _accessor = accessor;
            NameIs(_accessor.Name);
        }

        public IColumnConfig NameIs(string name)
        {
            Name = name;
            return this;
        }

        public IColumnConfig AlignHorizontalTo(HorizontalAlignment horizontalAlignment)
        {
            HorizontalAlignment = horizontalAlignment;
            return this;
        }

        public IColumnConfig AlignVerticalTo(VerticalAlignment verticalAlignment)
        {
            VerticalAlignment = verticalAlignment;
            return this;
        }

        public IColumnConfig FormatTo(string format)
        {
            Format = format;
            return this;
        }

        internal IColumn<TItem> Build() 
            => new Column<TItem>(Name, HorizontalAlignment,  VerticalAlignment, Format, _accessor);
    }
}