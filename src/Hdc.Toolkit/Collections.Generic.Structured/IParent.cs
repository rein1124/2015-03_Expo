using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IParent<TChild>
    {
        IList<TChild> Children { get; set; }
    }
}