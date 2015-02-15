using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hdc.Mvvm;

namespace Hdc.Collections.Generic
{
    public class BidirectionStructureMiddle<TThis,
                                            TParent,
                                            TChild> : IBidirectionStructureMiddle<
                                                          TThis,
                                                          TParent,
                                                          TChild>
        where TParent : IBidirectionStructureParent<TParent, TThis>
        where TChild : IBidirectionStructureChild<TChild, TThis>
        where TThis : IBidirectionStructureMiddle<TThis, TParent, TChild>
    {
        private IList<TChild> _children = new List<TChild>();

        public IList<TChild> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public TParent Parent { get; set; }

        public int Index { get; set; }
    }
}