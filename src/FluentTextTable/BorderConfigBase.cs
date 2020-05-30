namespace FluentTextTable
{
    public class BorderConfigBase
    {
        public bool IsEnable { get; private set; } = true;

        public void Disable()
        {
            IsEnable = false;
        }
    }
}