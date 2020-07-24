using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class HorizontalsBorderBuilder<TItem> : CompositeTextTableBuilder<TItem>, IHorizontalBorderBuilder<TItem>
    {
        private readonly List<IHorizontalBorderBuilder<TItem>> _horizontalBorderBuilders;
        public HorizontalsBorderBuilder(ITextTableBuilder<TItem> textTableBuilder, params IHorizontalBorderBuilder<TItem>[] horizontalBorderBuilders) : base(textTableBuilder)
        {
            _horizontalBorderBuilders = horizontalBorderBuilders.ToList();
        }

        public IHorizontalBorderBuilder<TItem> AllStylesAs(string s)
        {
            _horizontalBorderBuilders.ForEach(x => x.AllStylesAs(s));
            return this;
        }

        public IHorizontalBorderBuilder<TItem> LeftStyleAs(string s)
        {
            _horizontalBorderBuilders.ForEach(x => x.LeftStyleAs(s));
            return this;
        }

        public IHorizontalBorderBuilder<TItem> LineStyleAs(string s)
        {
            _horizontalBorderBuilders.ForEach(x => x.LineStyleAs(s));
            return this;
        }

        public IHorizontalBorderBuilder<TItem> IntersectionStyleAs(string s)
        {
            _horizontalBorderBuilders.ForEach(x => x.IntersectionStyleAs(s));
            return this;
        }

        public IHorizontalBorderBuilder<TItem> RightStyleAs(string s)
        {
            _horizontalBorderBuilders.ForEach(x => x.RightStyleAs(s));
            return this;
        }

        public IHorizontalBorderBuilder<TItem> AsDisable()
        {
            _horizontalBorderBuilders.ForEach(x => x.AsDisable());
            return this;
        }
    }
}