using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : TableConfig<TItem>, ITextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();

        public IBordersConfig Borders => _borders;

        private Borders BuildBorders() => _borders.Build();

        internal ITable<TItem> Build()
        {
            return new Table<TItem>(Padding, BuildHeader(), BuildBorders(), ToStrings);
        }
        
        private static IEnumerable<string> ToStrings(IEnumerable<object> objects, string format)
        {
            return objects.Select(x => x.ToString(format));
        }


    }
}