using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    /// <summary>
    /// The configuration for building a Text Table.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface ITextTableBuilder<TItem>
    {
        /// <summary>
        /// Get the Margins Builder.
        /// </summary>
        IMarginsBuilder<TItem> Margins { get; }

        /// <summary>
        /// Get the Paddings Builder.
        /// </summary>
        IPaddingsBuilder<TItem> Paddings { get; }

        /// <summary>
        /// Get the Columns Builder.
        /// </summary>
        IColumnsBuilder<TItem> Columns { get; }
    }
}