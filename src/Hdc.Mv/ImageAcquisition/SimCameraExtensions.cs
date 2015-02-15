using System.Collections.Generic;

namespace Hdc.Mv.ImageAcquisition
{
    public static class SimCameraExtensions
    {
        public static void AddImageFileNames(this SimCamera simCamera , IEnumerable<string> imageFileNames)
        {
            foreach (var imageFileName in imageFileNames)
            {
                simCamera.AddImageFileName(imageFileName);
            }
        }
    }
}