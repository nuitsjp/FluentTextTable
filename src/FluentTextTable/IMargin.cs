using System.IO;

namespace FluentTextTable
{
    public interface IMargin
    {
        void Write(TextWriter textWriter);
    }
}