using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    internal class Header<TItem>
    {
        private readonly List<Column<TItem>> _columns;
        private readonly Borders _borders;

        internal Header(List<Column<TItem>> columns, Borders borders)
        {
            _columns = columns;
            _borders = borders;
        }
    }
}