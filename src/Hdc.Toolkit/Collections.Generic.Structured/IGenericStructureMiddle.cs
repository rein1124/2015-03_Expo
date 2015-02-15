using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IGenericStructureMiddle<TThis,
                                             TParent,
                                             TChild,
                                             TThisContext,
                                             TParentContext,
                                             TChildContext> : IStructureMiddle<TThis,
                                                                  TParent,
                                                                  TChild>,
                                                              IGenericStructureChild<TThisContext,
                                                                  TParent,
                                                                  TParentContext>,
                                                              IGenericStructureParent<TThis,
                                                                  TChild,
                                                                  TThisContext,
                                                                  TChildContext>
        where TThis : IGenericStructureMiddle<TThis,
                          TParent,
                          TChild,
                          TThisContext,
                          TParentContext,
                          TChildContext>
        where TParent : IGenericStructureBase<TParentContext>
        where TChild : IGenericStructureChild<TChildContext,
                           TThis,
                           TThisContext>
    {
    }
}