using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvInspection.ImageAcquisition;

namespace MvInspection.Matrox.NativeApi.Interop.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SimMatroxInteropApi.InitGrabDevice();
            var ii1 = SimMatroxInteropApi.GrabSingleFrame();
            var ii2 = SimMatroxInteropApi.GrabSingleFrameFromFile(@"sample\SurfaceFront_720x1280.bmp");
            SimMatroxInteropApi.FreeDevice();
        }
    }
}
