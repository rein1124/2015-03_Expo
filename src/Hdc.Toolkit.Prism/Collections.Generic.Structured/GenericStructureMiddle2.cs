using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Collections.Generic
{
    public abstract class GenericStructureMiddle2<TThis,
                                                  TParent,
                                                  TChild,
                                                  TThisContext,
                                                  TParentContext,
                                                  TChildContext> : IGenericStructureMiddle<
                                                                       TThis,
                                                                       TParent,
                                                                       TChild,
                                                                       TThisContext,
                                                                       TParentContext,
                                                                       TChildContext>
        where TThis : class, IGenericStructureMiddle<
                                 TThis,
                                 TParent,
                                 TChild,
                                 TThisContext,
                                 TParentContext,
                                 TChildContext>
        where TParent : IGenericStructureBase<TParentContext>
        where TChild : IGenericStructureChild<
                           TChildContext,
                           TThis,
                           TThisContext>
    {
        public int Index { get; set; }

        public TParent Parent { get; set; }

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

        protected abstract IEnumerable<TChildContext> GetChildContexts(TThisContext thisContext);
    }
}