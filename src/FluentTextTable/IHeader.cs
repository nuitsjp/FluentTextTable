using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IHeader
    {
#if NET40
        IList<IColumn> Columns { get; }
#else
        IReadOnlyList<IColumn> Columns { get; }
#endif

        void Write(TextWriter textWriter, ITextTableLayout textTableLayout);
    }
}