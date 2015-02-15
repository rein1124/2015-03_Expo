using System;
using Hdc.Mvvm;

namespace ODM.Presentation.ViewModels
{
    public class WorkpieceInfoEntryViewModel : ViewModel
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

        private int _indexOfTotal;

        public int IndexOfTotal
        {
            get { return _indexOfTotal; }
            set
            {
                if (Equals(_indexOfTotal, value)) return;
                _indexOfTotal = value;
                RaisePropertyChanged(() => IndexOfTotal);
            }
        }

        private int _indexOfDay;

        public int IndexOfDay
        {
            get { return _indexOfDay; }
            set
            {
                if (Equals(_indexOfDay, value)) return;
                _indexOfDay = value;
                RaisePropertyChanged(() => IndexOfDay);
            }
        }

        private int _indexOfJob;

        public int IndexOfJob
        {
            get { return _indexOfJob; }
            set
            {
                if (Equals(_indexOfJob, value)) return;
                _indexOfJob = value;
                RaisePropertyChanged(() => IndexOfJob);
            }
        }

        private DateTime? _inspectDateTime;

        public DateTime? InspectDateTime
        {
            get { return _inspectDateTime; }
            set
            {
                if (Equals(_inspectDateTime, value)) return;
                _inspectDateTime = value;
                RaisePropertyChanged(() => InspectDateTime);
            }
        }
    }
}