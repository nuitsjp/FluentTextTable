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

        public IVerticalBorderBuilder<TItem> AsDisable()
        {
            _isEnable = false;
            return this;
        }

        public IVerticalBorderBuilder<TItem>  LineStyleAs(string c)
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