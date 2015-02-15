using Hdc.Mv;
using Hdc.Mv.Inspection;

namespace MvInspection.Inspection
{
    public class SimInspector : IInspector
    {
        public bool Init()
        {
            return SimInspectorInteropApi.Init() == 0;
        }

        public bool LoadParameters()
        {
            return SimInspectorInteropApi.LoadParameters() == 0;
        }

        public void FreeObject()
        {
            SimInspectorInteropApi.FreeObject();
        }

        public InspectionInfo Inspect(ImageInfo imageInfo)
        {
            var defectInfos = new DefectInfo[1024];
            var measurementInfos = new MeasurementInfo[1024];

            var inspectInfo = SimInspectorInteropApi.Inspect(imageInfo, defectInfos, measurementInfos);

            var iiEntity = new InspectionInfo
            {
                Index = inspectInfo.Index,
                SurfaceTypeIndex = inspectInfo.SurfaceTypeIndex,
                HasError = (inspectInfo.HasError != 0),
            };

            for (int i = 0; i < inspectInfo.DefectsCount; i++)
            {
                var di = defectInfos[i];
//                di.X = di.X - di.Width/2;
//                di.Y = di.Y - di.Height/2;
                iiEntity.DefectInfos.Add(di);
            }

            for (int i = 0; i < inspectInfo.MeasurementsCount; i++)
            {
                var di = measurementInfos[i];
                iiEntity.MeasurementInfos.Add(di);
            }

            return iiEntity;
        }

        public void Dispose()
        {
            FreeObject();
        }
    }
}