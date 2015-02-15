using System;
using Hdc.Mvvm;
using ODM.Domain.Inspection;

namespace ODM.Presentation.ViewModels
{
    public class MeasurementInfoViewModel : ViewModel
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
                RaisePropertyChanged(() => IndexDisplayValue);
            }
        }

        public int DisplayIndex
        {
            get
            {
                return Index + 1;
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
                RaisePropertyChanged(() => TypeDescription);
            }
        }

        public string TypeDescription
        {
            get { return "Type=" + TypeCode; }
        }

        private double _startPointX;

        public double StartPointX
        {
            get { return _startPointX; }
            set
            {
                if (Equals(_startPointX, value)) return;
                _startPointX = value;
                RaisePropertyChanged(() => StartPointX);
                RaisePropertyChanged(() => StartPointXDisplayValue);
            }
        }

        private double _startPointY;

        public double StartPointY
        {
            get { return _startPointY; }
            set
            {
                if (Equals(_startPointY, value)) return;
                _startPointY = value;
                RaisePropertyChanged(() => StartPointY);
                RaisePropertyChanged(() => StartPointYDisplayValue);
            }
        }

        private double _endPointX;

        public double EndPointX
        {
            get { return _endPointX; }
            set
            {
                if (Equals(_endPointX, value)) return;
                _endPointX = value;
                RaisePropertyChanged(() => EndPointX);
                RaisePropertyChanged(() => EndPointXDisplayValue);
            }
        }

        private double _endPointY;

        public double EndPointY
        {
            get { return _endPointY; }
            set
            {
                if (Equals(_endPointY, value)) return;
                _endPointY = value;
                RaisePropertyChanged(() => EndPointY);
                RaisePropertyChanged(() => EndPointYDisplayValue);
            }
        }

        private double _value;

        public double Value
        {
            get { return _value; }
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                RaisePropertyChanged(() => Value);
                RaisePropertyChanged(() => ValueDisplayValue);
            }
        }

//        private int _groupIndex;
//
//        public int GroupIndex
//        {
//            get { return _groupIndex; }
//            set
//            {
//                if (Equals(_groupIndex, value)) return;
//                _groupIndex = value;
//                RaisePropertyChanged(() => GroupIndex);
//                RaisePropertyChanged(() => DisplayGroupIndex);
//            }
//        }

//        public int DisplayGroupIndex
//        {
//            get { return GroupIndex + 1; }
//        }

        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (Equals(_groupName, value)) return;
                _groupName = value;
                RaisePropertyChanged(() => GroupName);
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
                        return "正面";
                    case 1:
                        return "背面";
                    default:
                        return "默认";
                }
            }
        }

        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (Equals(_displayName, value)) return;
                _displayName = value;
                RaisePropertyChanged(() => DisplayName);
            }
        }

        public string IndexDisplayValue
        {
            get
            {
//                return SurfaceTypeDisplayName + "-" +
//                    (GroupIndex + 1).ToString("D1") +
//                    (Index + 1).ToString("D2");
                return (Index + 1).ToString("D2");
            }
        }

        private double _startPointXActualValue;

        public double StartPointXActualValue
        {
            get { return _startPointXActualValue; }
            set
            {
                if (Equals(_startPointXActualValue, value)) return;
                _startPointXActualValue = value;
                RaisePropertyChanged(() => StartPointXActualValue);
            }
        }


        private double _startPointYActualValue;

        public double StartPointYActualValue
        {
            get { return _startPointYActualValue; }
            set
            {
                if (Equals(_startPointYActualValue, value)) return;
                _startPointYActualValue = value;
                RaisePropertyChanged(() => StartPointYActualValue);
            }
        }

        private double _endPointXActualValue;

        public double EndPointXActualValue
        {
            get { return _endPointXActualValue; }
            set
            {
                if (Equals(_endPointXActualValue, value)) return;
                _endPointXActualValue = value;
                RaisePropertyChanged(() => EndPointXActualValue);
            }
        }

        private double _endPointYActualValue;

        public double EndPointYActualValue
        {
            get { return _endPointYActualValue; }
            set
            {
                if (Equals(_endPointYActualValue, value)) return;
                _endPointYActualValue = value;
                RaisePropertyChanged(() => EndPointYActualValue);
            }
        }

        private double _valueActualValue;

        public double ValueActualValue
        {
            get { return _valueActualValue; }
            set
            {
                if (Equals(_valueActualValue, value)) return;
                _valueActualValue = value;
                RaisePropertyChanged(() => ValueActualValue);
            }
        }

        public string StartPointXDisplayValue { get { return StartPointXActualValue.ToString("00.00"); } }
        public string StartPointYDisplayValue { get { return StartPointYActualValue.ToString("00.00"); } }
        public string EndPointXDisplayValue { get { return EndPointXActualValue.ToString("00.00"); } }
        public string EndPointYDisplayValue { get { return EndPointYActualValue.ToString("00.00"); } }
        public string ValueDisplayValue { get { return ValueActualValue.ToString("00.00"); } }

        private double _expectValue;

        public double ExpectValue
        {
            get { return _expectValue; }
            set
            {
                if (Equals(_expectValue, value)) return;
                _expectValue = value;
                RaisePropertyChanged(() => ExpectValue);
                RaisePropertyChanged(() => ExpectValueDisplayValue);
                RaisePropertyChanged(() => DiffValueDisplayValue);
            }
        }

        public string ExpectValueDisplayValue { get { return ExpectValue.ToString("00.00"); } }
        public string DiffValueDisplayValue { get { return (ValueActualValue - ExpectValue).ToString("00.00"); } }

    }
}