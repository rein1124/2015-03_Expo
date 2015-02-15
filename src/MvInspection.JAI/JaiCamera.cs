using System.Threading;
using Jai_FactoryDotNET;

namespace MvInspection.ImageAcquisition
{
    public class JaiCamera : ICamera
    {
        // Main factory object
        private readonly CFactory _myFactory = new CFactory();

        // Opened camera object
        private CCamera _myCamera;

        private int _indexCounter;

        public bool Init()
        {
            // Open the factory with the default Registry database
            var error = _myFactory.Open("");
            if (error != Jai_FactoryWrapper.EFactoryError.Success) 
                return false;

            // Discover GigE and/or generic GenTL devices using myFactory.UpdateCameraList(in this case specifying Filter Driver for GigE cameras).
            _myFactory.UpdateCameraList(Jai_FactoryDotNET.CFactory.EDriverType.FilterDriver);

            // Open the camera - first check for GigE devices
            for (int i = 0; i < _myFactory.CameraList.Count; i++)
            {
                _myCamera = _myFactory.CameraList[i];
                Jai_FactoryWrapper.EFactoryError eFactoryError = _myCamera.Open();
                if (Jai_FactoryWrapper.EFactoryError.Success == eFactoryError)
                {
                    break;
                }
                else
                {
                    if (i == (_myFactory.CameraList.Count - 1))
                        return false;
                }
            }

            if (_myCamera.GetNode("TriggerSelector") != null)
            {
                // Here we assume that this is the GenICam SFNC way of setting up the trigger
                // To switch to Continuous the following is required:
                // TriggerSelector=FrameStart
                // TriggerMode[TriggerSelector]=On
                // TriggerSource[TriggerSelector]=Software
                _myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                _myCamera.GetNode("TriggerMode").Value = "On";
                _myCamera.GetNode("TriggerSource").Value = "Software";
            }

            error = _myCamera.StartImageAcquisition(false, 5);
            if (error != Jai_FactoryWrapper.EFactoryError.Success) 
                return false;

            return true;
        }

        public ImageInfo Acquisition()
        {
            _evt = new AutoResetEvent(false);

            _myCamera.NewImageDelegate += HandleImage;

            if (_myCamera.GetNode("TriggerSoftware") != null)
            {
                _myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                _myCamera.GetNode("TriggerSoftware").ExecuteCommand();
            }

            _evt.WaitOne();
            _evt.Dispose();

            _myCamera.NewImageDelegate -= HandleImage;

            return _acqImage;
        }

        private ImageInfo _acqImage;
        private AutoResetEvent _evt;

        // Local callback function used for handle new images
        private void HandleImage(ref Jai_FactoryWrapper.ImageInfo imageInfo)
        {
            _acqImage = imageInfo.ToImageInfo();
            _acqImage.Index = _indexCounter;
            _indexCounter++;
            _evt.Set();
        }

        public void Dispose()
        {
            if (_myCamera != null)
            {
                _myCamera.StopImageAcquisition();
                _myCamera.Close();
            }
            if (_myFactory != null)
            {
                _myFactory.Close();
                _myFactory.Dispose();
            }
        }
    }
}