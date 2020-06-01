using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly List<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Borders _borders;

        internal TextTable(TextTableConfig<TItem> config, List<Column<TItem>> columns)
        {
            _columns = columns;
            _borders = config.BuildBorders();
            _headers = new Headers<TItem>(_columns);
        }

        public IEnumerable<TItem> DataSource { get; set; }


        public string ToPlanText()
        {
            var writer = new StringWriter();
            WritePlanText(writer);
            return writer.ToString();
        }

        public void WritePlanText(TextWriter writer)
        {
            var body = new Body<TItem>(_columns, DataSource);
            new TextTableWriter<TItem>(writer, _columns, _headers, body, _borders).WritePlanText();
        }

        public string ToMarkdown()
        {
            var writer = new StringWriter();
            WriteMarkdown(writer);
            return writer.ToString();
        }

        public void WriteMarkdown(TextWriter writer)
        {
            var body = new Body<TItem>(_columns, DataSource);
            new TextTableWriter<TItem>(writer, _columns, _headers, body, _borders).WriteMarkdown();
        }
    }
}
