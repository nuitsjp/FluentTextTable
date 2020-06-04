namespace FluentTextTable
{
    public class BorderConfigBase
    {
        protected bool IsEnable { get; private set; } = true;

        public void Disable()
        {
            IsEnable = false;
        }
    }
}