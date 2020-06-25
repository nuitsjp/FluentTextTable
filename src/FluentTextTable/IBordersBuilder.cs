﻿namespace FluentTextTable
{
    public interface IBordersBuilder<TItem> : IBorderBuilder<TItem>
    {
        IHorizontalBorderBuilder<TItem> Top { get; }
        IHorizontalBorderBuilder<TItem> HeaderHorizontal { get; }
        IHorizontalBorderBuilder<TItem> InsideHorizontal { get; }
        IHorizontalBorderBuilder<TItem> Bottom { get; }
        IVerticalBorderBuilder<TItem> Left { get; }
        IVerticalBorderBuilder<TItem> InsideVertical { get; }
        IVerticalBorderBuilder<TItem> Right { get; }
        IBordersBuilder<TItem> AsFullWidthStyle();
    }
}