namespace Hdc.Mercury.Navigation
{
    public interface IRecorder
    {
        void Undo();
        void Redo();
    }
}