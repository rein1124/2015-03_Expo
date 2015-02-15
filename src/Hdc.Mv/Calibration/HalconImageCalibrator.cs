using System;
using System.Runtime.InteropServices;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Mv.Inspection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Calibration
{
    public class HalconImageCalibrator //: IImageCalibrator
    {
        public HImage CalibrateImage(HImage originalImage, Interpolation interpolation)
        {
            var cameraParamsFileName = "camera_params.cal";
            var cameraPoseFileName = "camera_pose.dat";

            HImage hImage;

            var sw2 = new NotifyStopwatch("HalconImageCalibrator.Calibrate()");
            hImage = originalImage.Calibrate(cameraParamsFileName, cameraPoseFileName, interpolation);
            sw2.Stop();
            sw2.Dispose();

            var sw = new NotifyStopwatch("HalconImageCalibrator.MirrorImage()");
            var mirroredImage = hImage.MirrorImage("row");
            mirroredImage = mirroredImage.MirrorImage("column");
            sw.Stop();
            sw.Dispose();

            return mirroredImage;
        }
    }
}