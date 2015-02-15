namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InternalTree<TData, TTree> where TTree : InternalTree<TData, TTree>
    {
        public InternalTree()
        {
        }

        public InternalTree(params TTree[] childNodes)
        {
            foreach (var childNode in childNodes)
            {
                _childNodes.Add(childNode);
            }
        }

        private List<TTree> _childNodes = new List<TTree>();

        public List<TTree> ChildNodes
        {
            get { return _childNodes; }
        }

        public TData Value { get; set; }
    }
}