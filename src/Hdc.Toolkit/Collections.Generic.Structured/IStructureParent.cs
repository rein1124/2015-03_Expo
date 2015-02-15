using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IStructureParent<TThis, TChild> where TThis : IStructureParent<TThis, TChild>
                                                     where TChild : IStructureChild<TThis>
    {
        IList<TChild> Children { get; }

//        TChild this[int index] { get; }
    }
}