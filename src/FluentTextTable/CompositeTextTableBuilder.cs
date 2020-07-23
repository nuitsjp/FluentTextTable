using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public abstract class CompositeTextTableBuilder<TItem> : ITextTableBuilder<TItem>
    {
        protected CompositeTextTableBuilder(ITextTableBuilder<TItem> textTableBuilder)
        {
            TextTableBuilder = textTableBuilder;
        }

        protected ITextTableBuilder<TItem> TextTableBuilder { get; }

        public IMarginsBuilder<TItem> Margins => TextTableBuilder.Margins;
        public IPaddingsBuilder<TItem> Paddings => TextTableBuilder.Paddings;
        public IColumnsBuilder<TItem> Columns => TextTableBuilder.Columns;
        public IBordersBuilder<TItem> Borders => TextTableBuilder.Borders;
    }
}