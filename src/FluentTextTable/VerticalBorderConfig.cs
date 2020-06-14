namespace FluentTextTable
{
    public class VerticalBorderConfig : IVerticalBorderConfig
    {
        private bool _isEnable  = true;
        private char _line  = '|';

        public void Disable()
        {
            _isEnable = false;
        }

        public IVerticalBorderConfig LineIs(char c)
        {
            _line = c;
            return this;
        }

        internal VerticalBorder Build()
        {
            return new VerticalBorder(_isEnable, _line);
        }
    }
}