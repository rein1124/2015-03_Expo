using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using HalconDotNet;
using Hdc;
using Hdc.Diagnostics;
using Hdc.Mercury;
using Hdc.Mv;
using Hdc.Mv.Calibration;
using Hdc.Mv.Halcon;
using Hdc.Mv.ImageAcquisition;
using Hdc.Mv.Inspection;
using Hdc.Patterns;
using Hdc.Reactive;
using Microsoft.Practices.Unity;
using MvInspection;
using MvInspection.ImageAcquisition;
using ODM.Domain.Configs;

namespace ODM.Domain.Inspection
{
    internal class InspectService : IInspectService
    {
        private readonly ISubject<int> _acquisitionStartedEvent = new Subject<int>();
        private readonly ISubject<int> _inspectionStartedEvent = new Subject<int>();
        private readonly ISubject<ImageInfo> _acquisitionCompletedEvent = new Subject<ImageInfo>();
        private readonly ISubject<SurfaceInspectInfo> _inspectionCompletedEvent = new Subject<SurfaceInspectInfo>();
        private readonly ISubject<int> _calibrationStartedEvent = new Subject<int>();
        private readonly ISubject<ImageInfo> _calibrationCompletedEvent = new Subject<ImageInfo>();
        private InspectionController _inspectionController; // = new InspectionController();

        private int _grabReadyPlcEventCounter;

        private ICamera _camera;
        private int _inspectCounter = 0;
        private InspectionSchema _inspectionSchema;

        [Dependency]
        public IMachineProvider MachineProvider { get; set; }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        public IMachine Machine
        {
            get { return MachineProvider.Machine; }
        }

        public MachineConfig MachineConfig
        {
            get { return MachineConfigProvider.MachineConfig; }
        }

        [Dependency]
        public IInspectionDomainService InspectionDomainService { get; set; }

        [InjectionMethod]
        public void Init()
        {
            InitCameraAndInspector();

            Machine.Inspection_SurfaceFront_GrabReadyPlcEventDevice
                .Subscribe(async x =>
                                 {
                                     if (!x) return;

                                     _grabReadyPlcEventCounter++;
                                     Machine.Inspection_SurfaceFront_GrabReadyPlcEventDevice.WriteFalse();
                                     await AcquisitionAndInspectAsync(0);
                                 });

            Machine.Inspection_SurfaceBack_GrabReadyPlcEventDevice
                .Subscribe(async x =>
                                 {
                                     if (!x) return;

                                     _grabReadyPlcEventCounter++;
                                     Machine.Inspection_SurfaceBack_GrabReadyPlcEventDevice.WriteFalse();
                                     await AcquisitionAndInspectAsync(1);
                                 });

            Machine.Inspection_Forward_MotionStartedPlcEventDevice
                .WhereTrue()
                .Subscribe(x => { Machine.Inspection_Forward_MotionStartedPlcEventDevice.WhereFalse(); });
            Machine.Inspection_Forward_MotionFinishedPlcEventDevice
                .WhereTrue()
                .Subscribe(x => { Machine.Inspection_Forward_MotionFinishedPlcEventDevice.WhereFalse(); });
            Machine.Inspection_Backward_MotionStartedPlcEventDevice
                .WhereTrue()
                .Subscribe(x => { Machine.Inspection_Backward_MotionStartedPlcEventDevice.WhereFalse(); });
            Machine.Inspection_Backward_MotionFinishedPlcEventDevice
                .WhereTrue()
                .Subscribe(x => { Machine.Inspection_Backward_MotionFinishedPlcEventDevice.WhereFalse(); });
        }

        private async void InitCameraAndInspector()
        {
            if (MachineConfig.MV_SimulationAcquisitionEnabled)
            {
                _camera = new SimCamera(MachineConfigProvider.MachineConfig.MV_SimulationImageFileNames);
            }
            else
            {
                _camera = new E2VMatroxCamera();
            }

            if (!MachineConfig.MV_SimulationInspectorEnabled)
            {
                _inspectionSchema = InspectionController.GetInspectionSchema();
                _inspectionController = new InspectionController();
            }

            bool isSuccessful;

            isSuccessful = await Task.Run(() => _camera.Init());

            if (!isSuccessful)
                throw new Exception("Camera cannot init");
        }

        public async Task<int> AcquisitionAndInspectAsync(int surfaceTypeIndex)
        {
            // Acquisition
            Task.Run(() => _acquisitionStartedEvent.OnNext(surfaceTypeIndex));
            var imageInfo = await _camera.AcquisitionAsync();

            switch (surfaceTypeIndex)
            {
                case 0:
                    Machine.Inspection_SurfaceFront_GrabFinishedPcEventDevice.WriteTrue();
                    break;
                case 1:
                    Machine.Inspection_SurfaceBack_GrabFinishedPcEventDevice.WriteTrue();
                    break;
            }

            Task.Run(() =>
                     {
                         _acquisitionCompletedEvent.OnNext(imageInfo.ToEntity());
                         _calibrationStartedEvent.OnNext(surfaceTypeIndex);
                     });

            // Calibration

            HImage calibImage;
            if (MachineConfig.MV_SimulationCalibrationEnabled)
            {
                calibImage = imageInfo.To8BppHImage();
            }
            else
            {
                var swOriToHImageInfo = new NotifyStopwatch("imageInfo.To8BppHImage()");
                HImage oriImage = imageInfo.To8BppHImage();
                swOriToHImageInfo.Dispose();

                //                HImage reducedImage = oriImage.Rectangle1Domain(2000, 2000, 5000, 5000);

                var calib = new HalconImageCalibrator();
                var swCalib = new NotifyStopwatch("AcquisitionAndInspectAsync.CalibrateImage(imageInfo)");
                calibImage = calib.CalibrateImage(oriImage, MachineConfig.MV_CalibrationInterpolation);
                swCalib.Dispose();

                oriImage.Dispose();
            }

            var swCalibToImageInfo = new NotifyStopwatch("calibImage.ToImageInfo()");
            var calibImageInfo = calibImage.ToImageInfo();
            swCalibToImageInfo.Dispose();

            calibImageInfo.SurfaceTypeIndex = surfaceTypeIndex;

            Debug.WriteLine("SurfaceType " + surfaceTypeIndex + ": StartGrabAsync() Finished");

            Task.Run(() =>
                     {
                         _calibrationCompletedEvent.OnNext(calibImageInfo.ToEntity());
                         _inspectionStartedEvent.OnNext(surfaceTypeIndex);
                     });

            Task.Run(
                () =>
                {
                    InspectInfo inspectionInfo;
                    if (MachineConfigProvider.MachineConfig.MV_SimulationInspectorEnabled)
                    {
                        inspectionInfo = new InspectInfo();
                        calibImage.Dispose();
                    }
                    else
                    {
                        InspectionResult inspectionResult;

                        try
                        {
                            Debug.WriteLine("_inspectionController.Inspect() start");
                            var sw3 = new NotifyStopwatch("_inspectionController.Inspect()");
                            _inspectionController
                                .SetInspectionSchema(_inspectionSchema.DeepClone())
                                .SetImage(calibImage)
                                .CreateCoordinate()
                                .Inspect();
                            Debug.WriteLine("_inspectionController.Inspect() OK");
                            sw3.Dispose();
                            calibImage.Dispose();
                            inspectionResult = _inspectionController.InspectionResult;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("_inspectionController error.\n\n" + e.Message + "\n\n" +
                                            e.InnerException.Message);
                            Debug.WriteLine("_inspectionController error.\n\n" + e.Message + "\n\n" +
                                            e.InnerException.Message);

                            inspectionResult = new InspectionResult();
                        }

                        Debug.WriteLine("_inspectionController.GetInspectionInfo() Start");
                        inspectionInfo = inspectionResult.GetInspectionInfo(_inspectionController.Coordinate);
                        Debug.WriteLine("_inspectionController.GetInspectionInfo() OK");
                    }

                    var surfaceInspectInfo = new SurfaceInspectInfo()
                                             {
                                                 SurfaceTypeIndex = surfaceTypeIndex,
                                                 ImageInfo = calibImageInfo.ToEntity(),
                                                 InspectInfo = inspectionInfo,
                                                 WorkpieceIndex =
                                                     _inspectCounter/
                                                     MachineConfigProvider.MachineConfig
                                                     .MV_AcquisitionCountPerWorkpiece
                                             };

                    Task.Run(() => _inspectionCompletedEvent.OnNext(surfaceInspectInfo));

                    Debug.WriteLine("AcquisitionAndInspectAsync.HandleInspect() above");
                    HandleInspect(surfaceInspectInfo);
                });

            return calibImageInfo.Index;
        }

        private void HandleInspect(SurfaceInspectInfo surfaceInspectInfo)
        {
            Debug.WriteLine("Inspect Finished. SurfaceTypeIndex: " + surfaceInspectInfo.SurfaceTypeIndex);

            if (surfaceInspectInfo.InspectInfo.DefectInfos.Count <= 0)
            {
                InspectionDomainService.HandleSurfaceInspectionInfo(surfaceInspectInfo);

                switch (surfaceInspectInfo.SurfaceTypeIndex)
                {
                    case 0:
                        Machine.Inspection_SurfaceFront_InspectionFinishedWithAcceptedPcEventDevice.WriteTrue();
                        Debug.WriteLine(
                            "Inspect Finished. Inspection_SurfaceFront_InspectionFinishedWithAcceptedPcEventDevice.WriteTrue()");
                        break;
                    case 1:
                        Machine.Inspection_SurfaceBack_InspectionFinishedWithAcceptedPcEventDevice.WriteTrue();
                        Debug.WriteLine(
                            "Inspect Finished. Inspection_SurfaceBack_InspectionFinishedWithAcceptedPcEventDevice.WriteTrue()");
                        break;
                }

                if (MachineConfigProvider.MachineConfig.MV_SimulationInspectorEnabled)
                {
                    Machine.Production_TotalCountDevice.Value++;
                }
            }
            else
            {
                InspectionDomainService.HandleSurfaceInspectionInfo(surfaceInspectInfo);

                switch (surfaceInspectInfo.SurfaceTypeIndex)
                {
                    case 0:
                        Machine.Inspection_SurfaceFront_InspectionFinishedWithRejectedPcEventDevice.WriteTrue();
                        Debug.WriteLine(
                            "Inspect Finished. Inspection_SurfaceFront_InspectionFinishedWithRejectedPcEventDevice.WriteTrue()");
                        break;
                    case 1:
                        Machine.Inspection_SurfaceBack_InspectionFinishedWithRejectedPcEventDevice.WriteTrue();
                        Debug.WriteLine(
                            "Inspect Finished. Inspection_SurfaceBack_InspectionFinishedWithRejectedPcEventDevice.WriteTrue()");
                        break;
                }

                if (MachineConfigProvider.MachineConfig.MV_SimulationInspectorEnabled)
                {
                    Machine.Production_TotalCountDevice.Value++;
                    Machine.Production_TotalRejectCountDevice.Value++;
                }
            }

            _inspectCounter++;
        }

        public IObservable<int> AcquisitionStartedEvent
        {
            get { return _acquisitionStartedEvent; }
        }

        public IObservable<ImageInfo> AcquisitionCompletedEvent
        {
            get { return _acquisitionCompletedEvent; }
        }

        public IObservable<int> CalibrationStartedEvent
        {
            get { return _calibrationStartedEvent; }
        }

        public IObservable<ImageInfo> CalibrationCompletedEvent
        {
            get { return _calibrationCompletedEvent; }
        }

        public IObservable<int> InspectionStartedEvent
        {
            get { return _inspectionStartedEvent; }
        }

        public IObservable<SurfaceInspectInfo> InspectionCompletedEvent
        {
            get { return _inspectionCompletedEvent; }
        }

        public void Start()
        {
            _grabReadyPlcEventCounter = 0;

            MachineProvider.Machine.Production_StartCommandDevice.WriteTrue();
        }

        public void Stop()
        {
            MachineProvider.Machine.Production_StopCommandDevice.WriteTrue();
        }

        public void Reset()
        {
            MachineProvider.Machine.Production_ResetCommandDevice.WriteTrue();
        }

        public async void InspectImageFile(int surfaceTypeIndex, string fileName)
        {
            Task.Run(() => _calibrationStartedEvent.OnNext(surfaceTypeIndex));

            var sw = new NotifyStopwatch("InspectImageFile: new BitmapImage(fileName)");
            BitmapImage bi = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));
            sw.Dispose();

            var sw1 = new NotifyStopwatch("InspectImageFile: BitmapImage.ToImageInfoWith8Bpp()");
            Hdc.Mv.ImageInfo imageInfo = bi.ToImageInfoWith8Bpp();
            sw1.Dispose();

            imageInfo.Index = 0;
            imageInfo.SurfaceTypeIndex = surfaceTypeIndex;
            ImageInfo imageInfoEntity = imageInfo.ToEntity();

            await Task.Run(() => _calibrationCompletedEvent.OnNext(imageInfoEntity));

            Task.Run(() => _inspectionStartedEvent.OnNext(surfaceTypeIndex));

            await Task.Run(
                () =>
                {
                    var sw4 = new NotifyStopwatch("imageInfo.To8BppHImage()");
                    var to8BppHImage = imageInfo.To8BppHImage();
                    sw4.Dispose();

                    InspectInfo inspectionInfo;

                    if (MachineConfigProvider.MachineConfig.MV_SimulationInspectorEnabled)
                    {
                        inspectionInfo = new InspectInfo();
                    }
                    else
                    {
                        var sw3 = new NotifyStopwatch("inspector.Inspect()");
                        inspectionInfo = _inspectionController
                            .SetInspectionSchema()
                            .SetImage(to8BppHImage)
                            .CreateCoordinate()
                            .Inspect()
                            .GetInspectionInfo()
                            ;
                        sw3.Dispose();
                    }

                    var surfaceInspectInfo = new SurfaceInspectInfo()
                                             {
                                                 SurfaceTypeIndex = surfaceTypeIndex,
                                                 BitmapSource = bi,
                                                 InspectInfo = inspectionInfo,
                                                 WorkpieceIndex =
                                                     _inspectCounter/
                                                     MachineConfigProvider.MachineConfig
                                                     .MV_AcquisitionCountPerWorkpiece
                                             };

                    Task.Run(() => _inspectionCompletedEvent.OnNext(surfaceInspectInfo));

                    HandleInspect(surfaceInspectInfo);
                });
        }
    }
}