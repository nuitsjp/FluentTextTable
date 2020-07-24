using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class VerticalsBorderBuilder<TItem> : CompositeTextTableBuilder<TItem>, IVerticalBorderBuilder<TItem>
    {
        private readonly List<IVerticalBorderBuilder<TItem>> _verticalBorderBuilders;
        public VerticalsBorderBuilder(ITextTableBuilder<TItem> textTableBuilder, params IVerticalBorderBuilder<TItem>[] verticalBorderBuilders) : base(textTableBuilder)
        {
            _verticalBorderBuilders = verticalBorderBuilders.ToList();
        }

        public IVerticalBorderBuilder<TItem> LineStyleAs(string s)
        {
            _verticalBorderBuilders.ForEach(x => x.LineStyleAs(s));
            return this;
        }

        public IVerticalBorderBuilder<TItem> AsDisable()
        {
            _verticalBorderBuilders.ForEach(x => x.AsDisable());
            return this;
        }
    }
}