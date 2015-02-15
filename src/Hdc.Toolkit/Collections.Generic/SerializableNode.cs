/*using System.Collections.ObjectModel;

namespace Hdc.Collections.Generic
{
    public interface ISerializableNode<TNode> where TNode : ISerializableNode<TNode>
    {
        Collection<TNode> Nodes { get; }
    }
}*/

using System;
using System.Collections.ObjectModel;

namespace Hdc.Collections.Generic
{
    [Serializable]
    public class SerializableNode<TNode> where TNode : SerializableNode<TNode>
    {
        private Collection<TNode> _nodes = new Collection<TNode>();

        public Collection<TNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }
    }
}