using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using AutoMapper;
using Hdc.Patterns;
using Hdc.Windows.Media.Imaging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using ODM.Domain.Inspection;

namespace ODM.Presentation.ViewModels.Inspection
{
    public static class Ex
    {
        static Ex()
        {
            Mapper.CreateMap<WorkpieceInfoEntryViewModel, WorkpieceInfoEntry>();
            Mapper.CreateMap<WorkpieceInfoEntry, WorkpieceInfoEntryViewModel>();

            Mapper.CreateMap<DefectInfoViewModel, DefectInfo>();
            Mapper.CreateMap<DefectInfo, DefectInfoViewModel>();

            Mapper.CreateMap<MeasurementInfoViewModel, MeasurementInfo>();
            Mapper.CreateMap<MeasurementInfo, MeasurementInfoViewModel>();
            Mapper.CreateMap<InspectInfo, InspectionInfoViewModel>();
        }

        public static WorkpieceInfoEntryViewModel ToViewModel(this WorkpieceInfoEntry source)
        {
            var vm = source.Map<WorkpieceInfoEntry, WorkpieceInfoEntryViewModel>();
            return vm;
        }

        public static DefectInfoViewModel ToViewModel(this DefectInfo source)
        {
            var vm = source.Map<DefectInfo, DefectInfoViewModel>();
            return vm;
        }

        public static MeasurementInfoViewModel ToViewModel(this MeasurementInfo source)
        {
            var vm = source.Map<MeasurementInfo, MeasurementInfoViewModel>();
            return vm;
        }

        public static InspectionInfoViewModel ToViewModel(this InspectInfo source)
        {
            var vm = source.Map<InspectInfo, InspectionInfoViewModel>();

            foreach (var di in vm.DefectInfos)
            {
                di.SurfaceTypeIndex = vm.SurfaceTypeIndex;
            }

            foreach (var mi in vm.MeasurementInfos)
            {
                mi.SurfaceTypeIndex = vm.SurfaceTypeIndex;
            }

            return vm;
        }

        private static IImageSaveLoadService _imageSaveLoadService;

        public static IImageSaveLoadService ImageSaveLoadService
        {
            get
            {
                return _imageSaveLoadService ??
                       (_imageSaveLoadService = ServiceLocator.Current.GetInstance<IImageSaveLoadService>());
            }
        }

        public static BitmapSource LoadImage(this StoredImageInfo storedImageInfo)
        {
            var bs = ImageSaveLoadService.GetBitmapImage(storedImageInfo.StoredDateTime, storedImageInfo.ImageGuid);
            return bs;
        }

        public async static void OpenSaveImageToFileDialog(BitmapSource bitmapSource, int surfaceIndex)
        {

            //            bitmapSource = SelectedSurfaceMonitor.BitmapSource;

            if (bitmapSource == null)
            {
                MessageBox.Show("Does not have BitmapSource, cannot save to TIFF!");
                return;
            }

            var now = DateTime.Now;
            var dialg = new SaveFileDialog
            {
                Title = "Save Surface Index " + surfaceIndex + " to TIFF",
                Filter = "TIFF files (*.tif)|*.tif|" +
                         "BMP files (*.bmp)|*.bmp|" +
                         "All files (*.*)|*.*",
                FileName = now.ToString(@"yyyy-MM-dd_HH'h'mm'm'ss's'_") +
                           surfaceIndex,
            };

            var ret = dialg.ShowDialog();

            if (ret.HasValue && ret.Value)
            {
                var fi = new FileInfo(dialg.FileName);
                var ext = fi.Extension;

                switch (ext)
                {
                    case ".BMP":
                    case ".bmp":
                        await bitmapSource.SaveToBmpAsync(dialg.FileName);
                        MessageBox.Show("BMP saved: " + dialg.FileName);
                        break;
                    case ".TIFF":
                    case ".TIF":
                    case ".tiff":
                    case ".tif":
                        await bitmapSource.SaveToTiffAsync(dialg.FileName, TiffCompressOption.None);
                        MessageBox.Show("TIFF saved: " + dialg.FileName);
                        break;
                }
            }
        }

        public static Task OpenSaveImageToFileDialogAsync(this BitmapSource bitmapSource, int surfaceIndex)
        {
            return Task.Run(() => OpenSaveImageToFileDialog(bitmapSource, surfaceIndex));
        }
    }
}