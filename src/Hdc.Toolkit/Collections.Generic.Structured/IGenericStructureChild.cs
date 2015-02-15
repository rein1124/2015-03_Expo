using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IGenericStructureChild<TThisContext,
                                            TParent,
                                            TParentContext> : IGenericStructureBase<TThisContext>,
                                                              IStructureChild<TParent>
        where TParent : IGenericStructureBase<TParentContext>
    {
    }
}