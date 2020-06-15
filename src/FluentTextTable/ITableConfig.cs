using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITableConfig<TItem>
    {
        ITableConfig<TItem> HasPadding(int padding);
        ITableConfig<TItem> EnableGenerateColumns();
        IColumnConfig AddColumn(Expression<Func<TItem, object>> expression);
    }
}