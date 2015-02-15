namespace Hdc.Patterns
{
    public interface IFileImporter<out TData>
    {
        TData Import(string fileName);
    }
}