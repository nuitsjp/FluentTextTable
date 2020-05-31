using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FluentTextTable
{
    public class HorizontalBorder : BorderBase
    {
        internal HorizontalBorder(bool isEnable, char lineStyle, char leftStyle, char intersectionStyle, char rightStyle) : base(isEnable, lineStyle)
        {
            _leftStyle = leftStyle;
            _intersectionStyle = intersectionStyle;
            _rightStyle = rightStyle;
        }

        private readonly char _leftStyle;
        private readonly char _intersectionStyle;
        private readonly char _rightStyle;

        internal void Write(TextWriter writer, IEnumerable<ColumnConfig> columns, Borders borders)
        {
            if(!IsEnable) return;
            
            if(borders.Left.IsEnable) writer.Write(_leftStyle);
            var items = new List<string>();
            foreach (var column in columns)
            {
                items.Add(new string(_lineStyle, column.Width));
            }

            if (borders.InsideVertical.IsEnable)
            {
                writer.Write(string.Join(_intersectionStyle.ToString(), items));
            }
            else
            {
                writer.Write(string.Join(string.Empty, items));
            }
            
            if(borders.Right.IsEnable) writer.Write(_rightStyle);
            
            writer.WriteLine();
        }

    }
}