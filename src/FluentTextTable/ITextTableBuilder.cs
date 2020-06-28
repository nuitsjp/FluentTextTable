using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> Margins { get; }
        IPaddingsBuilder<TItem> Paddings { get; }
        IColumnsBuilder<TItem> Columns { get; }
    }
}