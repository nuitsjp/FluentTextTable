using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface ITextTable<in TItem>
    {
        string ToString(IEnumerable<TItem> items);
        void WriteLine(IEnumerable<TItem> items);
        void Write(TextWriter textWriter, IEnumerable<TItem> items);
    }
}