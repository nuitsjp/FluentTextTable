using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        bool HasColumns { get; }
        ITextTableConfig<TItem> HasPadding(int padding);
        IColumnConfig AddColumn(Expression<Func<TItem, object>> expression);
    }
}