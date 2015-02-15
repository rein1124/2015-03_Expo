using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hdc.Mvvm;

namespace Hdc.Collections.Generic
{
    public class ViewModelBidirectionStructureMiddle<TThis,
                                                     TParent,
                                                     TChild> : ViewModel,
                                                               IBidirectionStructureMiddle<
                                                                   TThis,
                                                                   TParent,
                                                                   TChild>
        where TParent : IBidirectionStructureParent<TParent, TThis>
        where TChild : IBidirectionStructureChild<TChild, TThis>
        where TThis : IBidirectionStructureMiddle<TThis, TParent, TChild>
    {
        private IList<TChild> _children = new ObservableCollection<TChild>();

        private TParent _parent;

        private int _index;
       
        public IList<TChild> Children
        {
            get { return _children; }
            set
            {
                if (Equals(_children, value)) return;
                _children = value;
                RaisePropertyChanged(() => Children);
            }
        } 

        public TParent Parent
        {
            get { return _parent; }
            set
            {
                if (Equals(_parent, value)) return;
                _parent = value;
                RaisePropertyChanged(() => Parent);
            }
        }

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
    }
}