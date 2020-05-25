using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentTextTable
{
    public interface ITextTable<TItem>
    {
        IEnumerable<TItem> DataSource { get; set; }

        string ToPlanText();
        string ToMarkdown();
    }
}