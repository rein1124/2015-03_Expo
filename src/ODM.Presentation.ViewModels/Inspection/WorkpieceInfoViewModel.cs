using System;
using System.Collections.Generic;
using System.ComponentModel;
using Hdc.Mvvm;

namespace ODM.Presentation.ViewModels
{
    public class WorkpieceInfoViewModel : ViewModel
    {
        public long Id { get; set; }

        private readonly BindingList<DefectInfoViewModel> _defectInfos;

        public IList<DefectInfoViewModel> DefectInfos
        {
            get { return _defectInfos; }
        }

        public WorkpieceInfoViewModel()
        {
            _defectInfos = new BindingList<DefectInfoViewModel>();
        }

        private DateTime _inspectDateTime;

        public DateTime InspectDateTime
        {
            get { return _inspectDateTime; }
            set
            {
                if (Equals(_inspectDateTime, value)) return;
                _inspectDateTime = value;
                RaisePropertyChanged(() => InspectDateTime);
            }
        }

        private Guid _imageId;

        public Guid ImageId
        {
            get { return _imageId; }
            set
            {
                if (Equals(_imageId, value)) return;
                _imageId = value;
                RaisePropertyChanged(() => ImageId);
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
    }
}