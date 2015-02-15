using System;
using System.Windows.Media.Imaging;
using PPH;

namespace ODM.Domain.Inspection
{
    public interface IImageSaveLoadService
    {
        StoredImageInfo StoreImage(string originalImageFileName);

        //StoredImageInfo StoreImage(Action<string> storeImageWithNewNameAction);

        StoredImageInfo StoreImage(Action<string> storeImageWithNewNameAction, string prefix = null, string ext = null);

        void DeleteImage(DateTime inspectDateTime, Guid imageId);

        BitmapImage GetBitmapImage(DateTime inspectDateTime, Guid imageId);

        string GetBitmapImageFileName(DateTime inspectDateTime, Guid imageId);
    }
}