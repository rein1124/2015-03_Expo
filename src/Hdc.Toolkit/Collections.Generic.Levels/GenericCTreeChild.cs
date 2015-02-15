/*using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.DataStructure
{
    public class DpGenericStructureChild<TThisContext> : DependencyObject,
                                                         IGenericCTreeChild<TThisContext>
    {
        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }


        void IGenericCTreeBase<TThisContext>.Initialize(TThisContext context)
        {
            Context = context;

            Initialize(context);
        }

        protected virtual void Initialize(TThisContext context)
        {
        }

        public int Index { get; set; }

        public TThisContext Context { get; private set; }
    }
}*/