using System;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class HorizontalBorder : IHorizontalBorder
    {
        private readonly bool _isEnable;
        private readonly string _lineStyle;
        private readonly string _leftStyle;
        private readonly string _intersectionStyle;
        private readonly string _rightStyle;
        private readonly IVerticalBorder _leftVerticalBorder;
        private readonly IVerticalBorder _insideVerticalBorder;
        private readonly IVerticalBorder _rightVerticalBorder;

        internal HorizontalBorder(
            bool isEnable, 
            string lineStyle,
            string leftStyle,
            string intersectionStyle,
            string rightStyle,
            IVerticalBorder leftVerticalBorder,
            IVerticalBorder insideVerticalBorder,
            IVerticalBorder rightVerticalBorder)
        {
            _isEnable = isEnable;
            _lineStyle = lineStyle;
            _leftStyle = leftStyle;
            _intersectionStyle = intersectionStyle;
            _rightStyle = rightStyle;
            _leftVerticalBorder = leftVerticalBorder;
            _insideVerticalBorder = insideVerticalBorder;
            _rightVerticalBorder = rightVerticalBorder;
        }

        protected HorizontalBorder(string lineStyle)
        {
            _lineStyle = lineStyle;
        }

        public int LineStyleWidth => _lineStyle.GetWidth();
        
        public virtual void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            if(!_isEnable) return;
            
            if(_leftVerticalBorder.IsEnable) textWriter.Write(_leftStyle);
            
            var items = textTableLayout
                .Columns
                .Select(column => string.Concat(Enumerable.Repeat(_lineStyle, textTableLayout.GetColumnWidth(column) / _lineStyle.GetWidth())))
                .ToList();

            textWriter.Write(_insideVerticalBorder.IsEnable
                ? string.Join(_intersectionStyle, items)
                : string.Join(string.Empty, items));

            if(_rightVerticalBorder.IsEnable) textWriter.Write(_rightStyle);
            
            textWriter.WriteLine();
        }
    }
}