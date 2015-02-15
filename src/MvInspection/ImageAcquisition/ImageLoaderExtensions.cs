using System.Threading.Tasks;

namespace MvInspection.ImageAcquisition
{
    public static class ImageLoaderExtensions
    {
        public static Task<ImageInfo> LoadFromFileAsync(this IImageLoader imageLoader, string fileName)
        {
            return Task.Run(() => imageLoader.LoadFromFile(fileName));
        }
    }
}