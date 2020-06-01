using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FluentTextTable
{
    public class HorizontalBorder : BorderBase
    {
        private readonly char _leftStyle;
        private readonly char _intersectionStyle;
        private readonly char _rightStyle;
        private readonly VerticalBorder _leftVerticalBorder;
        private readonly VerticalBorder _insideVerticalBorder;
        private readonly VerticalBorder _rightVerticalBorder;

        internal HorizontalBorder(
            bool isEnable, 
            char lineStyle,
            char leftStyle,
            char intersectionStyle,
            char rightStyle,
            VerticalBorder leftVerticalBorder,
            VerticalBorder insideVerticalBorder,
            VerticalBorder rightVerticalBorder) 
            : base(isEnable, lineStyle)
        {
            _leftStyle = leftStyle;
            _intersectionStyle = intersectionStyle;
            _rightStyle = rightStyle;
            _leftVerticalBorder = leftVerticalBorder;
            _insideVerticalBorder = insideVerticalBorder;
            _rightVerticalBorder = rightVerticalBorder;
        }

        internal void Write<TItem>(TextTableWriter<TItem> writer, IEnumerable<Column<TItem>> columns)
        {
            if(!IsEnable) return;
            
            if(_leftVerticalBorder.IsEnable) writer.Write(_leftStyle);
            var items = new List<string>();
            foreach (var column in columns)
            {
                items.Add(new string(_lineStyle, writer.GetColumnWidth(column)));
            }

            if (_insideVerticalBorder.IsEnable)
            {
                writer.Write(string.Join(_intersectionStyle.ToString(), items));
            }
            else
            {
                writer.Write(string.Join(string.Empty, items));
            }
            
            if(_rightVerticalBorder.IsEnable) writer.Write(_rightStyle);
            
            writer.WriteLine();
        }

    }
}