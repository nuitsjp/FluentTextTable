using System;

namespace FluentTextTable
{
    public class VerticalBorderConfig : IVerticalBorderConfig
    {
        private bool _isEnable  = true;
        private string _line  = "|";

        public void Disable()
        {
            _isEnable = false;
        }

        public IVerticalBorderConfig LineIs(string c)
        {
            _line = c;
            return this;
        }

        internal int LineWidth => _line.GetWidth();

        internal IVerticalBorder Build()
        {
            return new VerticalBorder(_isEnable, _line);
        }
    }
}