using System;
using System.IO;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using HalconDotNet;
using Hdc.IO;
using Hdc.Linq;
using Hdc.Mv.Halcon;
using Hdc.Reflection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv
{
    public static class Ex
    {
        static Ex()
        {
        }

        public static Point ToPoint(this Vector vector)
        {
            return new Point(vector.X, vector.Y);
        }

        public static Vector ToVector(this Point point)
        {
            return new Vector(point.X, point.Y);
        }

        public static Point GetCenterPoint(this Circle circle)
        {
            return new Point(circle.CenterX, circle.CenterY);
        }

        public static Vector GetCenterVector(this Circle circle)
        {
            return new Vector(circle.CenterX, circle.CenterY);
        }

        public static double GetDistanceToOrigin(this Circle circle)
        {
            return circle.GetCenterVector().Length;
        }

        public static Vector GetVectorTo(this Circle circle1, Circle circle2)
        {
            var v1 = circle1.GetCenterVector();
            var v2 = circle2.GetCenterVector();
            var v1to2 = v1 - v2;
            return v1to2;
        }

        public static Vector GetVectorTo(this Point p1, Point p2)
        {
            var v1 = p1.ToVector();
            var v2 = p2.ToVector();
            var v = v1 - v2;
            return v;
        }

        public static Vector GetVectorTo(this Vector p1, Vector p2)
        {
            var v = p1 - p2;
            return v;
        }

        public static double GetDistanceTo(this Circle circle1, Circle circle2)
        {
            return circle1.GetVectorTo(circle2).Length;
        }

        public static Point GetPoint1(this Line line)
        {
            return new Point(line.X1, line.Y1);
        }

        public static Point GetPoint2(this Line line)
        {
            return new Point(line.X2, line.Y2);
        }

        public static Vector GetVector1(this Line line)
        {
            return new Vector(line.X1, line.Y1);
        }

        public static Vector GetVector2(this Line line)
        {
            return new Vector(line.X2, line.Y2);
        }

        public static Vector GetVectorFrom1To2(this Line line)
        {
            var v1 = line.GetVector1();
            var v2 = line.GetVector2();
            var v = v1 - v2;
            return v;
        }

        public static Vector GetVectorFrom2To1(this Line line)
        {
            var v1 = line.GetVector1();
            var v2 = line.GetVector2();
            var v = v2 - v1;
            return v;
        }

        public static double GetLength(this Line line)
        {
            return line.GetVectorFrom1To2().Length;
        }

        public static Vector Rotate(this Vector vector, double angle)
        {
            var matrix = new Matrix();
            matrix.Rotate(angle);
            var rotatedVector = matrix.Transform(vector);
            return rotatedVector;
        }

        public static double GetAngleTo(this Vector fromVector, Vector toVector)
        {
            return Vector.AngleBetween(fromVector, toVector);
        }

        public static double GetAngleToX(this Vector fromVector)
        {
            return Vector.AngleBetween(fromVector, new Vector(10000,0));
        }

        public static Point GetRelativePoint(this Point point, Line baseLine, double angle)
        {
            var vFromOriginToTarget = point.GetVectorTo(baseLine.GetPoint1());
            var vFromOriginToRight = baseLine.GetPoint2().GetVectorTo(baseLine.GetPoint1());
            var coordinateVector = vFromOriginToRight.Rotate(angle);
            // 0 degree mains: the line is X, direct to right. x>0, follow the clock.

            var angleBetweenTargetAndRight = vFromOriginToTarget.GetAngleTo(coordinateVector);

            var vectorNoAngle = new Vector(vFromOriginToTarget.Length, 0);
            var vectorIncludeAngle = vectorNoAngle.Rotate(0 - angleBetweenTargetAndRight);
            return vectorIncludeAngle.ToPoint();
        }

        public static Point CreateRelativeC(this Point point, Line baseLine, double angle)
        {
            var vFromOriginToTarget = point.GetVectorTo(baseLine.GetPoint1());
            var vFromOriginToRight = baseLine.GetPoint2().GetVectorTo(baseLine.GetPoint1());
            var coordinateVector = vFromOriginToRight.Rotate(angle);
            // 0 degree mains: the line is X, direct to right. x>0, follow the clock.

            var angleBetweenTargetAndRight = vFromOriginToTarget.GetAngleTo(coordinateVector);

            var vectorNoAngle = new Vector(vFromOriginToTarget.Length, 0);
            var vectorIncludeAngle = vectorNoAngle.Rotate(0 - angleBetweenTargetAndRight);
            return vectorIncludeAngle.ToPoint();
        }

        public static Point GetCenterPoint(this Line line)
        {
            return new Point((line.X1 + line.X2)/2.0, (line.Y1 + line.Y2)/2.0);
        }

        public static string ToNumbericString(this double value, int intCount = 4)
        {
            switch (intCount)
            {
                case 0:
                    throw new NotSupportedException();
                case 1:
                    return value.ToString("+0.000;-0.000");
                case 2:
                    return value.ToString("+00.000;-00.000");
                case 3:
                    return value.ToString("+000.000;-000.000");
                case 4:
                    return value.ToString("+0000.000;-0000.000");
                case 5:
                    return value.ToString("+00000.000;-00000.000");
                case 6:
                    return value.ToString("+000000.000;-000000.000");
            }
            return null;
        }

        public static double ToMillimeterFromPixel(this double value, double factor)
        {
            return value*factor/1000.0;
        }

        public static double ToMicrometerFromPixel(this double value, double factor)
        {
            return value*factor;
        }

        public static string ToNumbericStringInMillimeterFromPixel(this double value, double factor, int intCount = 4)
        {
            return value.ToMillimeterFromPixel(factor).ToNumbericString(intCount); //+" mm";
        }

        public static string ToNumbericStringInMicrometerFromPixel(this double value, double factor, int intCount = 4)
        {
            return value.ToMicrometerFromPixel(factor).ToNumbericString(intCount); // + " um";
        }

        public static string ToHalconString(this Polarity polarity)
        {
            string polarityString = null;

            switch (polarity)
            {
                case Polarity.All:
                    polarityString = "all";
                    break;
                case Polarity.Negative:
                    polarityString = "negative";
                    break;
                case Polarity.Positive:
                    polarityString = "positive";
                    break;
            }

            return polarityString;
        }


        public static string ToHalconString(this Transition transition)
        {
            string polarityString = null;

            switch (transition)
            {
                case Transition.All:
                    polarityString = "all";
                    break;
                case Transition.Negative:
                    polarityString = "negative";
                    break;
                case Transition.Positive:
                    polarityString = "positive";
                    break;
            }

            return polarityString;
        }


        public static string ToHalconString(this SelectionMode selectionMode)
        {
            string selectionModeString = null;

            switch (selectionMode)
            {
                case SelectionMode.Max:
                    selectionModeString = "max";
                    break;
                case SelectionMode.First:
                    selectionModeString = "first";
                    break;
                case SelectionMode.Last:
                    selectionModeString = "last";
                    break;
            }

            return selectionModeString;
        }


        public static string ToHalconString(this CircleDirect selectionMode)
        {
            string selectionModeString = null;

            switch (selectionMode)
            {
                case CircleDirect.Inner:
                    selectionModeString = "inner";
                    break;
                case CircleDirect.Outer:
                    selectionModeString = "outer";
                    break;
            }

            return selectionModeString;
        }


        public static HRegion GetRegion(this IRectangle2 rect)
        {
            var processRegion = new HRegion();
            processRegion.GenRectangle2(rect.Y, rect.X, rect.Angle, rect.HalfWidth, rect.HalfHeight);
            return processRegion;
        }

        public static void SaveCacheImagesForRegion(this HImage image, HRegion domain, HRegion region, string fileName)
        {
            var dir = typeof (Ex).Assembly.GetAssemblyDirectoryPath();
            var cacheDir = Path.Combine(dir, "CacheImages");

            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            var reducedImage = image.ReduceDomain(domain);
            var croppedImage = reducedImage.CropDomain();
            croppedImage.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Ori.tif");

            var paintRegion = reducedImage.PaintRegion(region, 255.0, "margin");
            var croppedImage2 = paintRegion.CropDomain();
            croppedImage2.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Paint.Margin.tif");

            var paintRegion2 = reducedImage.PaintRegion(region, 255.0, "fill");
            var croppedImage2b = paintRegion2.CropDomain();
            croppedImage2b.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Paint.Fill.tif");

            var reducedImage3 = image.ReduceDomain(region);
            var croppedImage3 = reducedImage3.CropDomain();
            croppedImage3.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Crop.tif");

            domain.Dispose();
            reducedImage.Dispose();
            reducedImage3.Dispose();
            croppedImage.Dispose();
            croppedImage2.Dispose();
            croppedImage2b.Dispose();
            croppedImage3.Dispose();
            paintRegion.Dispose();
        }

        public static void SaveCacheImagesForRegion(this HImage image, HRegion domain, HRegion includeRegion,
                                                    HRegion excludeRegion, string fileName)
        {
            var dir = typeof (Ex).Assembly.GetAssemblyDirectoryPath();
            var cacheDir = Path.Combine(dir, "CacheImages");

            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            var imageWidth = image.GetWidth();
            var imageHeight = image.GetHeight();

            // Domain.Ori
            var reducedImage = image.ChangeDomain(domain);
            var croppedImage = reducedImage.CropDomain();
            croppedImage.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Domain.Ori.tif");
            reducedImage.Dispose();
            croppedImage.Dispose();

            // Domain.PaintMargin
            var reducedImage4 = image.ChangeDomain(domain);
            var paintRegionImage = reducedImage4.PaintRegion(includeRegion, 250.0, "margin");
            var paintRegion2Image = paintRegionImage.PaintRegion(excludeRegion, 5.0, "margin");
            var croppedImage2 = paintRegion2Image.CropDomain();
            croppedImage2.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Domain.PaintMargin.tif");
            reducedImage4.Dispose();
            croppedImage2.Dispose();
            paintRegionImage.Dispose();
            paintRegion2Image.Dispose();

            // PaintFill
//            var paintRegion3Image = reducedImage.PaintRegion(includeRegion, 250.0, "fill");
//            var croppedImage2bImage = paintRegion3Image.CropDomain();
//            croppedImage2bImage.ToBitmapSource().SaveToTiff(cacheDir.CombilePath(fileName) + ".Domain.PaintFill.tif");
//            croppedImage2bImage.Dispose();

            // Domain.Crop
            var reducedImage3 = image.ChangeDomain(includeRegion);
            var croppedImage3 = reducedImage3.CropDomain();
            croppedImage3.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Domain.Crop.tif");
            reducedImage3.Dispose();
            croppedImage3.Dispose();

            // bin image in domain
            var row1 = domain.GetRow1();
            var column1 = domain.GetColumn1();
            var movedRegion = includeRegion.MoveRegion(-row1, -column1);

            var w = domain.GetWidth();
            var h = domain.GetHeight();
            var binImage = movedRegion.RegionToBin(255, 0, w, h);
            binImage.WriteImageOfTiff(cacheDir.CombilePath(fileName) + ".Domain.Bin.tif");
            binImage.Dispose();
            movedRegion.Dispose();

            // Full.Bin, 
            var binImage2 = includeRegion.RegionToBin(255, 0, imageWidth, imageHeight);
            binImage2.WriteImageOfJpeg(cacheDir.CombilePath(fileName) + ".Full.Bin.jpg");
            binImage2.Dispose();

            // Full.BinOnlyDomain
            var binImage3 = includeRegion.RegionToBin(255, 0, imageWidth, imageHeight);
            var reducedImage5 = binImage3.ReduceDomain(domain);
            var binOnlyDomainImage = image.Clone();
            binOnlyDomainImage.OverpaintGray(reducedImage5);
            binOnlyDomainImage.WriteImageOfJpeg(cacheDir.CombilePath(fileName) + ".Full.BinOnlyDomain.jpg");

            binImage3.Dispose();
            reducedImage5.Dispose();
            binOnlyDomainImage.Dispose();
        }

        public static string ToHalconString(this LightDark lightDark)
        {
            switch (lightDark)
            {
                case LightDark.Dark:
                    return "dark";
                case LightDark.Light:
                    return "light";
                case LightDark.Equal:
                    return "equal";
                case LightDark.NotEqual:
                    return "not_equal";
                default:
                    throw new InvalidOperationException("LightDark cannot convert to string");
            }
        }

        public static string ToHalconString(this SortMode sortMode)
        {
            switch (sortMode)
            {
                case SortMode.Character:
                    return "character";
                case SortMode.FirstPoint:
                    return "first_point";
                case SortMode.LastPoint:
                    return "last_point";
                case SortMode.LowerLeft:
                    return "lower_left";
                case SortMode.LowerRight:
                    return "lower_right";
                case SortMode.UpperLeft:
                    return "upper_left";
                case SortMode.UpperRight:
                    return "upper_right";
                default:
                    throw new InvalidOperationException("SortMode cannot convert to string");
            }
        }


        public static string ToHalconString(this Order order)
        {
            switch (order)
            {
                case Order.Increase:
                    return "true";
                case Order.Decrease:
                    return "false";
                default:
                    throw new InvalidOperationException("Order cannot convert to string");
            }
        }

        public static string ToHalconString(this RowOrCol rowOrCol)
        {
            switch (rowOrCol)
            {
                case RowOrCol.Row:
                    return "row";
                case RowOrCol.Column:
                    return "column";
                default:
                    throw new InvalidOperationException("RowOrCol cannot convert to string");
            }
        }


        public static string ToHalconString(this LogicOperation operation)
        {
            switch (operation)
            {
                case LogicOperation.And:
                    return "and";
                case LogicOperation.Or:
                    return "or";
                default:
                    throw new InvalidOperationException("LogicOperation cannot convert to string: " + operation);
            }
        }


        public static string ToHalconString(this MedianMaskType maskType)
        {
            switch (maskType)
            {
                case MedianMaskType.Circle:
                    return "circle";
                case MedianMaskType.Square:
                    return "square";
                default:
                    throw new InvalidOperationException("MedianMaskType cannot convert to string: " + maskType);
            }
        }


        public static string ToHalconString(this MedianMargin margin)
        {
            switch (margin)
            {
                case MedianMargin.Mirrored:
                    return "mirrored";
                case MedianMargin.Cyclic:
                    return "cyclic";
                case MedianMargin.Continued:
                    return "continued";
                default:
                    throw new InvalidOperationException("MedianMargin cannot convert to string: " + margin);
            }
        }

        public static string ToHalconString(this MaskShape maskShape)
        {
            switch (maskShape)
            {
                case MaskShape.Octagon:
                    return "octagon";
                case MaskShape.Rectangle:
                    return "rectangle";
                case MaskShape.Rhombus:
                    return "rhombus";
                default:
                    throw new InvalidOperationException("MaskShape cannot convert to string: " + maskShape);
            }
        }

        public static string ToHalconString(this SharpFeature feature)
        {
            switch (feature)
            {
                case SharpFeature.Area:
                    return "area";
                case SharpFeature.Bulkiness:
                    return "bulkiness";
                case SharpFeature.Circularity:
                    return "circularity";
                case SharpFeature.Row:
                    return "row";
                case SharpFeature.Column:
                    return "column";
                case SharpFeature.Row1:
                    return "row1";
                case SharpFeature.Column1:
                    return "column1";
                case SharpFeature.Row2:
                    return "row2";
                case SharpFeature.Column2:
                    return "column2";
                case SharpFeature.Convexity:
                    return "convexity";
                case SharpFeature.Height:
                    return "height";
                case SharpFeature.Width:
                    return "width";
                case SharpFeature.Roundness:
                    return "roundness";
                case SharpFeature.Rect2Len1:
                    return "rect2_len1";
                case SharpFeature.Rect2Len2:
                    return "rect2_len2";
                case SharpFeature.Rect2Phi:
                    return "rect2_phi";
                default:
                    throw new InvalidOperationException("SharpFeature cannot convert to string: " + feature);
            }
        }

        public static string ToHalconString(this SobelAmpFilterType filterType)
        {
            switch (filterType)
            {
                case SobelAmpFilterType.SumAbs:
                    return "sum_abs";
                case SobelAmpFilterType.SumAbsBinomial:
                    return "sum_abs_binomial";
                case SobelAmpFilterType.SumSqrt:
                    return "sum_sqrt";
                case SobelAmpFilterType.SumSqrtBinomial:
                    return "sum_sqrt_binomial";
                case SobelAmpFilterType.ThinMaxAbs:
                    return "thin_max_abs";
                case SobelAmpFilterType.ThinMaxAbsBinomial:
                    return "thin_max_abs_binomial";
                case SobelAmpFilterType.X:
                    return "x";
                case SobelAmpFilterType.XBinomial:
                    return "x_binomial";
                case SobelAmpFilterType.Y:
                    return "y";
                case SobelAmpFilterType.YBinomial:
                    return "y_binomial";
                default:
                    throw new InvalidOperationException("SobelAmpFilterType cannot convert to string: " + filterType);
            }
        }

        public static string ToHalconString(this Interpolation interpolation)
        {
            switch (interpolation)
            {
                case Interpolation.Bilinear:
                    return "bilinear";
                case Interpolation.NearestNeighbor:
                    return "nearest_neighbor";
                default:
                    throw new InvalidOperationException("Interpolation cannot convert to string: " + interpolation);
            }
        }

        public static string ToHalconString(this BoundaryType boundaryType)
        {
            switch (boundaryType)
            {
                case BoundaryType.Inner:
                    return "inner";
                case BoundaryType.InnerFilled:
                    return "inner_filled";
                case BoundaryType.Outer:
                    return "outer";
                default:
                    throw new InvalidOperationException("BoundaryType cannot convert to string: " + boundaryType);
            }
        }

        public static Line GetMiddleLineUsingAngle(this Line line1, Line line2)
        {
            var vectorAxisX = new Vector(10000, 0);

            var vector1A = new Vector(line1.X1, line1.Y1);
            var vector1B = new Vector(line1.X2, line1.Y2);
            var vector2A = new Vector(line2.X1, line2.Y1);
            var vector2B = new Vector(line2.X2, line2.Y2);

            var vector1BToA = vector1B - vector1A;
            var angle1 = Vector.AngleBetween(vector1BToA, vectorAxisX);
            var vector2BToA = vector2B - vector2A;
            var angle2 = Vector.AngleBetween(vector2BToA, vectorAxisX);

            var angleAvg = (angle1 + angle2)/2.0;

            var matrix1 = new Matrix();
            matrix1.Rotate(angle1 - angleAvg + 90);
            var vertical1BToA = matrix1.Transform(vector1BToA);


            var matrix2 = new Matrix();
            matrix2.Rotate(angle2 - angleAvg + 90);
            var vertical2BToA = matrix2.Transform(vector2BToA);

            var v1C = vertical1BToA + vector1A;
            var v2C = vertical2BToA + vector2A;
            var v2CB = vertical2BToA + vector2B;


            HTuple row, column, isOverlapping;
            HOperatorSet.IntersectionLines(vector1A.Y, vector1A.X, vector1B.Y, vector1B.X,
                vector2A.Y, vector2A.X, v2C.Y, v2C.X, out row, out column, out isOverlapping);

            HTuple rowB, columnB, isOverlappingB;
            HOperatorSet.IntersectionLines(vector1A.Y, vector1A.X, vector1B.Y, vector1B.X,
                vector2B.Y, vector2B.X, v2CB.Y, v2CB.X, out rowB, out columnB, out isOverlappingB);

            HTuple row2, column2, isOverlapping2;
            HOperatorSet.IntersectionLines(vector2A.Y, vector2A.X, vector2B.Y, vector2B.X,
                vector1A.Y, vector1A.X, v1C.Y, v1C.X, out row2, out column2, out isOverlapping2);


            var middle1X = (vector2B.X + columnB)/2.0;
            var middle1Y = (vector2B.Y + rowB)/2.0;

            var middle2X = (vector1A.X + column2)/2.0;
            var middle2Y = (vector1A.Y + row2)/2.0;

            return new Line(middle2X, middle2Y, middle1X, middle1Y);
        }

        public static Line Reverse(this Line line)
        {
            return new Line(line.X2, line.Y2, line.X1, line.Y1);
        }

        public static Line GetLine(this Circle circle, double angle)
        {
            var centerVector = circle.GetCenterVector();
            var leftVector = new Vector(circle.Radius, 0).Rotate(angle);
            var rightVector = new Vector(circle.Radius, 0).Rotate(angle - 180);
            var offsetLeft = leftVector + centerVector;
            var offsetRight = rightVector + centerVector;
            return new Line(offsetLeft.ToPoint(), offsetRight.ToPoint());
        }

        public static Line GetLine(this IRoiRectangle roiRectangle)
        {
            return new Line(roiRectangle.StartX,
                roiRectangle.StartY,
                roiRectangle.EndX,
                roiRectangle.EndY);
        }

        public static Line GetWidthLine(this IRoiRectangle roiRectangle)
        {
            var centerVector = roiRectangle.GetCenterVector();
            var linkLine = roiRectangle.GetLine().GetVectorFrom2To1().GetAngleToX();

            var leftVector = new Vector(roiRectangle.ROIWidth, 0).Rotate(linkLine - 90);
            var rightVector = new Vector(roiRectangle.ROIWidth, 0).Rotate(linkLine + 90);
            var offsetLeft = leftVector + centerVector;
            var offsetRight = rightVector + centerVector;
            return new Line(offsetLeft.ToPoint(),offsetRight.ToPoint());
        }

        public static Vector GetStartVector(this IRoiRectangle roiRectangle)
        {
            return new Vector(roiRectangle.StartX , roiRectangle.StartY);
        }

        public static Vector GetEndVector(this IRoiRectangle roiRectangle)
        {
            return new Vector(roiRectangle.EndX , roiRectangle.EndY);
        }

        public static Vector GetCenterVector(this IRoiRectangle roiRectangle)
        {
            return new Vector((roiRectangle.StartX + roiRectangle.EndX) / 2.0, (roiRectangle.StartY + roiRectangle.EndY) / 2.0);
        }

        public static HRegion GenRegion(this IRectangle2 rectangle2)
        {
            var region = new HRegion();
            region.GenRectangle2(
                rectangle2.Y, 
                rectangle2.X, 
                rectangle2.Angle,
                rectangle2.HalfWidth,
                rectangle2.HalfHeight);
            return region;
        }
    }
}