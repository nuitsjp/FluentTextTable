using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        bool AutoGenerateColumns { get; set; }
        IBorderConfig TopBorder { get; }
        IColumn AddColumn(Expression<Func<TItem, object>> expression);
    }
}