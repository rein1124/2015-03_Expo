namespace Hdc.Collections.Generic
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System;

    public interface ITree<TData, TTree> where TTree : ITree<TData, TTree>
    {
        TTree ParentNode { get; set; }

        IList<TTree> ChildNodes { get; set; }

        IList<TTree> GetAncestors();

        IList<TTree> GetAncestorsIncludeSelf();

        TTree GetAncestor(int upLevel);

        IEnumerable<TTree> GetDescendants();

        IEnumerable<TTree> GetDescendantsIncludeSelf();

        IEnumerable<TData> GetDescendantDatas();

        void Remove(TData data);

        int Degree { get; }

        bool IsLeafNode { get; }

        bool IsRootNode { get; }

        IEnumerable<TTree> GetLeafNodes();

        TData Data { get; set; }

        int Height { get; }

        int Depth { get; }

        int GetLevel();

        IEnumerable<TTree> Traverse();

        int Index { get; }

        TTree FindNode(Predicate<TTree> condition);

        void Add(TTree item);

        void Clear();

        bool Contains(TTree item);

        void CopyTo(TTree[] array, int arrayIndex);

        bool Remove(TTree item);

        int Count { get; }

        bool IsReadOnly { get; }

        int IndexOf(TTree item);

        void Insert(int index, TTree item);

        void RemoveAt(int index);

        TTree this[int index] { get; set; }
    }
}