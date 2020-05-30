using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly HorizontalBorderConfig _topBorder;
        private readonly HorizontalBorderConfig _headerHorizontalBorder;
        private readonly HorizontalBorderConfig _insideHorizontalBorder;
        private readonly HorizontalBorderConfig _bottomBorder;
        private readonly VerticalBorderConfig _leftBorder;
        private readonly VerticalBorderConfig _insideVerticalBorder;
        private readonly VerticalBorderConfig _rightBorder;

        
        private readonly List<Column> _columns;
        private readonly Header _header;
        private readonly Borders _borders;
        private readonly Dictionary<Column, MemberAccessor<TItem>> _memberAccessors;

        internal TextTable(TextTableConfig<TItem> config)
        {
            _columns = config.Columns;
            _topBorder = (HorizontalBorderConfig) config.Borders.Top;
            _headerHorizontalBorder = (HorizontalBorderConfig) config.Borders.HeaderHorizontal;
            _insideHorizontalBorder = (HorizontalBorderConfig) config.Borders.InsideHorizontal;
            _bottomBorder = (HorizontalBorderConfig) config.Borders.Bottom;
            _leftBorder = (VerticalBorderConfig) config.Borders.Left;
            _insideVerticalBorder = (VerticalBorderConfig) config.Borders.InsideVertical;
            _rightBorder = (VerticalBorderConfig) config.Borders.Right;

            _borders = config.BuildBorders();
            _header = new Header(_columns);
            _memberAccessors = config.MemberAccessors;
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
            var rows = DataSource.Select(x => Row.Create(x, _memberAccessors)).ToList();

            foreach (var column in _columns)
            {
                column.UpdateWidth(rows);
            }


            // Write top border.
            _borders.Top.Write(writer, _columns, _borders);

            // Write header.
            _header.Write(writer, _borders);
            
            // Write Header and table separator.
            _borders.HeaderHorizontal.Write(writer, _columns, _borders);

            // Write table.
            if (rows.Any())
            {
                rows.First().WritePlanText(writer, _columns, _leftBorder, _insideVerticalBorder, _rightBorder);
                foreach (var row in rows.Skip(1))
                {
                    _borders.InsideHorizontal.Write(writer, _columns, _borders);
                    row.WritePlanText(writer, _columns, _leftBorder, _insideVerticalBorder, _rightBorder);
                }
            }

            // Write bottom border.
            _borders.Bottom.Write(writer, _columns, _borders);
        }

        private string TopHorizontalBorder(HorizontalBorderConfig borderConfig, IEnumerable<IColumn> columns)
        {
            var builder = new StringBuilder();
            if(_leftBorder.IsEnable) builder.Append(borderConfig.LeftEnd);
            var borders = new List<string>();
            foreach (var column in _columns)
            {
                borders.Add(new string(borderConfig.Line, column.Width));
            }

            if (_insideVerticalBorder.IsEnable)
            {
                builder.Append(string.Join(borderConfig.Intersection.ToString(), borders));
            }
            else
            {
                builder.Append(string.Join(string.Empty, borders));
            }
            
            if(_rightBorder.IsEnable) builder.Append(borderConfig.RightEnd);
            
            return builder.ToString();

        }

        public string ToMarkdown()
        {
            var writer = new StringWriter();
            WriteMarkdown(writer);
            return writer.ToString();
        }

        public void WriteMarkdown(TextWriter writer)
        {
            var rows = DataSource.Select(x => Row.Create(x, _memberAccessors)).ToList();

            foreach (var column in _columns)
            {
                column.UpdateWidth(rows);
            }


            // Write header and separator.
            var headerSeparator = new StringBuilder();
            writer.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                writer.Write(" ");

                writer.Write(column.Header);
                writer.Write(new string(' ', column.Width - column.HeaderWidth));

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', column.Width));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', column.Width - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', column.Width - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', column.Width - 1));
                        headerSeparator.Append(':');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                writer.Write(" |");
                headerSeparator.Append("|");
            }
            writer.WriteLine();
            writer.WriteLine(headerSeparator.ToString());

            // Write table.
            foreach (var row in rows)
            {
                row.WriteMarkdown(writer, _columns);
            }
        }
    }
}
