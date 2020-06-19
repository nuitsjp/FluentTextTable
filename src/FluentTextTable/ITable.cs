using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface ITable
    {
        public int Padding { get; }
        public IReadOnlyList<IColumn> Columns { get; }
    }
    
    public interface ITable<in TItem> : ITable
    {
        string ToString(IEnumerable<TItem> items);
        void Write(TextWriter writer, IEnumerable<TItem> items);
    }
}