using Hdc.Mvvm;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Collections.Generic
{
    public class GenericStructureChild<TThisContext,
                                       TParent,
                                       TParentContext> : ViewModel,
                                                         IGenericStructureChild<TThisContext,
                                                             TParent,
                                                             TParentContext>
        where TParent : IGenericStructureBase<TParentContext>
    {
        void IGenericStructureBase<TThisContext>.Initialize(TThisContext context)
        {
            Context = context;

            Initialize(context);
        }

        protected virtual void Initialize(TThisContext context)
        {
        }


        public int Index { get; set; }

        public TThisContext Context { get; private set; }

        public TParent Parent { get; set; }
    }
}