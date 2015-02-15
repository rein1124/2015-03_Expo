using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Threading;
using Hdc.Mvvm;
//using Kent.Boogaart.Truss;
//using Kent.Boogaart.Truss.Fluent;
using Hdc.Reactive;
using Hdc.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Hdc.Collections.Generic
{
    public abstract class ViewModelContextNodeMiddle<TThis,
        TThisContext,
        TParent,
        TParentContext,
        TChild,
        TChildContext> : ViewModel,
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

        //        private BindingManager _bm;

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

            // handle ValueMonitorAttribute
            var apis = this.GetAttributePropertyInfos<ValueMonitorAttribute>(false);
            foreach (var api in apis)
            {
                var attribute = api.Attribute as ValueMonitorAttribute;
                var vm = this.GetPropertyValue(api.PropertyInfo.Name);
                if (vm == null)
                {
                    var dataType = api.PropertyInfo.PropertyType.GetGenericArguments()[0];

                    var vmType = typeof (IValueMonitor<>).MakeGenericType(dataType);
                    //vm = vmType.CreateInstance(); 
                    vm = ServiceLocator.GetInstance(vmType);
                    
                    this.SetPropertyValue(api.PropertyInfo.Name, vm);
                }


                //
                string displayPropertyName;

                if (string.IsNullOrEmpty(attribute.PropertyName))
                {
                    var i = api.PropertyInfo.Name.LastIndexOf("Monitor", System.StringComparison.Ordinal);
                    if (i == -1)
                    {
                        throw new InvalidOperationException("ValueMonitorAttribute must declare name end with [Monitor]");
                    }
                    displayPropertyName = api.PropertyInfo.Name.Remove(i, "Monitor".Count());
                }
                else
                {
                    displayPropertyName = attribute.PropertyName;
                }
                
                var dvm = vm as IDisplayValueMonitor;
                dvm.RaisePropertyChangedOnViewModelUsingDispatcher(this, displayPropertyName);
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
            set
            {
                if (Equals(_children, value)) return;
                _children = value;
                RaisePropertyChanged(() => Children);
            }
        }

        public TParent Parent
        {
            get { return _parent; }
            set
            {
                if (Equals(_parent, value)) return;
                _parent = value;
                RaisePropertyChanged(() => Parent);
            }
        }

        public int Index
        {
            get { return _index; }
            set
            {
                if (Equals(_index, value)) return;
                _index = value;
                RaisePropertyChanged(() => Index);
            }
        }

        public TThisContext Context
        {
            get { return _context; }
        }

        protected abstract IEnumerable<TChildContext> GetChildContexts(TThisContext thisContext);

        public void BindingTo(TThisContext context)
        {
            //            if (_bm != null)
            //            {
            //                _bm.Bindings.Clear();
            //                _bm.Dispose();
            //                _bm = null;
            //            }
            //
            //            _bm = new BindingManager();

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

            OnBindingToCompleted(context);
        }

        protected virtual void OnBindingTo(TThisContext context)
        {
        }

        protected virtual void OnBindingToCompleted(TThisContext context)
        {
        }
    }
}