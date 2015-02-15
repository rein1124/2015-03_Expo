using System;
using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface INode<TNode>  where TNode : INode<TNode>
    {
        IList<TNode> Nodes { get;  }
    }
}