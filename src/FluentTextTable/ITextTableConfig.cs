using System;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableConfig<TItem>
    {
        bool AutoGenerateColumns { get; set; }
        IHorizontalBorderConfig TopBorder { get; }
        IHorizontalBorderConfig HeaderHorizontalBorder { get; }
        IHorizontalBorderConfig InsideHorizontalBorder { get; }
        IHorizontalBorderConfig BottomBorder { get; }
        IVerticalBorderConfig LeftBorder { get; }
        IVerticalBorderConfig InsideVerticalBorder { get; }
        IVerticalBorderConfig RightBorder { get; }
        IColumn AddColumn(Expression<Func<TItem, object>> expression);
    }
}