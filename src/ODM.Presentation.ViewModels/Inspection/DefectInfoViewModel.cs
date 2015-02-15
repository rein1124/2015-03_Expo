using Hdc.Mvvm;
using ODM.Domain.Inspection;

namespace ODM.Presentation.ViewModels
{
    public class DefectInfoViewModel : ViewModel
    {
        private long _id;

        public long Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value)) return;
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (Equals(_index, value)) return;
                _index = value;
                RaisePropertyChanged(() => Index);
                RaisePropertyChanged(() => DisplayIndex);
            }
        }

        public int DisplayIndex
        {
            get
            {
                return Index + 1;
            }
        }

        private bool _isReject;

        public bool IsReject
        {
            get { return _isReject; }
            set
            {
                if (Equals(_isReject, value)) return;
                _isReject = value;
                RaisePropertyChanged(() => IsReject);
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

        private double _width;

        public double Width
        {
            get { return _width; }
            set
            {
                if (Equals(_width, value)) return;
                _width = value;
                RaisePropertyChanged(() => Width);
            }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set
            {
                if (Equals(_height, value)) return;
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        }

        private DefectType _type;

        public DefectType Type
        {
            get { return _type; }
            set
            {
                if (Equals(_type, value)) return;
                _type = value;
                RaisePropertyChanged(() => Type);
                RaisePropertyChanged(() => TypeDescription);
            }
        }

        private int _typeCode;

        public int TypeCode
        {
            get { return _typeCode; }
            set
            {
                if (Equals(_typeCode, value)) return;
                _typeCode = value;
                RaisePropertyChanged(() => TypeCode);
            }
        }

        private string _surfaceName;

        public string SurfaceName
        {
            get { return _surfaceName; }
            set
            {
                if (Equals(_surfaceName, value)) return;
                _surfaceName = value;
                RaisePropertyChanged(() => SurfaceName);
            }
        }

        public string TypeDescription
        {
            get
            {
                switch (Type)
                {
                    case DefectType.Undefined:
                        return "未定义";
                    case DefectType.ForeignSpot:
                        return "异物";
                    case DefectType.DefectType04:
                        return "黑点";
                    case DefectType.DirtyPoint:
                        return "污点";
                    case DefectType.DefectType05:
                        return "黑斑";
                    case DefectType.DefectType02:
                        return "亮斑";
                    case DefectType.NonuniformSpot:
                        return "不匀斑块";
                    case DefectType.HorizontalNonuniform:
                        return "横向不匀";
                    case DefectType.VerticalNonuniform:
                        return "纵向不匀";

                    case DefectType.PartExist:
                        return "部件存在";
                    case DefectType.PartNoExist:
                        return "部件确实";

                    default:
                        return "未定义-" + Type.ToString();
                }
//                return Type.ToString().Replace(
//                    "DefectType", "DT-");
            }
        }

        private double _size;

        public double Size
        {
            get { return _size; }
            set
            {
                if (Equals(_size, value)) return;
                _size = value;
                RaisePropertyChanged(() => Size);
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
                RaisePropertyChanged(() => SurfaceTypeDisplayName);
            }
        }

        public string SurfaceTypeDisplayName
        {
            get
            {
                switch (SurfaceTypeIndex)
                {
                    case 0:
                        return "A";
                    case 1:
                        return "B";
                    default:
                        return "N";
                }
            }
        }


        public string IndexDisplayValue
        {
            get
            {
                return SurfaceTypeDisplayName + "-" +
                    (Index + 1).ToString("D2");
            }
        }


        private double _xActualValue;

        public double XActualValue
        {
            get { return _xActualValue; }
            set
            {
                if (Equals(_xActualValue, value)) return;
                _xActualValue = value;
                RaisePropertyChanged(() => XActualValue);
            }
        }

        private double _yActualValue;

        public double YActualValue
        {
            get { return _yActualValue; }
            set
            {
                if (Equals(_yActualValue, value)) return;
                _yActualValue = value;
                RaisePropertyChanged(() => YActualValue);
            }
        }

        private double _widthActualValue;

        public double WidthActualValue
        {
            get { return _widthActualValue; }
            set
            {
                if (Equals(_widthActualValue, value)) return;
                _widthActualValue = value;
                RaisePropertyChanged(() => WidthActualValue);
            }
        }

        private double _heightActualValue;

        public double HeightActualValue
        {
            get { return _heightActualValue; }
            set
            {
                if (Equals(_heightActualValue, value)) return;
                _heightActualValue = value;
                RaisePropertyChanged(() => HeightActualValue);
            }
        }

        private double _sizeActualValue;

        public double SizeActualValue
        {
            get { return _sizeActualValue; }
            set
            {
                if (Equals(_sizeActualValue, value)) return;
                _sizeActualValue = value;
                RaisePropertyChanged(() => SizeActualValue);
            }
        }

        public string WidthDisplayValue { get { return WidthActualValue.ToString("0.0"); } }
        public string HeightDisplayValue { get { return HeightActualValue.ToString("0.0"); } }
        public string XDisplayValue { get { return XActualValue.ToString("0.0"); } }
        public string YDisplayValue { get { return YActualValue.ToString("0.0"); } }
        public string SizeDisplayValue { get { return SizeActualValue.ToString("0.0"); } }
        public string GroupName { get; set; }
    }
}