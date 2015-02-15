namespace MvInspection.ImageAcquisition
{
    public interface IImageLoader
    {
        ImageInfo LoadFromFile(string fileName);
    }
}