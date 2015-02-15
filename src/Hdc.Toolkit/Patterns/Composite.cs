using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace Hdc.Patterns
{
    /// <summary>
    /// Composite object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Composite<T>
    {
        protected List<T> _childrenList;

        public Composite()
        {
            _childrenList = new List<T>();
        }

        /// <summary>
        /// Add a child node.
        /// </summary>
        /// <param name="node"></param>
        public virtual void Add(T node)
        {
            _childrenList.Add(node);
        }

        /// <summary>
        /// Number of children.
        /// </summary>
        public int Count
        {
            get { return _childrenList.Count; }
        }

        /// <summary>
        /// Determine if this has children.
        /// </summary>
        public bool HasChildren
        {
            get { return _childrenList != null && _childrenList.Count > 0; }
        }

        /// <summary>
        /// Children.
        /// </summary>
        public ReadOnlyCollection<T> Children
        {
            get { return new ReadOnlyCollection<T>(_childrenList); }
        }
    }
}