using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Hdc.Collections.ObjectModel;
using Hdc.ComponentModel;
using Hdc.Linq;
using Hdc.Mercury;
using Hdc.Mvvm;
using Hdc.Reactive.Linq;
using Hdc.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using ODM.Domain;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using System;
using ODM.Presentation.ViewModels.Events;
using Shared;
using IEventAggregator = Hdc.Patterns.IEventAggregator;

// ReSharper disable ConvertToLambdaExpression

namespace ODM.Presentation.ViewModels.Inspection
{
    public class InspectionManagerViewModel : ViewModel, IViewModel
    {
        private bool _isZoomFitDisplayAreaEnabled;
        private bool _isZoomActualEnabled;
        private bool _displayAllDefectsOnImages;
        private bool _displayAllMeasurementInfosOnImages;
        //        private int _inspectionCounter;

        [Microsoft.Practices.Unity.Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public IEventAggregator EventAggregator { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public Microsoft.Practices.Prism.PubSubEvents.IEventAggregator PrismEventAggregator { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public IInspectionDomainService InspectionDomainService { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public IInspectService InspectService { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public IInspectionViewModelService InspectionViewModelService { get; set; }

        [InjectionMethod]
        public void Init()
        {
            DefectInfos = new BindableCollection<DefectInfoViewModel>();
            DefectInfosCollectionView = DefectInfos.GetDefaultCollectionView();

            MeasurementInfos = new BindableCollection<MeasurementInfoViewModel>();
            MeasurementInfosCollectionView = MeasurementInfos.GetDefaultCollectionView();

            // Init SurfaceMonitors
            SurfaceMonitors = new List<SurfaceMonitorViewModel>();

            int surfaceCount = 0;
            surfaceCount = 2;

            for (int i = 0; i < surfaceCount; i++)
            {
                var sm = new SurfaceMonitorViewModel
                         {
                             Index = i,
                             SurfaceTypeIndex = i,
                             DisplayDefectInfo = true,
                             DisplayAllDefectInfos = false,
                             DisplayMeasurementInfo = true,
                             DisplayAllMeasurementInfos = false,
                         };
                SurfaceMonitors.Add(sm);
            }

            //
            SelectSurfaceMonitorCommand = new DelegateCommand<SurfaceMonitorViewModel>(
                o =>
                {
                    if (SelectedSurfaceMonitor == o) return;

                    HideAll();

                    SelectedSurfaceMonitor = o;
                    SelectedSurfaceMonitor.IsSelected = true;

                    // Show DefectInfosCollectionView
                    if (_displayAllDefectsOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllDefectInfos = false);
                        SelectedSurfaceMonitor.DisplayAllDefectInfos = true;
                    }

                    DefectInfosCollectionView.Filter =
                        x =>
                        {
                            var di = (DefectInfoViewModel)x;
                            return di.SurfaceTypeIndex == SelectedSurfaceMonitor.SurfaceTypeIndex;
                        };
                    DefectInfosCollectionView.Refresh();

                    // Show MeasurementInfosCollectionView
                    if (_displayAllMeasurementInfosOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllMeasurementInfos = false);
                        SelectedSurfaceMonitor.DisplayAllMeasurementInfos = true;
                    }

                    MeasurementInfosCollectionView.Filter =
                        x =>
                        {
                            var di = (MeasurementInfoViewModel)x;
                            return di.SurfaceTypeIndex == SelectedSurfaceMonitor.SurfaceTypeIndex;
                        };
                    MeasurementInfosCollectionView.Refresh();

                    UpdateCommandStates();
                });

            SaveImageToFileCommand = new DelegateCommand(
                () =>
                {
                    InspectionViewModelService.OpenSaveImageToFileDialog(SelectedSurfaceMonitor.BitmapSource,
                        "SurfaceType_" + SelectedSurfaceMonitor.SurfaceTypeIndex);
                }, () => SelectedSurfaceMonitor != null);

            ShowAllMeasurementsCommand = new DelegateCommand(
                OnShowAllMeasurementsCommand);

            ShowGroupMeasurementsCommand = new DelegateCommand(
                OnShowGroupMeasurementsCommand);

            InspectImageFileCommand = new DelegateCommand(async () =>
                {
                    int surfaceTypeIndex = SelectedSurfaceMonitor.SurfaceTypeIndex;

                    var dialog = new OpenFileDialog()
                                 {
                                     Title = "Load image to surface " + surfaceTypeIndex,
                                     Filter =
                                         //                                     "BMP files (*.bmp)|*.bmp|" +
                                         //                                              "TIFF files (*.tif)|*.tif|" +
                                         //                                              "JPG files (*.jpg)|*.jpg|" +
                                         "All files (*.*)|*.*",
                                 };
                    var result = dialog.ShowDialog();

                    if (!result.HasValue || !result.Value) return;


                    DefectInfos.Clear();
                    MeasurementInfos.Clear();

                    var fileName = dialog.FileName;
                    await InspectService.InspectImageFileAsync(surfaceTypeIndex, fileName);
                }, () => SelectedSurfaceMonitor != null);

            StartCommand = new DelegateCommand(
                () => { InspectService.Start(); });

            StopCommand = new DelegateCommand(
                () => { InspectService.Reset(); });


            ZoomInCommand = new DelegateCommand(
                () =>
                {
                    if (SelectedSurfaceMonitor != null)
                    {
                        SelectedSurfaceMonitor.ZoomIn();
                    }
                    _isZoomFitDisplayAreaEnabled = false;
                    _isZoomActualEnabled = false;
                },
                () => SelectedSurfaceMonitor != null);

            ZoomOutCommand = new DelegateCommand(
                () =>
                {
                    if (SelectedSurfaceMonitor != null)
                    {
                        SelectedSurfaceMonitor.ZoomOut();
                    }
                    _isZoomFitDisplayAreaEnabled = false;
                    _isZoomActualEnabled = false;
                },
                () => SelectedSurfaceMonitor != null);

            ZoomFitCommand = new DelegateCommand(
                () =>
                {
                    _isZoomFitDisplayAreaEnabled = !_isZoomFitDisplayAreaEnabled;
                    if (_isZoomFitDisplayAreaEnabled)
                    {
                        if (SelectedSurfaceMonitor != null)
                        {
                            SelectedSurfaceMonitor.ZoomFitDisplayArea();
                        }
                    }
                    else
                    {
                        if (SelectedSurfaceMonitor != null)
                        {
                            SelectedSurfaceMonitor.ZoomFit();
                        }
                    }
                    _isZoomActualEnabled = false;
                },
                () => SelectedSurfaceMonitor != null);

            ZoomActualCommand = new DelegateCommand(
                () =>
                {
                    _isZoomActualEnabled = !_isZoomActualEnabled;
                    if (_isZoomActualEnabled)
                    {
                        if (SelectedSurfaceMonitor != null)
                        {
                            SelectedSurfaceMonitor.ZoomActual();
                        }
                    }
                    else
                    {
                        if (SelectedSurfaceMonitor != null)
                        {
                            SelectedSurfaceMonitor.ZoomFit();
                        }
                    }

                    _isZoomFitDisplayAreaEnabled = false;
                },
                () => SelectedSurfaceMonitor != null);

            // publish CommunicationInitializedEvent
            PrismEventAggregator
                .GetEvent<SplashFinishedEvent>()
                .Publish(new SplashFinishedEvent());

            InspectService.AcquisitionStartedEvent
                .ObserveOnDispatcher()
                .Subscribe(surfaceIndex =>
                           {
                               var surfaceMonitor = SurfaceMonitors[surfaceIndex];
                               surfaceMonitor.BitmapSource = null;
                               surfaceMonitor.DefectInfos = null;
                               surfaceMonitor.MeasurementInfos = null;
                               surfaceMonitor.InspectState = InspectState.Grabbing;
                               Debug.WriteLine("InspectState.Grabbing");
                               //                               surfaceMonitor.MeasurementInfos.Clear();
                               //                               surfaceMonitor.DefectInfos.Clear();

                               UpdateCommandStates();
                           });

            InspectService.AcquisitionCompletedEvent
                .ObserveOnDispatcher()
                .Subscribe(imageInfo =>
                           {
                               var surfaceMonitor = SurfaceMonitors[imageInfo.SurfaceTypeIndex];
                               surfaceMonitor.BitmapSource = null;
                               surfaceMonitor.DefectInfos = null;
                               surfaceMonitor.MeasurementInfos = null;
                               surfaceMonitor.InspectState = InspectState.Grabbed;
                               Debug.WriteLine("InspectState.Grabbed");

                               UpdateCommandStates();
                           });

            InspectService.CalibrationStartedEvent
                .ObserveOnDispatcher()
                .Subscribe(async surfaceIndex =>
                                 {
                                     var surfaceMonitor = SurfaceMonitors[surfaceIndex];
                                     surfaceMonitor.BitmapSource = null;
                                     surfaceMonitor.DefectInfos = null;
                                     surfaceMonitor.MeasurementInfos = null;
                                     surfaceMonitor.InspectState = InspectState.Calibrating;
                                     Debug.WriteLine("InspectState.Calibrating");

                                     UpdateCommandStates();
                                 });

            InspectService.CalibrationCompletedEvent
                .ObserveOnDispatcher()
                .Subscribe(async imageInfo =>
                                 {
                                     var surfaceMonitor = SurfaceMonitors[imageInfo.SurfaceTypeIndex];
                                     surfaceMonitor.InspectState = InspectState.Calibrated;
                                     Debug.WriteLine("InspectState.Calibrated");

                                     var bs = await imageInfo.ToBitmapSourceAsync();
                                     surfaceMonitor.BitmapSource = bs;

                                     UpdateCommandStates();
                                 });

            InspectService.InspectionStartedEvent
                .ObserveOnDispatcher()
                .Subscribe(x =>
                           {
                               var surfaceMonitor = SurfaceMonitors[x];
                               surfaceMonitor.InspectState = InspectState.Inspecting;
                               Debug.WriteLine("InspectState.Inspecting");

                               UpdateCommandStates();
                           });

            InspectService.InspectionCompletedEvent
                .ObserveOnDispatcher()
                .Subscribe(inspectInfo =>
                           {
                               DefectInfos.Clear();
                               MeasurementInfos.Clear();

                               var surfaceMonitor = SurfaceMonitors[inspectInfo.SurfaceTypeIndex];
                               surfaceMonitor.InspectState = inspectInfo.InspectInfo.DefectInfos.Count == 0
                                   ? InspectState.InspectedWithAccepted
                                   : InspectState.InspectedWithRejected;

                               var iiVm = inspectInfo.InspectInfo.ToViewModel();

                               surfaceMonitor.DefectInfos = iiVm.DefectInfos.OrderBy(x => x.GroupName).ToList();
                               surfaceMonitor.MeasurementInfos = iiVm.MeasurementInfos.OrderBy(x=>x.GroupName).ToList();

                               DefectInfos.AddRange(iiVm.DefectInfos);
                               MeasurementInfos.AddRange(iiVm.MeasurementInfos);

                               UpdateCommandStates();
                           });

            SwitchDisplayAllDefectInfosCommand = new DelegateCommand(
                () =>
                {
                    HideAll();

                    DefectInfosCollectionView.Filter = null;
                    DefectInfosCollectionView.Refresh();

                    if (_displayAllDefectsOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllDefectInfos = true);
                    }

                    UpdateCommandStates();
                });

            SwitchDisplayAllDefectInfosOnImagesCommand = new DelegateCommand(
                () =>
                {
                    _displayAllDefectsOnImages = !_displayAllDefectsOnImages;

                    if (!_displayAllDefectsOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllDefectInfos = false);
                        return;
                    }

                    if (SelectedSurfaceMonitor == null)
                        SurfaceMonitors.ForEach(x => x.DisplayAllDefectInfos = true);
                    else
                        SelectedSurfaceMonitor.DisplayAllDefectInfos = true;

                    UpdateCommandStates();
                });


            SwitchDisplayAllMeasurementInfosCommand = new DelegateCommand(
                () =>
                {
                    HideAll();

                    MeasurementInfosCollectionView.Filter = null;
                    MeasurementInfosCollectionView.Refresh();

                    if (_displayAllMeasurementInfosOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllMeasurementInfos = true);
                    }

                    UpdateCommandStates();
                });

            SwitchDisplayAllMeasurementInfosOnImagesCommand = new DelegateCommand(
                () =>
                {
                    _displayAllMeasurementInfosOnImages = !_displayAllMeasurementInfosOnImages;

                    if (!_displayAllMeasurementInfosOnImages)
                    {
                        SurfaceMonitors.ForEach(x => x.DisplayAllMeasurementInfos = false);
                        return;
                    }

                    if (SelectedSurfaceMonitor == null)
                        SurfaceMonitors.ForEach(x => x.DisplayAllMeasurementInfos = true);
                    else
                        SelectedSurfaceMonitor.DisplayAllMeasurementInfos = true;

                    UpdateCommandStates();
                });
        }

        private void OnShowGroupMeasurementsCommand()
        {
            if (SelectedMeasurementInfo == null)
                return;

            var surfaceTypeIndex = SelectedMeasurementInfo.SurfaceTypeIndex;
            var groupIndex = SelectedMeasurementInfo.GroupName;

            HideAll();

            Predicate<object> filter = x =>
                                       {
                                           var mi = ((MeasurementInfoViewModel)x);
                                           return mi.SurfaceTypeIndex == surfaceTypeIndex && mi.GroupName == groupIndex;
                                       };

            MeasurementInfosCollectionView.Filter = filter;
            MeasurementInfosCollectionView.Refresh();
            SelectedMeasurementInfo = null;

            var sm = SurfaceMonitors[surfaceTypeIndex];
            sm.MeasurementInfosCollectionView.Filter = filter;
            sm.MeasurementInfosCollectionView.Refresh();
            sm.SelectedMeasurementInfo = null;
            sm.DisplayAllMeasurementInfos = true;
        }

        private void OnShowAllMeasurementsCommand()
        {
            HideAll();
            foreach (var surfaceMonitorViewModel in SurfaceMonitors)
            {
                if (surfaceMonitorViewModel.MeasurementInfosCollectionView != null)
                {
                    surfaceMonitorViewModel.MeasurementInfosCollectionView.Filter = null;
                    surfaceMonitorViewModel.MeasurementInfosCollectionView.Refresh();
                }
                surfaceMonitorViewModel.DisplayAllMeasurementInfos = false;
            }

            SelectedMeasurementInfo = null;
            MeasurementInfosCollectionView.Filter = null;
            MeasurementInfosCollectionView.Refresh();
        }

        private void HideAll()
        {
            SelectedSurfaceMonitor = null;
            SurfaceMonitors.ForEach(x =>
                                    {
                                        x.IsSelected = false;
                                        x.ZoomFit();
                                    });

            _isZoomFitDisplayAreaEnabled = false;
            _isZoomActualEnabled = false;
        }

        //        public DelegateCommand ManualGrabCommand { get; set; }
        public DelegateCommand ZoomInCommand { get; set; }
        public DelegateCommand ZoomOutCommand { get; set; }
        public DelegateCommand ZoomFitCommand { get; set; }
        public DelegateCommand ZoomActualCommand { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }

        public BindableCollection<DefectInfoViewModel> DefectInfos { get; set; }
        public ICollectionView DefectInfosCollectionView { get; set; }

        public BindableCollection<MeasurementInfoViewModel> MeasurementInfos { get; set; }
        public ICollectionView MeasurementInfosCollectionView { get; set; }


        private DefectInfoViewModel _selectedDefectInfo;

        public DefectInfoViewModel SelectedDefectInfo
        {
            get { return _selectedDefectInfo; }
            set
            {
                if (Equals(_selectedDefectInfo, value)) return;
                _selectedDefectInfo = value;
                RaisePropertyChanged(() => SelectedDefectInfo);

                foreach (var surfaceMonitor in SurfaceMonitors)
                {
                    surfaceMonitor.SelectedDefectInfo = null;

                    if (surfaceMonitor.DefectInfos == null)
                        continue;

                    foreach (var di in surfaceMonitor.DefectInfos)
                    {
                        if (di == SelectedDefectInfo)
                        {
                            surfaceMonitor.SelectedDefectInfo = SelectedDefectInfo;
                        }
                    }
                }
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

                foreach (var surfaceMonitor in SurfaceMonitors)
                {
                    surfaceMonitor.SelectedMeasurementInfo = null;

                    if (surfaceMonitor.MeasurementInfos == null)
                        continue;

                    foreach (var di in surfaceMonitor.MeasurementInfos)
                    {
                        if (di == SelectedMeasurementInfo)
                        {
                            surfaceMonitor.SelectedMeasurementInfo = SelectedMeasurementInfo;
                        }
                    }
                }
            }
        }


        private void UpdateCommandStates()
        {
            ZoomActualCommand.RaiseCanExecuteChanged();
            ZoomFitCommand.RaiseCanExecuteChanged();
            ZoomInCommand.RaiseCanExecuteChanged();
            ZoomOutCommand.RaiseCanExecuteChanged();
            SaveImageToFileCommand.RaiseCanExecuteChanged();
            InspectImageFileCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand<SurfaceMonitorViewModel> SelectSurfaceMonitorCommand { get; set; }
        public DelegateCommand SaveImageToFileCommand { get; set; }
        public DelegateCommand ShowAllMeasurementsCommand { get; set; }
        public DelegateCommand ShowGroupMeasurementsCommand { get; set; }
        public DelegateCommand InspectImageFileCommand { get; set; }

        /// <summary>
        /// just display defect infos in list
        /// </summary>
        public DelegateCommand SwitchDisplayAllDefectInfosCommand { get; set; }

        /// <summary>
        /// show defects in images
        /// </summary>
        public DelegateCommand SwitchDisplayAllDefectInfosOnImagesCommand { get; set; }

        /// <summary>
        /// just display defect infos in list
        /// </summary>
        public DelegateCommand SwitchDisplayAllMeasurementInfosCommand { get; set; }

        /// <summary>
        /// show defects in images
        /// </summary>
        public DelegateCommand SwitchDisplayAllMeasurementInfosOnImagesCommand { get; set; }

        public IList<SurfaceMonitorViewModel> SurfaceMonitors { get; private set; }

        private SurfaceMonitorViewModel _selectedSurfaceMonitor;

        public SurfaceMonitorViewModel SelectedSurfaceMonitor
        {
            get { return _selectedSurfaceMonitor; }
            set
            {
                if (Equals(_selectedSurfaceMonitor, value)) return;
                _selectedSurfaceMonitor = value;
                RaisePropertyChanged(() => SelectedSurfaceMonitor);
            }
        }
    }


    // ReSharper restore ConvertToLambdaExpression
}