namespace ClearCode
{
    public interface IClearCodeConfiguration
    {
        string[] ToRemoveDirectories { get; }
        string[] ToRemoveFiles { get; }
        IDestination[] Destinations { get; }
    }

    public interface IDestination
    {
        string Label { get; }
        string Path { get; }
    }
}