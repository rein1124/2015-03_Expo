using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using HalconDotNet;
using Hdc.Windows;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Halcon
{
    public static class HImageExtensions
    {
        public static int GetWidth(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return w;
        }

        public static int GetHeight(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return h;
        }

        public static Int32Size GetSize(this HImage image)
        {
            int w, h;
            image.GetImageSize(out w, out h);
            return new Int32Size(w, h);
        }

        public static HImage ReduceDomainForRing(this HImage hImage, double centerX, double centerY, double innerRadius,
                                                 double outerRadius)
        {
            var innerCircle = new HRegion();
            innerCircle.GenCircle(centerY, centerX, innerRadius);

            var outerCircle = new HRegion();
            outerCircle.GenCircle(centerY, centerX, outerRadius);

            var ring = outerCircle.Difference(innerCircle);
            var reducedImage = hImage.ChangeDomain(ring);

            innerCircle.Dispose();
            outerCircle.Dispose();
            ring.Dispose();

//            reducedImage.CropDomain()
//                      .ToBitmapSource()
//                      .SaveToJpeg("_EnhanceEdgeArea_Domain.jpg");

            return reducedImage;
        }

        public static HImage PaintGrayOffset(this HImage imageSource, HImage imageDestination,
                                             int offsetRow, int offsetColumn)
        {
            HObject image;
            HDevelopExport.Singletone.PaintGrayOffset(imageSource, imageDestination, out image, offsetRow, offsetColumn);
            return new HImage(image);
        }

        public static HImage ToHImage(this BitmapSource bitmapSource)
        {
            var stride = bitmapSource.PixelWidth;

            int bufferSize = stride*bitmapSource.PixelHeight;
            IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

            bitmapSource.CopyPixels(Int32Rect.Empty, bufferPtr, bufferSize, bitmapSource.PixelWidth);


            var hImage = new HImage("byte", bitmapSource.PixelWidth, bitmapSource.PixelHeight, bufferPtr);

            Marshal.FreeHGlobal(bufferPtr);

            return hImage;
        }

        [Obsolete]
        public static HImage AddImagesWithFullDomain(this HImage image1, HImage image2)
        {
            var imageFull1 = image1.FullDomain();
            var imageFull2 = image2.FullDomain();
            return imageFull1.AddImage(imageFull2, new HTuple(1), new HTuple(0));
        }

        public static void SaveTiffWithPaintRegion(this HObject imageHObject, HObject regionHObject, double foreground,
                                                   double background, string fileName)
        {
            SaveTiffWithPaintRegion(new HImage(imageHObject), new HRegion(regionHObject), foreground, background,
                fileName);
        }

        public static void SaveTiffWithPaintRegion(this HImage image, HRegion region, double foreground,
                                                   double background, string fileName)
        {
            var imagePainted = image.PaintRegion(region, foreground, "fill");
            imagePainted.WriteImage("tiff", background, fileName);
            imagePainted.Dispose();
        }

        public static HImage ToHImage(this HObject hObject)
        {
            return new HImage(hObject);
        }

        public static void WriteImageOfTiff(this HImage image, string fileName, double background = 0)
        {
            string finalFileName = fileName;
            if (!fileName.EndsWith(".tif"))
                finalFileName += ".tif";

            var count = image.CountObj();
            image.WriteImage("tiff", background, finalFileName);
        }

        public static void WriteImageOfTiffOfCropDomain(this HImage image, string fileName, double background = 0)
        {
            var cropDomainImage = image.CropDomain();
            cropDomainImage.WriteImageOfTiff(fileName);
            cropDomainImage.Dispose();
        }

        public static void WriteImageOfTiffLzw(this HImage image, string fileName, double background = 0)
        {
            string finalFileName = fileName;
            if (!fileName.EndsWith(".tif"))
                finalFileName += ".tif";

            image.WriteImage("tiff lzw", background, finalFileName);
        }

        public static void WriteImageOfTiffLzwOfCropDomain(this HImage image, string fileName, double background = 0)
        {
            var cropDomain = image.CropDomain();
            cropDomain.WriteImageOfTiffLzw(fileName);
            cropDomain.Dispose();
        }

        public static void WriteImageOfTiffLzwOfCropDomain(this HImage image, HRegion domain, string fileName,
                                                           double background = 0)
        {
            var changeDomain = image.ChangeDomain(domain);
            changeDomain.WriteImageOfTiffLzwOfCropDomain(fileName);
            changeDomain.Dispose();
        }

        public static void WriteImageOfJpeg(this HImage image, string fileName, double background = 0)
        {
            string finalFileName = fileName;
            if (!fileName.EndsWith(".jpg"))
                finalFileName += ".jpg";

            image.WriteImage("jpeg", background, finalFileName);
        }

        public static void WriteImageOfBmp(this HImage image, string fileName, double background = 0)
        {
            string finalFileName = fileName;
            if (!fileName.EndsWith(".bmp"))
                finalFileName += ".bmp";

            image.WriteImage("bmp", background, finalFileName);
        }

        public static void WriteImageOfPng(this HImage image, string fileName, double background = 0)
        {
            string finalFileName = fileName;
            if (!fileName.EndsWith(".png"))
                finalFileName += ".png";

            image.WriteImage("png", background, finalFileName);
        }

        public static HImage Calibrate(this HImage orignalHImage,
                                       string cameraParams,
                                       string cameraPose,
                                       Interpolation interpolation)
        {
            HObject hCalibImage;
            HTuple lengthPerPixelX, lengthPerPixelY;
            HDevelopExport.Singletone.Calibrate(orignalHImage, out hCalibImage, cameraParams, cameraPose,
                interpolation.ToHalconString(),
                out lengthPerPixelX, out lengthPerPixelY);

            var hi = new HImage(hCalibImage);
            return hi;
        }

        public static HImage CropRectangle1(this HImage image,
                                       HRectangle1 rectangle1)
        {
            var croppedImage = image.CropRectangle1(rectangle1.Row1, rectangle1.Column1, rectangle1.Row2,
                rectangle1.Column2);
            return croppedImage;
        }

        public static HImage ChangeDomainForRoiRectangle(this HImage image,
                                       Line line, double halfWidth)
        {
            var rectImage = HDevelopExport.Singletone.ChangeDomainForRectangle(
             image,
             line,
             halfWidth);
            return rectImage;
        }
    }
}