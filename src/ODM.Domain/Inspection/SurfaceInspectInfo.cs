using System.Windows.Media.Imaging;
using Hdc.Mv;

namespace ODM.Domain.Inspection
{
    public class SurfaceInspectInfo
    {
        private BitmapSource _bitmapSource;
        private ImageInfo _imageInfo;

        public int WorkpieceIndex { get; set; }

        public int SurfaceTypeIndex { get; set; }

        public ImageInfo ImageInfo
        {
            get
            {
                if (_imageInfo != null)
                    return _imageInfo;

                if (_bitmapSource != null)
                    _imageInfo = _bitmapSource.ToImageInfoWith8Bpp().ToEntity();

                return _imageInfo;
            }
            set
            {
                _imageInfo = value;
                _bitmapSource = null;
            }
        }

        public BitmapSource BitmapSource
        {
            get
            {
                if (_bitmapSource != null)
                    return _bitmapSource;

                if (ImageInfo != null)
                    _bitmapSource = ImageInfo.ToBitmapSource();

                return _bitmapSource;
            }
            set
            {
                _bitmapSource = value;
                _imageInfo = null;
            }
        }

        public InspectInfo InspectInfo { get; set; }
    }
}