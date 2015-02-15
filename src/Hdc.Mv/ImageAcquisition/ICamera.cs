using System;
using System.Threading.Tasks;
using Hdc.Mv.Inspection;

namespace Hdc.Mv.ImageAcquisition
{
    public interface ICamera : IDisposable
    {
        bool Init();

        ImageInfo Acquisition();
    }
}