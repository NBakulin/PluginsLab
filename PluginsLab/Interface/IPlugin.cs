namespace Interface
{
    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        void Transform(IMainApp app);
    }
}