namespace FluentTextTable
{
    internal interface ITextTableWriter
    {
        void Write(char c);
        void Write(string s);
        void WriteLine(string s);
        void WriteLine();
    }
}