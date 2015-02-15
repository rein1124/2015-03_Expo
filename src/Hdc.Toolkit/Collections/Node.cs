using System.Collections.Generic;

namespace Hdc.Collections
{
    public class Node : INode
    {
        private IList<INode> _nodes = new List<INode>();

        public IList<INode> Nodes
        {
            get { return _nodes; }
        }
    }
}