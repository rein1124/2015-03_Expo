using System.Windows;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;

namespace Hdc.Collections.Generic.Levels
{
    public abstract class DpGenericStructureParent<TThis,
                                                   TChild,
                                                   TThisContext,
                                                   TChildContext> : DependencyObject,
                                                                    IGenericCTreeParent<TThis,
                                                                        TChild,
                                                                        TThisContext,
                                                                        TChildContext>
        where TThis : class, IGenericCTreeParent<TThis,
                                 TChild,
                                 TThisContext,
                                 TChildContext>
        where TChild : IGenericCTreeChild<TChildContext>
    {
        private readonly IList<TChild> _children = new ObservableCollection<TChild>();

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        private TThisContext _context;

        void IGenericCTreeBase<TThisContext>.Initialize(TThisContext context)
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