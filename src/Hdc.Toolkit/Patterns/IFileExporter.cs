namespace Hdc.Patterns
{
    public interface IFileExporter<in TData>
    {
        void Export(TData data, string fileName);
    }
}