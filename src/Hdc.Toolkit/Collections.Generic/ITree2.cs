using System;
using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface ITree<TTree> where TTree : ITree<TTree>
    {
        TTree ParentNode { get; set; }

        IList<TTree> ChildNodes { get; set; }

        void Add(TTree child);

        int Index { get; set; }
    }
}