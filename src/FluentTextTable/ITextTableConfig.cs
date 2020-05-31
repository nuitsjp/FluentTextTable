using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        bool AutoGenerateColumns { get; set; }
        IBordersConfig Borders { get; }
        IColumnConfig AddColumn(Expression<Func<TItem, object>> expression);
    }
}