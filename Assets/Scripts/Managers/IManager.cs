namespace Managers
{
    public interface IManager
    {
        public bool IsReady { get; }

        public void Initialize();
    }
}
