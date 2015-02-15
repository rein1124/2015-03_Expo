using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HalconDotNet;

namespace Hdc.Mv.Halcon
{
    public static class HalconExtensions
    {
        public static HImage To8BppHImage(this ImageInfo imageInfo)
        {
            var hImage = new HImage("byte", imageInfo.PixelWidth, imageInfo.PixelHeight, imageInfo.BufferPtr);
            return hImage;
        }

        public static ImageInfo ToImageInfo(this HImage hImage)
        {
            int pixelHeight;
            int pixelWidth;
            string type;
            IntPtr intPtr = hImage.GetImagePointer1(out type, out pixelWidth, out pixelHeight);


            var stride = pixelWidth;
            int bufferSize = stride*pixelHeight;
            IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

            //            Marshal.Copy(new IntPtr[] { intPtr }, 0, bufferPtr, 0);
            var buffer = new byte[bufferSize];
            Marshal.Copy(intPtr, buffer, 0, bufferSize);
            Marshal.Copy(buffer, 0, bufferPtr, bufferSize);

            var bsi = new ImageInfo()
                      {
                          BitsPerPixel = 8,
                          BufferPtr = bufferPtr,
                          PixelHeight = pixelHeight,
                          PixelWidth = pixelWidth
                      };
            return bsi;
        }

        public static BitmapSource ToBitmapSource(this HImage hImage)
        {
            int pixelHeight;
            int pixelWidth;
            string type;
            IntPtr intPtr = hImage.GetImagePointer1(out type, out pixelWidth, out pixelHeight);

            var stride = pixelWidth;
            var size = stride*pixelHeight;

/*            int bufferSize = stride * pixelHeight;
            IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

            var buffer = new byte[bufferSize];
            Marshal.Copy(intPtr, buffer, 0, bufferSize);
            Marshal.Copy(buffer, 0, bufferPtr, bufferSize);

            var bs = BitmapSource.Create(
            pixelWidth, pixelHeight,
            96, 96,
            PixelFormats.Gray8,
            BitmapPalettes.Gray256,
            bufferPtr,
            size,
            stride);*/

            var bs = BitmapSource.Create(
                pixelWidth, pixelHeight,
                96, 96,
                PixelFormats.Gray8,
                BitmapPalettes.Gray256,
                intPtr,
                size,
                stride);

            return bs;
        }

        public static void HobjectToHimage(this HObject hobject, ref HImage image)
        {
            HTuple pointer, type, width, height;

            HOperatorSet.GetImagePointer1(hobject, out pointer, out type, out width, out height);
            image.GenImage1(type, width, height, pointer);
        }

        public static Line GetRoiLineFromRectangle2Phi(this HRectangle2 rectangle2)
        {
            return GetRoiLineFromRectangle2Phi(rectangle2.Row,
                rectangle2.Column,
                rectangle2.Phi,
                rectangle2.Length1);
        }

        public static RoiRectangle GetRoiRectangleFromSmallestRectangle2(this HRegion region)
        {
            var smallest = region.GetSmallestHRectangle2();
            var roiLine = smallest.GetRoiLineFromRectangle2Phi();
            var roiRect = new RoiRectangle
                          {
                              StartX = roiLine.X1,
                              StartY = roiLine.Y1,
                              EndX = roiLine.X2,
                              EndY = roiLine.Y2,
                              ROIWidth = smallest.Length2
                          };

            return roiRect;
        }

        public static RoiRectangle GetRoiRectangle(this HRectangle2 rectangle2)
        {
            var roiLine = rectangle2.GetRoiLineFromRectangle2Phi();
            var roiRect = new RoiRectangle
                          {
                              StartX = roiLine.X1,
                              StartY = roiLine.Y1,
                              EndX = roiLine.X2,
                              EndY = roiLine.Y2,
                              ROIWidth = rectangle2.Length2
                          };

            return roiRect;
        }

        public static Line GetRoiLineFromRectangle2Phi(double row, double column, double phi, double length1)
        {
            var angle = -phi / 3.141592654 * 180;
            var angleRevers = angle + 180;

            Vector length1Vector = new Vector(length1, 0);

            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            var v1 = matrix.Transform(length1Vector);

            Matrix matrix2 = new Matrix();
            matrix2.Rotate(angleRevers);
            var v2 = matrix2.Transform(length1Vector);

            var centerVector = new Vector(column, row);

            var v1Offset = v1 + centerVector;
            var v2Offset = v2 + centerVector;

            return new Line(v1Offset.X, v1Offset.Y, v2Offset.X, v2Offset.Y);
        }


        public static Point IntersectionWith(this Line line1, Line line2)
        {
            if (line1.GetCenterPoint() == new Point())
                return new Point();

            if (line2.GetCenterPoint() == new Point())
                return new Point();

            HTuple pX, pY, pp;
            HOperatorSet.IntersectionLines(line1.Y1, line1.X1, line1.Y2, line1.X2,
                line2.Y1, line2.X1, line2.Y2, line2.X2,
                out pY, out pX, out pp);

            if (pp != 0)
            {
                //                throw new HalconInspectorException();
                return new Point();
            }

            var p = new Point(pX, pY);
            return p;
        } 
    }
}