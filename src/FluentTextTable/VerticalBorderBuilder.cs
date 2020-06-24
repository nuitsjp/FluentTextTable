using System;

namespace FluentTextTable
{
    public class VerticalBorderBuilder<TItem> : CompositeTextTableBuilder<TItem>, IVerticalBorderBuilder<TItem>
    {
        private bool _isEnable  = true;
        private string _line  = "|";

        public VerticalBorderBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
        }

        public void AsDisable()
        {
            _isEnable = false;
        }

        public IVerticalBorderBuilder<TItem>  LeftStyleAs(string c)
        {
            _line = c;
            return this;
        }

        internal int LineWidth => _line.GetWidth();

        internal IVerticalBorder Build()
        {
            return new VerticalBorder(_isEnable, _line);
        }
    }
}