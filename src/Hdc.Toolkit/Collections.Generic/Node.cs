using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public class Node<TNode> :  INode<TNode> where TNode : INode<TNode>
    {
        private IList<TNode> _nodes = new List<TNode>();

        public IList<TNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }
    }
}