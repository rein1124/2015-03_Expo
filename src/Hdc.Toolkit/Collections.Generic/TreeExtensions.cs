using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hdc.Collections.Generic
{
    public static class TreeExtensions
    {
        public static ISimpleTree<T> ToTree<T>(this IList<Tuple<T, T>> pairs) where T : class
        {
            var top = pairs.Single(x => x.Item2 == null).Item1;
            var topNode = new SimpleTree<T>(top);
            Add(topNode, pairs);
            return topNode;
        }

        private static void Add<T>(SimpleTree<T> node, IList<Tuple<T, T>> pairs) where T : class
        {
            var childrenPairs = pairs.Where(tuple => tuple.Item2 == node.Data).ToList<Tuple<T, T>>();

            foreach (var cp in childrenPairs)
            {
                var c = new SimpleTree<T>(cp.Item1);
                node.Add(c);
                pairs.Remove(cp);

                Add<T>(c, pairs);
            }
        }

        public static ISimpleTree<T> ToSimpleTree<T>(this IDictionary<T, T> pairs) where T : class
        {
            var top = pairs.Single(x => x.Value == null).Key;
            var topNode = new SimpleTree<T>(top);
            Add(topNode, pairs);
            return topNode;
        }

        private static void Add<T>(SimpleTree<T> node, IDictionary<T, T> pairs) where T : class
        {
            var childrenPairs = pairs.Where(pair => pair.Value == node.Data).ToList<KeyValuePair<T, T>>();

            foreach (var cp in childrenPairs)
            {
                var c = new SimpleTree<T>(cp.Key);
                node.Add(c);
                pairs.Remove(cp);

                Add<T>(c, pairs);
            }
        }

        public static TTree ToTree<TTree>(this IEnumerable<TTree> trees, Func<TTree, TTree> getParentFunc)
            where TTree : class, ITree<TTree>
        {
            var newPairs = trees.ToDictionary(x => x, x => getParentFunc(x));

            return newPairs.ToTree();
        }

        public static TTree ToTree<TTree>(this IDictionary<TTree, TTree> pairs)
            where TTree : class, ITree<TTree>
        {
            //            var newPairs = pairs.ToDictionary(pair => pair.Key, pair => pair.Value);
            var newPairs = pairs.Clone();

            var top = newPairs.Single(x => x.Value == null).Key;
            Add(top, newPairs);
            return top;
        }

        private static void Add<TTree>(TTree parentNode, IDictionary<TTree, TTree> pairs)
            where TTree : ITree<TTree>
        {
            var childrenPairs = pairs.Where(pair => Equals(pair.Value, parentNode)).ToList();

            foreach (var cp in childrenPairs)
            {
                var childNode = cp.Key;
                parentNode.Add(childNode);
                pairs.Remove(cp);

                Add(childNode, pairs);
            }
        }


        public static IDictionary<T, T> ToDictionary<T>(this ISimpleTree<T> tree) where T : class
        {
            var nodes = tree.GetDescendantsIncludeSelf();

            var dict = new Dictionary<T, T>();

            foreach (var node in nodes)
            {
                if (node.IsRootNode)
                    dict.Add(node.Data, null);
                else
                {
                    dict.Add(node.Data, node.ParentNode.Data);
                }
            }

            return dict;
        }


        public static TTree ToDataTree<TData, TTree>(this IDictionary<TData, TData> pairs)
            where TTree : ITree<TData, TTree>, new()
            where TData : class
        {
            var top = pairs.Single(x => x.Value == null).Key;
            var topNode = new TTree() { Data = top };
            Add(topNode, pairs);
            return topNode;
        }

        private static void Add<TData, TTree>(ITree<TData, TTree> node, IDictionary<TData, TData> pairs)
            where TTree : ITree<TData, TTree>, new()
            where TData : class
        {
            var childrenPairs = pairs.Where(pair => pair.Value == node.Data).ToList<KeyValuePair<TData, TData>>();

            foreach (var cp in childrenPairs)
            {
                var c = new TTree() { Data = cp.Key };
                node.Add(c);
                pairs.Remove(cp);

                Add<TData, TTree>(c, pairs);
            }
        }

        public static TTree ToTree<TData, TTree>(this IList<Tuple<TData, TData>> pairs,
                                                 Expression<Func<TData, object>> idSelectorExp)
            where TTree : ITree<TData, TTree>, new()
            where TData : class
        {
            var copyedList = pairs.ToList<Tuple<TData, TData>>();
            Func<TData, object> del = idSelectorExp.Compile();

            var rootPair = copyedList.Single(x => x.Item2 == null);

            copyedList.Remove(rootPair);

            var rootData = rootPair.Item1;
            var rootNode = new TTree { Data = rootData };

            Add(rootNode, copyedList, del);

            return rootNode;
        }

        private static void Add<TData, TTree>(ITree<TData, TTree> node,
                                              IList<Tuple<TData, TData>> pairs,
                                              Func<TData, object> idSelector)
            where TTree : ITree<TData, TTree>, new()
            where TData : class
        {
            var nodeId = idSelector(node.Data);
            var childrenPairs = pairs.Where(pair =>
                                                {
                                                    var childId = idSelector(pair.Item2);
                                                    return childId == nodeId;
                                                }).ToList<Tuple<TData, TData>>();

            foreach (var cp in childrenPairs)
            {
                var c = new TTree() { Data = cp.Item1 };
                node.Add(c);
                pairs.Remove(cp);

                Add<TData, TTree>(c, pairs, idSelector);
            }
        }



        public static IDictionary<T, T> ConvertToDictionary<T>(T node, Func<T, IEnumerable<T>> findChildNodes, T parentNode = default(T))
        {
            var dic = new Dictionary<T, T>();
            ConvertToDictionary(node, findChildNodes, dic, parentNode);
            return dic;
        }

        private static void ConvertToDictionary<T>(T node, Func<T, IEnumerable<T>> findChildNodes, IDictionary<T, T> dictionary, T parentNode)
        {
            dictionary.Add(parentNode, node);
            var children = findChildNodes(node);

            foreach (var child in children)
            {
                ConvertToDictionary(child, findChildNodes, dictionary, node);
            }
        }

        public static IList<T> Traverse<T>(this T node, Func<T, IEnumerable<T>> findChildNodes)
        {
            var list = new List<T>();
            Traverse(node, findChildNodes, list);
            return list;
        }

        private static void Traverse<T>(this T node, Func<T, IEnumerable<T>> findChildNodes, IList<T> list)
        {
            list.Add(node);

            var children = findChildNodes(node);

            foreach (var child in children)
            {
                Traverse(child, findChildNodes, list);
            }
        }
        public static IList<TTree> Traverse<TData, TTree>(this TTree tree) where TTree : ITree<TData, TTree>
        {
            return Traverse<TData, TTree>(tree, null, null);
        }

        public static IList<TTree> Traverse<TData, TTree>(this TTree tree, Action<TTree> preAction)
                where TTree : ITree<TData, TTree>
        {
            return Traverse<TData, TTree>(tree, preAction, null);
        }

        public static IList<TTree> Traverse<TData, TTree>(this TTree tree, Action<TTree> preAction, Action<TTree> postAction)
                where TTree : ITree<TData, TTree>
        {
            if (preAction != null)
                preAction(tree);

            IList<TTree> currentList = new List<TTree>();
            currentList.Add(tree);
            foreach (var child in tree.ChildNodes)
            {
                var childList = Traverse<TData, TTree>(child, preAction, postAction);
                foreach (var childSub in childList)
                {
                    currentList.Add(childSub);
                }
                currentList.Add(tree);
            }
            if (postAction != null)
                postAction(tree);

            return currentList;
        }

        public static IList<TTree> Traverse<TTree>(this TTree tree,
            Action<TTree> preAction = null,
            Action<TTree> postAction = null)
                where TTree : ITree<TTree>
        {
            if (preAction != null)
                preAction(tree);

            IList<TTree> currentList = new List<TTree>();
            currentList.Add(tree);
            foreach (var child in tree.ChildNodes)
            {
                var childList = Traverse<TTree>(child, preAction, postAction);
                foreach (var childSub in childList)
                {
                    currentList.Add(childSub);
                }
                //currentList.Add(tree);
            }
            if (postAction != null)
                postAction(tree);

            return currentList;
        }
    }
}