using System;
using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public class WeakReferenceCollection<T> : ICollection<T> where T : class
    {
        private readonly List<WeakReference> _references = new List<WeakReference>();

        #region ICollection<T> Members

        public void Add(T item)
        {
            if (Contains(item)) return;
            Add(new WeakReference(item));
        }

        public bool Contains(T item)
        {
            foreach (var each in this)
            {
                if (ReferenceEquals(each, item))
                    return true;
            }
            return false;
        }

        private void Add(WeakReference item)
        {
            _references.Add(item);
        }

        public void Clear()
        {
            _references.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var each in this)
            {
                array[arrayIndex++] = each;
            }
        }

        public int Count
        {
            get
            {
                int cnt = 0;
                var enumerator = GetEnumerator();
                while (enumerator.MoveNext()) cnt++;
                return cnt;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            foreach (var each in _references)
            {
                var target = each.Target;
                if (!ReferenceEquals(target, item))
                    continue;
                return _references.Remove(each);
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            var removables = new List<WeakReference>();
            foreach (var each in _references)
            {
                var target = each.Target as T;
                if (null != target)
                {
                    yield return target;
                }
                else
                {
                    removables.Add(each);
                }
            }
            foreach (var each in removables)
            {
                _references.Remove(each);
            }
            yield break;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}