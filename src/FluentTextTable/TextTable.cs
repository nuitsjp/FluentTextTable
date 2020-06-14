using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentTextTable.PlanText;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        public IReadOnlyList<IColumn<TItem>> Columns { get; }

        private TextTable(List<IColumn<TItem>> columns, Borders borders)
        {
            Columns = columns;
            Borders = borders;
        }

        public Borders Borders { get; }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var rows = new List<IRow<TItem>>();
            foreach (var item in items)
            {
                rows.Add(new Row<TItem>(Columns, item));
            }
            var rowSet = new RowSet<TItem>(Columns, rows);
            
            Borders.Top.Write(writer, this, rowSet);
            this.WriteHeader(writer, rowSet);
            Borders.HeaderHorizontal.Write(writer, this, rowSet);
            rowSet.Write(writer, this);
            Borders.Bottom.Write(writer, this, rowSet);
        }
        
        public static TextTable<TItem> Build()
        {
            var config = new TextTableConfig<TItem>();
            config.GenerateColumns();
            return new TextTable<TItem>(config.BuildColumns(), config.BuildBorders());
        }

        public static TextTable<TItem> Build(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return new TextTable<TItem>(config.BuildColumns(), config.BuildBorders());
        }
    }
}