using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface ITextTableWriter<in TItem>
    {
        string ToPlanText(IEnumerable<TItem> items);
        void WritePlanText(TextWriter writer, IEnumerable<TItem> items);

        string ToMarkdown(IEnumerable<TItem> items);
        void WriteMarkdown(TextWriter writer, IEnumerable<TItem> items);
    }
}