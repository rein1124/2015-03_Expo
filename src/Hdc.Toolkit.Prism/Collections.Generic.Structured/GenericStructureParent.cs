using Hdc.Mvvm;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;

namespace Hdc.Collections.Generic
{
    public abstract class GenericStructureParent<TThis,
                                                 TChild,
                                                 TThisContext,
                                                 TChildContext> : ViewModel,
                                                                  IGenericStructureParent<TThis,
                                                                      TChild,
                                                                      TThisContext,
                                                                      TChildContext>
        where TThis : class, IGenericStructureParent<TThis,
                                 TChild,
                                 TThisContext,
                                 TChildContext>
        where TChild : IGenericStructureChild<TChildContext,
                           TThis,
                           TThisContext>
    {
        private readonly IList<TChild> _children = new ObservableCollection<TChild>();

        private TThisContext _context;

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        void IGenericStructureBase<TThisContext>.Initialize(TThisContext context)
        {
            _context = context;
            var childModels = GetChildContexts(context);

            int counter = 0;
            foreach (var childModel in childModels)
            {
                var tree = childModel;
                var child = ServiceLocator.GetInstance<TChild>();

                child.Initialize(tree);

                _children.Add(child);
                child.Parent = this as TThis;
                child.Index = counter;
                counter++;
            }

            Initialize(context);
        }

        protected virtual void Initialize(TThisContext context)
        {
        }

        public TThisContext Context
        {
            get { return _context; }
        }

        public IList<TChild> Children
        {
            get { return _children; }
        }

//        public TChild this[int index]
//        {
//            get { return _children[index]; }
//        }


        protected abstract IEnumerable<TChildContext> GetChildContexts(TThisContext thisContext);
    }
}