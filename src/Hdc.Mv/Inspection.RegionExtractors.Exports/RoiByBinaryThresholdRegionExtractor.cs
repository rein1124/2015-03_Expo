using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class RoiByBinaryThresholdRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            if(!(LightDark==LightDark.Light || LightDark==LightDark.Dark))
                throw new InvalidOperationException("BinaryThresholdRegionExtractor.LightDark must be Light or Dark. Now is " + LightDark);

            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionOfRoiByBinaryThreshold(image,
                out foundRegionObject,
                LightDark.ToHalconString(),
                AreaMin,
                AreaMax,
                MoveRow,
                MoveColumn);

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public LightDark LightDark { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public int MoveRow { get; set; }
        public int MoveColumn { get; set; }
    }
}