using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITableConfig<TItem>
    {
        bool AutoGenerateColumns { get; set; }
        IColumnConfig AddColumn(Expression<Func<TItem, object>> expression);
    }
}