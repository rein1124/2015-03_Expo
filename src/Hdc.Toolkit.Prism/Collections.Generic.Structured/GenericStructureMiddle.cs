using Microsoft.Practices.ServiceLocation;

namespace Hdc.Collections.Generic
{
    public abstract class GenericStructureMiddle<TThis,
                                                 TParent,
                                                 TChild,
                                                 TThisContext,
                                                 TParentContext,
                                                 TChildContext> : GenericStructureParent<TThis,
                                                                      TChild,
                                                                      TThisContext,
                                                                      TChildContext>,
                                                                  IGenericStructureMiddle<TThis,
                                                                      TParent,
                                                                      TChild,
                                                                      TThisContext,
                                                                      TParentContext,
                                                                      TChildContext>
        where TThis : class, IGenericStructureMiddle<TThis,
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
        public int Index { get; set; }

        public TParent Parent { get; set; }
    }
}