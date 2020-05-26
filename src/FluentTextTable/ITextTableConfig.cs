using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        Column<TItem> AddColumn(Expression<Func<TItem, object>> expression);
    }
}