namespace Hdc.Mvvm.Resources
{
    using System.Windows.Media;

    public interface IDrawingBrushLoader
    {
        DrawingBrush Load(string name);
    }
}