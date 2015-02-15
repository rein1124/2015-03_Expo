namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
  

    public class Tree<TData, TTree> : ITree<TData, TTree> where TTree : class, ITree<TData, TTree>
    {
        private IList<TTree> _childNodes = new List<TTree>();

        private TTree _parentNode;

        private TData _data; 

        protected Tree() : this(default(TData))
        {
        }

        protected Tree(params TTree[] children) : this(default(TData), children as IEnumerable<TTree>)
        {
        }

        protected Tree(TData data, params TTree[] children) : this(data, children as IEnumerable<TTree>)
        {
        }

        protected Tree(TData data, IEnumerable<TTree> children)
        {
            var childNodes = new List<TTree>();
          

            _childNodes = childNodes;

            _data = data;

            children.ArgumentIsNotNull("children");
            foreach (var child in children)
            {
                Add(child);
            }
        }

        public List<TTree> ConcreteChildren
        {
            get { return _childNodes as List<TTree>; }
        }


        public IList<TTree> ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }


        public TTree ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }


        public int Degree
        {
            get { return _childNodes.Count; }
        }


        public int Depth
        {
            get { return GetLevel(); }
        }


        public bool IsLeafNode
        {
            get { return _childNodes.Count == 0; }
        }


        public bool IsRootNode
        {
            get { return _parentNode == null; }
        }

        public TData Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public bool IsReadOnly
        {
            get { return _childNodes.IsReadOnly; }
        }


        public int Count
        {
            get { return _childNodes.Count; }
        }


        public int Height
        {
            get
            {
                if (Degree == 0)
                    return 0;

                return 1 + FindMaximumChildHeight();
            }
        }


        public int Index
        {
            get
            {
                if (_parentNode == null)
                    return 0;

                return _parentNode.ChildNodes.IndexOf(this as TTree);
            }
        }


        public TTree this[int index]
        {
            get { return _childNodes[index]; }
            set { _childNodes[index] = value; }
        }

        public IEnumerable<TTree> GetLeafNodes()
        {
            if (IsLeafNode)
                yield return this as TTree;
            else
            {
                foreach (var child in _childNodes)
                {
                    var subs = child.GetLeafNodes();
                    foreach (var sub in subs)
                    {
                        yield return sub;
                    }
                }
            }
        }

        public void Add(TTree child)
        {
            _childNodes.Add(child);
            child.ParentNode = this as TTree;
        }

        public void CopyTo(TTree[] array, int arrayIndex)
        {
            _childNodes.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(TTree child)
        {
            var isRemoved = _childNodes.Remove(child);
            if (isRemoved)
                child.ParentNode = null;

            return isRemoved;
        }

        public void Remove(TData data)
        {
            Remove(FindChild(data));
        }

        public int IndexOf(TTree item)
        {
            return _childNodes.IndexOf(item);
        }

        public void Insert(int index, TTree item)
        {
            _childNodes.Insert(index, item);
            item.ParentNode = this as TTree;

            var enumerable = _childNodes.Select(node => _childNodes.IndexOf(node) > index);
            var lastItems = enumerable.ToList();
        }

        public void RemoveAt(int index)
        {
            Remove(_childNodes[index]);
        }

        public void Clear()
        {
            var children = _childNodes.ToList<TTree>();
            foreach (var child in children)
            {
                Remove(child);
            }
        }

        public bool Contains(TTree item)
        {
            return _childNodes.Contains(item);
        }

        private TTree FindChild(TData data)
        {
            foreach (var child in _childNodes)
            {
                if (child.Data.Equals(data))
                    return child;
            }

            throw new InvalidOperationException("cannot find tree from data.");
        }

        public IList<TTree> GetAncestors()
        {
            var path = new List<TTree>();

            for (var node = ParentNode; node != null; node = node.ParentNode)
            {
                path.Add(node);
            }

            path.Reverse();

            return path;
        }

        public IList<TTree> GetAncestorsIncludeSelf()
        {
            var ancestors = GetAncestors();
            ancestors.Add(this as TTree);

            return ancestors;
        }

        public TTree GetAncestor(int upLevel)
        {
            if (upLevel < 0)
                return null;
            var ans = GetAncestorsIncludeSelf().Reverse().ToList<TTree>();
            return upLevel < ans.Count ? ans[upLevel] : null;
        }

        public IEnumerable<TTree> GetDescendants()
        {
            var stack = new Stack<TTree>();

            stack.Push(this as TTree);

            while (stack.Count > 0)
            {
                var tree = stack.Pop();

                if (tree != null)
                {
                    if (!Equals(tree, this))
                        yield return tree;

                    for (var i = 0; i < tree.ChildNodes.Count; i++)
                    {
                        stack.Push(tree.ChildNodes[i]);
                    }
                }
            }
        }

        public IEnumerable<TData> GetDescendantDatas()
        {
            return GetDescendants().Select(d => d.Data);
        }

        public IEnumerable<TTree> GetDescendantsIncludeSelf()
        {
            var descendants = GetDescendants().ToList<TTree>();
            descendants.Insert(0, this as TTree);
            return descendants;
        }

        private int FindMaximumChildHeight()
        {
            var maximum = 0;

            for (var i = 0; i < Degree; i++)
            {
                var childHeight = _childNodes[i].Height;

                if (childHeight > maximum)
                    maximum = childHeight;
            }

            return maximum;
        }

        public int GetLevel()
        {
            return GetAncestors().Count;
        }

        public IEnumerable<TTree> Traverse()
        {
            var stack = new Stack<TTree>();

            stack.Push(this as TTree);

            while (stack.Count > 0)
            {
                var tree = stack.Pop();

                if (tree != null)
                {
                    yield return tree;

                    for (var i = 0; i < tree.Degree; i++)
                    {
                        stack.Push(tree[i]);
                    }
                }
            }
        }

        public TTree FindNode(Predicate<TTree> condition)
        {
            foreach (var childNode in _childNodes)
            {
                if (condition(childNode))
                    return childNode;
            }

            return null;
        }
    }
}