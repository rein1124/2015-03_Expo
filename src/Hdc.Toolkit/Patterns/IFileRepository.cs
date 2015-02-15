namespace Hdc.Patterns
{
    public interface IFileRepository<TData> : IFileImporter<TData>, IFileExporter<TData>
    {
    }
}