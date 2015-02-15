namespace Hdc.Modularity
{
    public interface IComponentWriter
    {
        void Write(string fileName, ComponentValueEntry[] entries);
    }
}