using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IHeader
    {
        IReadOnlyList<IColumn> Columns { get; }

        void Write(TextWriter textWriter, ITextTableLayout textTableLayout);
    }
}