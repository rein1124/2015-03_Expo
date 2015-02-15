using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hdc.Mv;
using Hdc.Mv.Inspection;
using MvInspection.Inspection;

namespace MvInspection.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {


            SimInspectorInteropApi.Init();
            SimInspectorInteropApi.LoadParameters();

            var dis = new DefectInfo[1024];
            var mis = new MeasurementInfo[1024];
            var ii= SimInspectorInteropApi.Inspect(new ImageInfo(), dis, mis);

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
            SimInspectorInteropApi.FreeObject();
        }
    }
}