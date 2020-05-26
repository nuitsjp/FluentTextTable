using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        IColumn AddColumn(Expression<Func<TItem, object>> expression);
    }
}