using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IGenericStructureParent<TThis,
                                             TChild,
                                             TThisContext,
                                             TChildContext> : IGenericStructureBase<TThisContext>,
                                                              IStructureParent<TThis,
                                                                  TChild>
        where TThis : IGenericStructureParent<TThis,
                          TChild,
                          TThisContext,
                          TChildContext>
        where TChild : IGenericStructureChild<TChildContext,
                           TThis,
                           TThisContext>
    {
    }

}