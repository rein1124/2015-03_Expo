using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Hdc.Collections.Generic
{
    public class ContextNodeMiddle<TThis,
                                            TThisContext,
                                            TParent,
                                            TParentContext,
                                            TChild,
                                            TChildContext> :
                                                IContextNodeMiddle<
                                                    TThis,
                                                    TThisContext,
                                                    TParent,
                                                    TParentContext,
                                                    TChild,
                                                    TChildContext>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           TThis,
                           TThisContext>
        where TThis : class, IContextNodeMiddle<
                                 TThis,
                                 TThisContext,
                                 TParent,
                                 TParentContext,
                                 TChild,
                                 TChildContext>
    {
        private IList<TChild> _children = new List<TChild>();

        private TParent _parent;

        private int _index;

        private TThisContext _context;

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public void Initialize(TThisContext context)
        {
            _context = context;

            OnInitializing(context);

            var childContexts = GetChildContexts(context);

            int counter = 0;

            foreach (var childContext in childContexts)
            {
                var child = ServiceLocator.GetInstance<TChild>();

                child.Parent = this as TThis;
                _children.Add(child);
                child.Index = counter;
                child.Initialize(childContext);

                counter++;
            }

            OnInitialized(context);
        }

        protected virtual void OnInitialized(TThisContext context)
        {
        }

        protected virtual void OnInitializing(TThisContext context)
        {
        }

        public void Reset()
        {
            OnResetting(Context);

            Children.ForEach(c =>
            {
                c.Reset();
                c.Parent = null;
            });
            Children.Clear();

            var context = _context;
            _context = default(TThisContext);

            OnReset(context);
        }

        protected virtual void OnResetting(TThisContext context)
        {
        }

        protected virtual void OnReset(TThisContext context)
        {
        }

        public IList<TChild> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public TParent Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public TThisContext Context
        {
            get { return _context; }
        }

        protected virtual IEnumerable<TChildContext> GetChildContexts(TThisContext thisContext)
        {
            yield break;
        }

        public void BindingTo(TThisContext context)
        {

            _context = context;

            OnBindingTo(context);

            //            foreach (var child in _children)
            //            {
            //                child.BindingTo(child.Context);
            //            }

            var newChildContexts = GetChildContexts(context).ToList();

            for (int i = 0; i < _children.Count(); i++)
            {
                var child = _children[i];
                var newChildContext = newChildContexts[i];
                child.BindingTo(newChildContext);
            }
        }

        protected virtual void OnBindingTo(TThisContext context)
        {
        }
    }
}