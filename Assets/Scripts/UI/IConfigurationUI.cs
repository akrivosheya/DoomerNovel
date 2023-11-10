namespace UI
{
    public interface IConfigurationUI
    {
        public bool Changed { get; }
        public void OnApplyConfiguration();
    }
}
