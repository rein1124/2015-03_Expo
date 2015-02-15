using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Collections.Generic
{
    public abstract class CompositeNode<TContext,
                                        TChild,
                                        TChildContext> : ComplexNode<TContext>,
                                                         ICompositeNode<TContext,
                                                             TChild,
                                                             TChildContext>
        where TChild : IComplexNode<TChildContext>
    {
        private readonly IList<TChild> _children = new ObservableCollection<TChild>();

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public IList<TChild> Children
        {
            get { return _children; }
        }

        public override void OnInitialized()
        {
            var childContexts = GetChildContexts(Context);

            int counter = 0;
            foreach (var childContext in childContexts)
            {
                var child = ServiceLocator.GetInstance<TChild>();

                child.Initialize(childContext, counter);
                counter++;

                _children.Add(child);
            }
        }


        protected abstract IEnumerable<TChildContext> GetChildContexts(TContext thisContext);
    }
}