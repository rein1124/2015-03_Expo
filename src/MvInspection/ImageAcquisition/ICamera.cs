using System;
using System.Threading.Tasks;
using MvInspection;

namespace MvInspection.ImageAcquisition
{
    public interface ICamera : IDisposable
    {
        bool Init();

        ImageInfo Acquisition();
    }
}