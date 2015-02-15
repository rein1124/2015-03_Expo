using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Hdc.Windows.Media.Imaging;
using Microsoft.Win32;

namespace ODM.Presentation.ViewModels.Inspection
{
    class InspectionViewModelService : IInspectionViewModelService
    {
        public async void OpenSaveImageToFileDialog(BitmapSource bitmapSource, string surfaceTypeName)
        {
            if (bitmapSource == null)
            {
                MessageBox.Show("Does not have BitmapSource, cannot save to TIFF!");
                return;
            }

            var now = DateTime.Now;
            var dialg = new SaveFileDialog
                        {
                            Title = "Save " + surfaceTypeName + " to TIFF",
                            Filter = "TIFF files (*.tif)|*.tif|" +
                                     "BMP files (*.bmp)|*.bmp|" +
                                     "All files (*.*)|*.*",
                            FileName =
                                now.ToString(@"yyyy-MM-dd_HH'h'mm'm'ss's'_") +
                                //                                 now.Year.ToString() + "-" + now.Month + "-" + now.Day + "_" +
                                //                                 now.Hour + "h" + now.Minute + "m" + now.Second + "s_" +
                                surfaceTypeName,
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
    }
}