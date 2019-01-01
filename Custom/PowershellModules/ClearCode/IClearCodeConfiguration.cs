namespace ClearCode
{
    public interface IClearCodeConfiguration
    {
        string[] ToRemoveDirectories { get; }
        string[] ToRemoveFiles { get; }
    }
}