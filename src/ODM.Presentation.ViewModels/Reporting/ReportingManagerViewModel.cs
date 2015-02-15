using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hdc.Collections.ObjectModel;
using Hdc.ComponentModel;
using Hdc.IO;
using Hdc.Mvvm;
using Hdc.Mvvm.Dialogs;
using Hdc.Patterns;
using Hdc.Reflection;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using ODM.Domain.Reporting;
using ODM.Presentation.ViewModels.Reporting;

namespace ODM.Presentation.ViewModels.Inspection
{
    public class ReportingManagerViewModel : ViewModel, IViewModel
    {
        private bool _displayAllDefectsOnImages;
        private bool _isZoomFitDisplayAreaEnabled;
        private bool _isZoomActualEnabled;

        private readonly BindableCollection<WorkpieceInfoEntryViewModel> _WorkpieceInfoEntries =
            new BindableCollection<WorkpieceInfoEntryViewModel>();

        private readonly BindableCollection<DefectInfoViewModel> _defectInfos =
            new BindableCollection<DefectInfoViewModel>();

        private readonly BindableCollection<DateTime> _monthReportDateTimes = new BindableCollection<DateTime>();

        private readonly BindableCollection<DateTime> _dayReportDateTimes = new BindableCollection<DateTime>();

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        [Dependency]
        public IInspectionDomainService InspectionDomainService { get; set; }

        [Dependency]
        public IAskDialogService AskDialogService { get; set; }

        //        [Dependency]
        //        public IImageSaveLoadService ImageSaveLoadService { get; set; }

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        [Dependency]
        public IReportingDomainService ReportingDomainService { get; set; }

        [Dependency]
        public IPreviewReportingDialogService PreviewReportingDialogService { get; set; }

        [Dependency]
        public IMessageDialogService MessageDialogService { get; set; }


        // ReSharper disable ConvertToLambdaExpression
        [InjectionMethod]
        public void Init()
        {
            DefectInfosCollectionView = DefectInfos.GetDefaultCollectionView();

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

            CreateWorkpieceInfoCommand = new DelegateCommand(
                () =>
                {
                    var dialog = new OpenFileDialog()
                                 {
                                     Multiselect = false,
                                     Title = "Select Image File",
                                 };

                    var ret = dialog.ShowDialog();

                    if (ret != true)
                        return;

                    var fn = dialog.FileName;

                    var di = ServiceLocator.GetInstance<WorkpieceInfo>();
                    //di.StoreImage(fn);
                    di.InspectDateTime = DateTime.Now;
                    di.IsReject = true;

                    InspectionDomainService.AddWorkpieceInfo(di);
                });

            DeleteWorkpieceInfoCommand = new DelegateCommand<WorkpieceInfoEntryViewModel>(
                (di) =>
                {
                    if (di == null)
                    {
                        MessageBox.Show("Please select a WorkpieceInfo!");
                        return;
                    }

                    InspectionDomainService.DeleteWorkpieceInfo(di.Id);
                },
                (x) => { return SelectedWorkpieceInfo != null; });

            SelectWorkpieceInfoCommand = new DelegateCommand<WorkpieceInfoEntryViewModel>(
                (workpieceInfoEntry) =>
                {
                    //           var deferRefresh=  DefectInfosCollectionView.DeferRefresh();
                    DefectInfosCollectionView.Filter = null;
                    _defectInfos.Clear();

                    foreach (var surfaceMonitor in SurfaceMonitors)
                    {
                        surfaceMonitor.Reset();
                    }

                    HideAll();

                    UpdateCommandStates();

                    if (workpieceInfoEntry == null)
                        return;

                    var id = workpieceInfoEntry.Id;
                    var workpieceInfo = InspectionDomainService.GetWorkpieceInfoById(id);
                    var defVms = workpieceInfo.DefectInfos.Select(x => x.ToViewModel());
                    _defectInfos.AddRange(defVms);

                    for (int i = 0; i < workpieceInfo.StoredImageInfo.Count; i++)
                    {
                        var sii = workpieceInfo.StoredImageInfo[i];
                        var bs = sii.LoadImage();

                        var surfaceMonitor = SurfaceMonitors[sii.SurfaceTypeIndex];
                        surfaceMonitor.BitmapSource = bs;

                        var ds = _defectInfos.Where(x => x.SurfaceTypeIndex == surfaceMonitor.SurfaceTypeIndex).ToList();

                        if (ds.IsEmpty())
                        {
                            surfaceMonitor.DefectInfos = null;
                            surfaceMonitor.InspectState = InspectState.InspectedWithAccepted;
                        }
                        else
                        {
                            surfaceMonitor.DefectInfos = ds;
                            surfaceMonitor.InspectState = InspectState.InspectedWithRejected;
                        }
                    }

                    //            deferRefresh.Dispose();
                    DefectInfosCollectionView.Filter = null;
                    DefectInfosCollectionView.Refresh();

                    UpdateCommandStates();
                });

            EventAggregator
                .GetEvents<WorkpieceInfoAddedDomainEvent>()
                .Subscribe(evt =>
                           {
                               var entryVm = evt.WorkpieceInfo.ToEntry().ToViewModel();
                               WorkpieceInfoEntries.Add(entryVm);

                               UpdateCommandStates();
                           });

            EventAggregator
                .GetEvents<WorkpieceInfoRemovedDomainEvent>()
                .Subscribe(evt =>
                           {
                               var s = WorkpieceInfoEntries.SingleOrDefault(x => x.Id == evt.Id);
                               if (s != null)
                                   WorkpieceInfoEntries.Remove(s);

                               UpdateCommandStates();
                           });

            CreateDefectInfoCommand = new DelegateCommand(
                () =>
                {
                    if (SelectedWorkpieceInfo == null)
                    {
                    }
                    else
                    {
                        var index = DefectInfos.Count;
                        var defectInfo = new DefectInfo {Width = 200, Height = 200, X = index*100, Y = index*100};

                        InspectionDomainService.AddDefectInfo(SelectedWorkpieceInfo.Id, defectInfo);
                    }
                },
                () => { return SelectedWorkpieceInfo != null; });

            DeleteDefectInfoCommand = new DelegateCommand<DefectInfoViewModel>(
                (di) =>
                {
                    if (SelectedWorkpieceInfo == null)
                        return;

                    if (SelectedDefectInfo == null)
                        return;

                    InspectionDomainService.DeleteDefectInfo(SelectedWorkpieceInfo.Id, SelectedDefectInfo.Id);
                },
                (x) => { return SelectedDefectInfo != null; });



            CleanOldWorkpieceInfosCommand = new DelegateCommand(OnCleanOldWorkpieceInfosCommand);

            SelectDefectInfoCommand = new DelegateCommand<DefectInfoViewModel>(
                (di) =>
                {
                    if (di != null)
                        CroppedRegionRect = new Rect(di.X, di.Y, di.Width, di.Height);

                    UpdateCommandStates();
                });

            CreateMonithReportCommand = new DelegateCommand(
                () =>
                {
                    var report = ReportingDomainService.GetMonthReport(SelectedMonthReportDateTime.Year,
                        SelectedMonthReportDateTime.Month);

                    if (report == null)
                    {
                        MessageDialogService.Show("没有数据，无法生成报表");
                        return;
                    }

                    PreviewReportingDialogService
                        .Show(report)
                        .Subscribe(args =>
                                   {
                                       if (args.IsCanceled) return;

                                       ReportingDomainService.ExportReport(args.Data);
                                   });
                });

            QueryMonthRecordsCommand = new DelegateCommand(
                () =>
                {
                    var dis = ReportingDomainService.GetWorkpieceInfoEntriesByMonth(SelectedMonthReportDateTime.Year,
                        SelectedMonthReportDateTime.Month);
                    WorkpieceInfoEntries.Clear();
                    WorkpieceInfoEntries.AddRange(dis.Select(x => x.ToViewModel()));
                });

            CreateDayReportCommand = new DelegateCommand(
                () =>
                {
                    var report = ReportingDomainService.GetDayReport(SelectedDayReportDateTime.Year,
                        SelectedDayReportDateTime.Month,
                        SelectedDayReportDateTime.Day);

                    if (report == null)
                    {
                        MessageDialogService.Show("没有数据，无法生成报表");
                        return;
                    }

                    PreviewReportingDialogService
                        .Show(report)
                        .Subscribe(args =>
                                   {
                                       if (args.IsCanceled) return;

                                       ReportingDomainService.ExportReport(args.Data);
                                   });
                });

            QueryDayRecordsCommand = new DelegateCommand(
                () =>
                {
                    var dis = ReportingDomainService.GetWorkpieceInfoEntriesByDay(SelectedDayReportDateTime.Year,
                        SelectedDayReportDateTime.Month,
                        SelectedDayReportDateTime.Day);
                    WorkpieceInfoEntries.Clear();
                    WorkpieceInfoEntries.AddRange(dis.Select(x => x.ToViewModel()));
                });


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



            SaveImageToFileCommand = new DelegateCommand(
                OnSaveImageToFileCommand,
                () => SelectedSurfaceMonitor != null);

            EventAggregator
                .GetEvents<DefectInfoAddedDomainEvent>()
                .Subscribe(evt =>
                           {
                               if (SelectedWorkpieceInfo == null)
                                   return;

                               if (SelectedWorkpieceInfo.Id != evt.WorkpieceInfoId)
                                   return;

                               _defectInfos.Add(evt.DefectInfo.ToViewModel());

                               UpdateCommandStates();
                           });


            EventAggregator
                .GetEvents<DefectInfoRemovedDomainEvent>()
                .Subscribe(evt =>
                           {
                               if (SelectedDefectInfo != null && SelectedDefectInfo.Id == evt.DefectInfoId)
                               {
                                   SelectedDefectInfo = null;
                               }

                               var di = _defectInfos.SingleOrDefault(x => x.Id == evt.DefectInfoId);
                               if (di != null)
                               {
                                   _defectInfos.Remove(di);
                               }

                               UpdateCommandStates();
                           });

            EventAggregator.GetEvents<ReportExportFailedEvent>()
                .Subscribe(evt => { MessageBox.Show("导出失败!" + "\n\n" + evt.Exception.Message); });

            EventAggregator.GetEvents<ReportExportSuccessfulEvent>()
                .Subscribe(evt => { MessageBox.Show("导出成功!" + "\n\n" + evt.FileName); });


            var thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            MonthReportDateTimes.Add(thisMonth);
            for (int i = 0; i < 5; i++)
            {
                MonthReportDateTimes.Add(thisMonth.AddMonths(-i - 1));
            }

            SelectedMonthReportDateTime = thisMonth;
            SelectedDayReportYearMonthDateTime = thisMonth;
        }

        private void OnCleanOldWorkpieceInfosCommand()
        {
            AskDialogService.Show("是否确定清空数据？").Subscribe(
                args =>
                {
                    if (args.IsCanceled)
                        return;

                    if (!args.Data)
                        return;

                    var thisMonthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    //                    var lastSixMonthFirstDay = DateTime.Now.AddMonths(-6); // 6 month earlier
                    var lastSixMonthFirstDay = DateTime.Now.AddMonths(+1);
                    ReportingDomainService.CleanOldWorkpieceInfos(lastSixMonthFirstDay);

                    UpdateCommandStates();

                    MessageDialogService.Show("历史数据已经成功删除");
                });
        }

        private async void OnSaveImageToFileCommand()
        {
            BitmapSource bitmapSource = null;
            bitmapSource = SelectedSurfaceMonitor.BitmapSource;

            if (bitmapSource == null)
            {
                MessageBox.Show("Does not have BitmapSource, cannot save to TIFF!");
                return;
            }

            await bitmapSource.OpenSaveImageToFileDialogAsync(SelectedSurfaceMonitor.SurfaceTypeIndex);
        }

        private void UpdateCommandStates()
        {
            CreateWorkpieceInfoCommand.RaiseCanExecuteChanged();
            DeleteWorkpieceInfoCommand.RaiseCanExecuteChanged();

            CreateDefectInfoCommand.RaiseCanExecuteChanged();
            DeleteDefectInfoCommand.RaiseCanExecuteChanged();

            ZoomActualCommand.RaiseCanExecuteChanged();
            ZoomFitCommand.RaiseCanExecuteChanged();
            ZoomInCommand.RaiseCanExecuteChanged();
            ZoomOutCommand.RaiseCanExecuteChanged();

            SaveImageToFileCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand CreateWorkpieceInfoCommand { get; set; }

        public DelegateCommand<WorkpieceInfoEntryViewModel> DeleteWorkpieceInfoCommand { get; set; }

        public DelegateCommand<WorkpieceInfoEntryViewModel> SelectWorkpieceInfoCommand { get; set; }

        public DelegateCommand CreateDefectInfoCommand { get; set; }

        public DelegateCommand<DefectInfoViewModel> DeleteDefectInfoCommand { get; set; }

        public DelegateCommand<DefectInfoViewModel> SelectDefectInfoCommand { get; set; }

        public DelegateCommand CreateMonithReportCommand { get; set; }

        public DelegateCommand CreateDayReportCommand { get; set; }

        public DelegateCommand QueryDayRecordsCommand { get; set; }

        public DelegateCommand QueryMonthRecordsCommand { get; set; }

        public BindableCollection<WorkpieceInfoEntryViewModel> WorkpieceInfoEntries
        {
            get { return _WorkpieceInfoEntries; }
        }

        public BindableCollection<DefectInfoViewModel> DefectInfos
        {
            get { return _defectInfos; }
        }

        private WorkpieceInfoEntryViewModel _selectedWorkpieceInfo;

        public WorkpieceInfoEntryViewModel SelectedWorkpieceInfo
        {
            get { return _selectedWorkpieceInfo; }
            set
            {
                if (Equals(_selectedWorkpieceInfo, value)) return;
                _selectedWorkpieceInfo = value;
                RaisePropertyChanged(() => SelectedWorkpieceInfo);
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

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (Equals(_imageSource, value)) return;
                _imageSource = value;
                RaisePropertyChanged(() => ImageSource);
            }
        }

        private ImageSource _indicatorSource;

        public ImageSource IndicatorSource
        {
            get { return _indicatorSource; }
            set
            {
                if (Equals(_indicatorSource, value)) return;
                _indicatorSource = value;
                RaisePropertyChanged(() => IndicatorSource);
            }
        }

        private Rect _defectRegion;

        public Rect DefectRegion
        {
            get { return _defectRegion; }
            set
            {
                if (Equals(_defectRegion, value)) return;
                _defectRegion = value;
                RaisePropertyChanged(() => DefectRegion);
            }
        }

        private double _x;

        public double X
        {
            get { return _x; }
            set
            {
                if (Equals(_x, value)) return;
                _x = value;
                RaisePropertyChanged(() => X);
            }
        }

        private double _y;

        public double Y
        {
            get { return _y; }
            set
            {
                if (Equals(_y, value)) return;
                _y = value;
                RaisePropertyChanged(() => Y);
            }
        }

        private Rect _croppedRegionRect;

        public Rect CroppedRegionRect
        {
            get { return _croppedRegionRect; }
            set
            {
                if (Equals(_croppedRegionRect, value)) return;
                _croppedRegionRect = value;
                RaisePropertyChanged(() => CroppedRegionRect);
            }
        }

        public BindableCollection<DateTime> MonthReportDateTimes
        {
            get { return _monthReportDateTimes; }
        }


        public BindableCollection<DateTime> DayReportDateTimes
        {
            get { return _dayReportDateTimes; }
        }

        private DateTime _selectedMonthReportDateTime;

        public DateTime SelectedMonthReportDateTime
        {
            get { return _selectedMonthReportDateTime; }
            set
            {
                if (Equals(_selectedMonthReportDateTime, value)) return;
                _selectedMonthReportDateTime = value;
                RaisePropertyChanged(() => SelectedMonthReportDateTime);
            }
        }

        private DateTime _selectedDayReportYearMonthDateTime;

        public DateTime SelectedDayReportYearMonthDateTime
        {
            get { return _selectedDayReportYearMonthDateTime; }
            set
            {
                if (Equals(_selectedDayReportYearMonthDateTime, value)) return;
                _selectedDayReportYearMonthDateTime = value;
                RaisePropertyChanged(() => SelectedDayReportYearMonthDateTime);


                // reset
                DayReportDateTimes.Clear();
                var dayCount = DateTime.DaysInMonth(value.Year, value.Month);
                for (int i = 0; i < dayCount; i++)
                {
                    DayReportDateTimes.Add(value.AddDays(i));
                }

                // reset
                if (SelectedDayReportYearMonthDateTime == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                {
                    var today = DayReportDateTimes.Single(x => x == DateTime.Today);
                    SelectedDayReportDateTime = today;
                }
                else
                {
                    SelectedDayReportDateTime = DayReportDateTimes.First();
                }
            }
        }

        private DateTime _selectedDayReportDateTime;

        public DateTime SelectedDayReportDateTime
        {
            get { return _selectedDayReportDateTime; }
            set
            {
                if (Equals(_selectedDayReportDateTime, value)) return;
                _selectedDayReportDateTime = value;
                RaisePropertyChanged(() => SelectedDayReportDateTime);
            }
        }

        public ICollectionView DefectInfosCollectionView { get; set; }

        public IList<SurfaceMonitorViewModel> SurfaceMonitors { get; private set; }

        public DelegateCommand<SurfaceMonitorViewModel> SelectSurfaceCommand { get; set; }
        public DelegateCommand SaveImageToFileCommand { get; set; }

        public DelegateCommand ZoomInCommand { get; set; }
        public DelegateCommand ZoomOutCommand { get; set; }
        public DelegateCommand ZoomFitCommand { get; set; }
        public DelegateCommand ZoomFitDisplayAreaCommand { get; set; }
        public DelegateCommand ZoomActualCommand { get; set; }

        private void HideAll()
        {
            SelectedDefectInfo = null;
            SelectedSurfaceMonitor = null;

            SurfaceMonitors.ForEach(x =>
                                    {
                                        x.IsSelected = false;
                                        x.ZoomFit();
                                    });

            _isZoomFitDisplayAreaEnabled = false;
            _isZoomActualEnabled = false;
        }

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
        /// <summary>
        /// just display defect infos in list
        /// </summary>
        public DelegateCommand SwitchDisplayAllDefectsCommand { get; set; }

        /// <summary>
        /// show defects in images
        /// </summary>
        public DelegateCommand SwitchDisplayAllDefectsOnImagesCommand { get; set; }

        public DelegateCommand CleanOldWorkpieceInfosCommand { get; set; }

    }
}