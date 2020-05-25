using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        TextTableColumn<TItem> AddColumn(Expression<Func<TItem, object>> expression);
    }
}