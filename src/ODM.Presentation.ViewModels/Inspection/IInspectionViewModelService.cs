using System.Windows.Media.Imaging;

namespace ODM.Presentation.ViewModels.Inspection
{
    public interface IInspectionViewModelService
    {
        void OpenSaveImageToFileDialog(BitmapSource bitmapSource, string surfaceTypeName);
    }
}