using System.Collections.Generic;

namespace Hdc.Collections
{
    public interface INode //: IEnumerable<INode>
    {
        IList<INode> Nodes { get; }
    }
}