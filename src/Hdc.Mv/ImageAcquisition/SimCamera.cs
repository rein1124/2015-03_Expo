using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hdc.Mv.Inspection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.ImageAcquisition
{
    public class SimCamera : ICamera
    {
        private int _index;

        readonly List<string> _fileNames = new List<string>();

        public void AddImageFileName(string imageFileName)
        {
            _fileNames.Add(imageFileName);
        }

        public SimCamera()
        {
        }

        public SimCamera(IEnumerable<string> fileNames )
        {
            _fileNames.AddRange(fileNames);
        }

        public void ResetIndex()
        {
            _index = 0;
        }

        public void Dispose()
        {
        }

        public bool Init()
        {
            return true;
        }

        public ImageInfo Acquisition()
        {
            string fileName = _fileNames[_index];

            var bs = fileName.GetBitmapSource();

            var bsi = bs.ToGray8BppBitmapSourceInfo();

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(bsi.Buffer.Length);
            Marshal.Copy(bsi.Buffer, 0, unmanagedPointer, bsi.Buffer.Length);

            var imageInfo = new ImageInfo()
                            {
                                BitsPerPixel = bs.Format.BitsPerPixel,
                                BufferPtr = unmanagedPointer,
                                PixelHeight = bsi.PixelHeight,
                                PixelWidth = bsi.PixelWidth,
                                Index = _index,
                            };

            _index++;
            if (_index == _fileNames.Count)
                _index = 0;

            return imageInfo;
        }
    }
}