using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        ITextTableConfig<TItem> HasPadding(int padding);
        ITextTableConfig<TItem> EnableAutoGenerateColumns();
        IColumnConfig AddColumn(Expression<Func<TItem, object>> expression);
    }
}