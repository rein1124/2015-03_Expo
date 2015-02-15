using System.Windows.Media.Imaging;

namespace ODM.Domain.Inspection
{
    public interface IWorkpieceImageSaveLoadService
    {
        BitmapSource LoadImage(long id);
        //void SaveImage(BitmapSource source);
    }
}