using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using Hdc.ComponentModel;
using Hdc.Mvvm;
using Microsoft.Practices.Prism.Commands;
using ODM.Domain.Inspection;

namespace ODM.Presentation.ViewModels.Inspection
{
    public class SurfaceMonitorViewModel : ViewModel
    {
        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (Equals(_index, value)) return;
                _index = value;
                RaisePropertyChanged(() => Index);
            }
        }

        private int _surfaceTypeIndex;

        public int SurfaceTypeIndex
        {
            get { return _surfaceTypeIndex; }
            set
            {
                if (Equals(_surfaceTypeIndex, value)) return;
                _surfaceTypeIndex = value;
                RaisePropertyChanged(() => SurfaceTypeIndex);
            }
        }

        private BitmapSource _bitmapSource;

        public BitmapSource BitmapSource
        {
            get { return _bitmapSource; }
            set
            {
                if (Equals(_bitmapSource, value)) return;
                _bitmapSource = value;
                RaisePropertyChanged(() => BitmapSource);
            }
        }

        private InspectState _inspectState;

        public InspectState InspectState
        {
            get { return _inspectState; }
            set
            {
                if (Equals(_inspectState, value)) return;
                _inspectState = value;
                RaisePropertyChanged(() => InspectState);
                RaisePropertyChanged(() => InspectStateTranslation);
                RaisePropertyChanged(() => IsGrabbing);
            }
        }

        public string InspectStateTranslation
        {
            get { return TranslateInspectState(_inspectState); }
        }

        private DelegateCommand _selectSurfaceCommand;

        public DelegateCommand SelectSurfaceCommand
        {
            get { return _selectSurfaceCommand; }
            set
            {
                if (Equals(_selectSurfaceCommand, value)) return;
                _selectSurfaceCommand = value;
                RaisePropertyChanged(() => SelectSurfaceCommand);
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (Equals(_isSelected, value)) return;
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        private bool _zoomInCommandChanged;

        public bool ZoomInCommandChanged
        {
            get { return _zoomInCommandChanged; }
            set
            {
                if (Equals(_zoomInCommandChanged, value)) return;
                _zoomInCommandChanged = value;
                RaisePropertyChanged(() => ZoomInCommandChanged);
            }
        }

        private bool _zoomOutCommandChanged;

        public bool ZoomOutCommandChanged
        {
            get { return _zoomOutCommandChanged; }
            set
            {
                if (Equals(_zoomOutCommandChanged, value)) return;
                _zoomOutCommandChanged = value;
                RaisePropertyChanged(() => ZoomOutCommandChanged);
            }
        }

        private bool _zoomFitCommandChanged;

        public bool ZoomFitCommandChanged
        {
            get { return _zoomFitCommandChanged; }
            set
            {
                if (Equals(_zoomFitCommandChanged, value)) return;
                _zoomFitCommandChanged = value;
                RaisePropertyChanged(() => ZoomFitCommandChanged);
            }
        }

        private bool _zoomActualCommandChanged;

        public bool ZoomActualCommandChanged
        {
            get { return _zoomActualCommandChanged; }
            set
            {
                if (Equals(_zoomActualCommandChanged, value)) return;
                _zoomActualCommandChanged = value;
                RaisePropertyChanged(() => ZoomActualCommandChanged);
            }
        }

        private bool _zoomFitDiplayAreaCommandChanged;

        public bool ZoomFitDiplayAreaCommandChanged
        {
            get { return _zoomFitDiplayAreaCommandChanged; }
            set
            {
                if (Equals(_zoomFitDiplayAreaCommandChanged, value)) return;
                _zoomFitDiplayAreaCommandChanged = value;
                RaisePropertyChanged(() => ZoomFitDiplayAreaCommandChanged);
            }
        }

        public void ZoomIn()
        {
            ZoomInCommandChanged = !ZoomInCommandChanged;
        }

        public void ZoomOut()
        {
            ZoomOutCommandChanged = !ZoomOutCommandChanged;
        }

        public void ZoomActual()
        {
            ZoomActualCommandChanged = !ZoomActualCommandChanged;
        }

        public void ZoomFit()
        {
            ZoomFitCommandChanged = !ZoomFitCommandChanged;
        }

        public void ZoomFitDisplayArea()
        {
            ZoomFitDiplayAreaCommandChanged = !ZoomFitDiplayAreaCommandChanged;
        }

        private IList<DefectInfoViewModel> _defectInfos;

        public IList<DefectInfoViewModel> DefectInfos
        {
            get { return _defectInfos; }
            set
            {
                if (Equals(_defectInfos, value)) return;
                _defectInfos = value;
                RaisePropertyChanged(() => DefectInfos);
            }
        }

        private IList<MeasurementInfoViewModel> _measurementInfos;

        public IList<MeasurementInfoViewModel> MeasurementInfos
        {
            get { return _measurementInfos; }
            set
            {
                if (Equals(_measurementInfos, value)) return;
                _measurementInfos = value;
                RaisePropertyChanged(() => MeasurementInfos);

                if (_measurementInfos == null)
                {
                    MeasurementInfosCollectionView = null;
                }
                else
                {
                    MeasurementInfosCollectionView = MeasurementInfos.GetDefaultCollectionView();
                }
                RaisePropertyChanged(() => MeasurementInfosCollectionView);
            }
        }

        //        private ICollectionView _measurementInfosCollectionView;
        //
        //        public ICollectionView MeasurementInfosCollectionView
        //        {
        //            get { return _measurementInfosCollectionView; }
        //            set
        //            {
        //                if (Equals(_measurementInfosCollectionView, value)) return;
        //                _measurementInfosCollectionView = value;
        //                RaisePropertyChanged(() => MeasurementInfosCollectionView);
        //            }
        //        }

        public ICollectionView MeasurementInfosCollectionView { get; set; }

        private bool _displayDefectInfo;

        public bool DisplayDefectInfo
        {
            get { return _displayDefectInfo; }
            set
            {
                if (Equals(_displayDefectInfo, value)) return;
                _displayDefectInfo = value;
                RaisePropertyChanged(() => DisplayDefectInfo);
            }
        }

        private bool _displayMeasurementInfo;

        public bool DisplayMeasurementInfo
        {
            get { return _displayMeasurementInfo; }
            set
            {
                if (Equals(_displayMeasurementInfo, value)) return;
                _displayMeasurementInfo = value;
                RaisePropertyChanged(() => DisplayMeasurementInfo);
            }
        }

        private bool _displayAllDefectInfos;

        public bool DisplayAllDefectInfos
        {
            get { return _displayAllDefectInfos; }
            set
            {
                if (Equals(_displayAllDefectInfos, value)) return;
                _displayAllDefectInfos = value;
                RaisePropertyChanged(() => DisplayAllDefectInfos);
            }
        }

        private bool _displayAllMeasurementInfos;

        public bool DisplayAllMeasurementInfos
        {
            get { return _displayAllMeasurementInfos; }
            set
            {
                if (Equals(_displayAllMeasurementInfos, value)) return;
                _displayAllMeasurementInfos = value;
                RaisePropertyChanged(() => DisplayAllMeasurementInfos);
            }
        }

        private DefectInfoViewModel _selectedDefectInfo;

        public DefectInfoViewModel SelectedDefectInfo
        {
            get { return _selectedDefectInfo; }
            set
            {
                if (Equals(_selectedDefectInfo, value)) return;
                _selectedDefectInfo = value;
                RaisePropertyChanged(() => SelectedDefectInfo);
            }
        }

        private MeasurementInfoViewModel _selectedMeasurementInfo;

        public MeasurementInfoViewModel SelectedMeasurementInfo
        {
            get { return _selectedMeasurementInfo; }
            set
            {
                if (Equals(_selectedMeasurementInfo, value)) return;
                _selectedMeasurementInfo = value;
                RaisePropertyChanged(() => SelectedMeasurementInfo);
            }
        }

        public void Reset()
        {
            //            Index = 0;
            //            SurfaceIndex = 0;
            //            SurfaceGroupIndex = 0;
            //            CameraIndex = 0;
            IsSelected = false;
            DefectInfos = null;
            InspectState = InspectState.Default;
            BitmapSource = null;
        }


        private static string TranslateInspectState(InspectState inspectState)
        {
            switch (inspectState)
            {
                case InspectState.Ready:
                    return "待命";
                case InspectState.Grabbing:
                    return "采集中";
                case InspectState.Grabbed:
                    return "采集完成";
                case InspectState.Calibrating:
                    return "校准中";
                case InspectState.Calibrated:
                    return "校准完成";
                case InspectState.Inspecting:
                    return "检测中";
                case InspectState.InspectedWithAccepted:
                    return "检测完成: 良品";
                case InspectState.InspectedWithRejected:
                    return "检测完成: 废品";
                default:
                    return string.Empty;
            }
        }

        public bool IsGrabbing
        {
            get { return InspectState == InspectState.Grabbing; }
        }
    }
}