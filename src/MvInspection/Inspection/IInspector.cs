using System;
using MvInspection;

namespace MvInspection.Inspection
{
    public interface IInspector : IDisposable
    {
        bool Init(); // return true = successful, false = failed
        bool LoadParameters(); // return true = successful, false = failed
        void FreeObject();

        InspectionInfo Inspect(ImageInfo imageInfo);
    }
}