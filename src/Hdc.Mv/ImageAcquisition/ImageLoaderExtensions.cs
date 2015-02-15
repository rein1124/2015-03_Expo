using System.Threading.Tasks;
using Hdc.Mv.Inspection;

namespace Hdc.Mv.ImageAcquisition
{
    public static class ImageLoaderExtensions
    {
        public static Task<ImageInfo> LoadFromFileAsync(this IImageLoader imageLoader, string fileName)
        {
            return Task.Run(() => imageLoader.LoadFromFile(fileName));
        }
    }
}