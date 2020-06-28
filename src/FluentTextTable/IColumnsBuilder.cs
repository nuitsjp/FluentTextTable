using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface IColumnsBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IColumnBuilder<TItem> Add(Expression<Func<TItem, object>> expression);
    }
}