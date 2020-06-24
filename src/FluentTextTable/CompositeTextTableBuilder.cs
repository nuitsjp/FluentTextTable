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

        public ITextTableBuilder<TItem> PaddingAs(int padding) => TextTableBuilder.PaddingAs(padding);

        public IColumnBuilder<TItem> AddColumn(Expression<Func<TItem, object>> expression) =>
            TextTableBuilder.AddColumn(expression);
    }
}