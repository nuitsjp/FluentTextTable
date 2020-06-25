using System.IO;

namespace FluentTextTable
{
    public class Margin : IMargin
    {
        private readonly int _value;

        public Margin(int value)
        {
            _value = value;
        }

        public void Write(TextWriter textWriter) => textWriter.Write(new string(' ', _value));
    }
}