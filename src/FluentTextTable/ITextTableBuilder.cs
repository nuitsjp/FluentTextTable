using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> Margins { get; }
        ITextTableBuilder<TItem> PaddingAs(int padding);
        IColumnBuilder<TItem> AddColumn(Expression<Func<TItem, object>> expression);
    }
}