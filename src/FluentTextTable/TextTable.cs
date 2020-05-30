using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly List<Column> _columns;
        private readonly HorizontalBorderConfig _topBorder;
        private readonly HorizontalBorderConfig _headerHorizontalBorder;
        private readonly HorizontalBorderConfig _insideHorizontalBorder;
        private readonly HorizontalBorderConfig _bottomBorder;
        private readonly VerticalBorderConfig _leftBorder;
        private readonly VerticalBorderConfig _insideVerticalBorder;
        private readonly VerticalBorderConfig _rightBorder;
        private readonly Dictionary<Column, MemberAccessor<TItem>> _memberAccessors;

        internal TextTable(TextTableConfig<TItem> config)
        {
            _columns = config.Columns;
            _topBorder = (HorizontalBorderConfig) config.TopBorder;
            _headerHorizontalBorder = (HorizontalBorderConfig) config.HeaderHorizontalBorder;
            _insideHorizontalBorder = (HorizontalBorderConfig) config.InsideHorizontalBorder;
            _bottomBorder = (HorizontalBorderConfig) config.BottomBorder;
            _leftBorder = (VerticalBorderConfig) config.LeftBorder;
            _insideVerticalBorder = (VerticalBorderConfig) config.InsideVerticalBorder;
            _rightBorder = (VerticalBorderConfig) config.RightBorder;
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


            var rowSeparatorBuilder = new StringBuilder();
            foreach (var column in _columns)
            {
                rowSeparatorBuilder.Append('+');
                rowSeparatorBuilder.Append(new string('-', column.Width));
            }
            rowSeparatorBuilder.Append("+");

            var rowSeparator = rowSeparatorBuilder.ToString();

            // Write top border.
            if (_topBorder.IsEnable)
            {
                writer.WriteLine(TopHorizontalBorder(_topBorder, _columns));
            }

            // Write header.
            if (_leftBorder.IsEnable) writer.Write(_leftBorder.Line);
            
            _columns.First().WriteHeader(writer);
            writer.Write(" ");
            
            foreach (var column in _columns.Skip(1))
            {
                if(_insideVerticalBorder.IsEnable) writer.Write(_insideVerticalBorder.Line);

                column.WriteHeader(writer);
                writer.Write(" ");
            }
            if(_rightBorder.IsEnable) writer.Write(_rightBorder.Line);
            writer.WriteLine();

            // Write Header and table separator.
            if (_headerHorizontalBorder.IsEnable)
            {
                writer.WriteLine(TopHorizontalBorder(_headerHorizontalBorder, _columns));
            }

            // Write table.
            if (rows.Any())
            {
                rows.First().WritePlanText(writer, _columns, _leftBorder, _insideVerticalBorder, _rightBorder);
                var insideHorizontalBorder = string.Empty;
                if (_insideHorizontalBorder.IsEnable)
                {
                    insideHorizontalBorder = TopHorizontalBorder(_insideHorizontalBorder, _columns);
                }
                
                foreach (var row in rows.Skip(1))
                {
                    if (_insideHorizontalBorder.IsEnable)
                    {
                        writer.WriteLine(insideHorizontalBorder);
                    }
                    row.WritePlanText(writer, _columns, _leftBorder, _insideVerticalBorder, _rightBorder);
                }
            }

            // Write bottom border.
            if (_bottomBorder.IsEnable)
            {
                writer.WriteLine(TopHorizontalBorder(_bottomBorder, _columns));
            }
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
