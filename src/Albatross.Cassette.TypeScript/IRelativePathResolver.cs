namespace Albatross.Cassette.TypeScript
{
    public interface IRelativePathResolver
    {
        string ToAbsolute(string relativePath);
    }
}