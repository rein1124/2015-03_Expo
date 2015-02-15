using System;
using System.Windows;

namespace Hdc.Collections.Generic
{
    public class ComplexNode<TContext> : DependencyObject, IComplexNode<TContext>
    {
        private TContext _context;

        private int _index;

        public void Initialize(TContext context, int index = 0)
        {
            _context = context;
            _index = index;

            OnInitialized();
        }

        public virtual void OnInitialized()
        {
        }

        public TContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public int Index
        {
            get { return _index; }
        }

        public int DisplayIndex
        {
            get { return _index + 1; }
        }
    }
}