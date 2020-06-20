using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public class HorizontalBorder
    {
        internal HorizontalBorder(
            bool isEnable, 
            char lineStyle,
            char leftStyle,
            char intersectionStyle,
            char rightStyle,
            VerticalBorder leftVerticalBorder,
            VerticalBorder insideVerticalBorder,
            VerticalBorder rightVerticalBorder)
        {
            IsEnable = isEnable;
            LineStyle = lineStyle;
            LeftStyle = leftStyle;
            IntersectionStyle = intersectionStyle;
            RightStyle = rightStyle;
            LeftVerticalBorder = leftVerticalBorder;
            InsideVerticalBorder = insideVerticalBorder;
            RightVerticalBorder = rightVerticalBorder;
        }

        protected HorizontalBorder()
        {
        }

        private char LeftStyle { get; }
        private char IntersectionStyle { get; }
        private char RightStyle { get; }
        private VerticalBorder LeftVerticalBorder { get; }
        private VerticalBorder InsideVerticalBorder { get; }
        private VerticalBorder RightVerticalBorder { get; }
        private bool IsEnable { get; }
        private char LineStyle { get; }
        
        internal virtual void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            if(!IsEnable) return;
            
            if(LeftVerticalBorder.IsEnable) textWriter.Write(LeftStyle);
            
            var items = new List<string>();
            foreach (var column in textTableLayout.Columns)
            {
                items.Add(new string(LineStyle, textTableLayout.GetWidthOf(column)));
            }

            textWriter.Write(InsideVerticalBorder.IsEnable
                ? string.Join(IntersectionStyle.ToString(), items)
                : string.Join(string.Empty, items));

            if(RightVerticalBorder.IsEnable) textWriter.Write(RightStyle);
            
            textWriter.WriteLine();
        }

    }
}