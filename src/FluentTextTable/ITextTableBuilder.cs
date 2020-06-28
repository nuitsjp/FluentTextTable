using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> Margins { get; }
        IPaddingsBuilder<TItem> Paddings { get; }
        IColumnBuilder<TItem> AddColumn(Expression<Func<TItem, object>> expression);
    }
}