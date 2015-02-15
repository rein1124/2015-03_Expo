using System;
using System.Collections.Generic;

namespace Hdc.Collections
{
    public static class TraverseEx
    {
        public static TTarget TraverseMap<TSource, TTarget>(TSource source,
                                                            Func<TSource, TTarget> targetInitializer,
                                                            Func<TSource, IEnumerable<TSource>> findChildren,
                                                            Action<TTarget, TTarget> onChildCreated,
                                                            Stack<TTarget> stack = null)
        {
            if (stack == null)
                stack = new Stack<TTarget>();

            var dt = targetInitializer(source);

            stack.Push(dt);

            var children = findChildren(source);
            foreach (var childDeviceTreeConfig in children)
            {
                var childDt = TraverseMap(childDeviceTreeConfig, targetInitializer, findChildren, onChildCreated,
                                          stack);
                var peek = stack.Peek();
                onChildCreated(peek, childDt);
            }

            stack.Pop();

            return dt;
        }

        public static IEnumerable<TSource> TraverseFromTopLeft<TSource>(this TSource node,
                                                                        Func<TSource, IEnumerable<TSource>> findChildren,
                                                                        Action<TSource, TSource>
                                                                            enterAction = null,
                                                                        Action<TSource, TSource>
                                                                            exitAction = null)
        {
            var nodes = new List<TSource>();
            TraverseFromTopLeft(node, nodes, findChildren, enterAction, exitAction);

            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="node"></param>
        /// <param name="traversedNodes"></param>
        /// <param name="enterAction">thisNode,parentNode</param>
        /// <param name="exitAction">thisNode,parentNode</param>
        private static void TraverseFromTopLeft<TSource>(this TSource node,
                                                         ICollection<TSource> traversedNodes,
                                                         Func<TSource, IEnumerable<TSource>> findChildren,
                                                         Action<TSource, TSource> enterAction = null,
                                                         Action<TSource, TSource> exitAction = null)
        {
            traversedNodes.Add(node);
            var children = findChildren(node);
            foreach (var childNode in children)
            {
                if (enterAction != null) enterAction(childNode, node);
                TraverseFromTopLeft(childNode, traversedNodes, findChildren, enterAction, exitAction);
                if (exitAction != null) exitAction(childNode, node);
            }
        }

        public static IEnumerable<TSource> TraverseFromBottomLeft<TSource>(
            this TSource node,
            Func<TSource, IEnumerable<TSource>> findChildren,
            Action<TSource, TSource> enterAction = null,
            Action<TSource, TSource> exitAction = null)
        {
            var nodes = new List<TSource>();
            TraverseFromBottomLeft(node, nodes, findChildren, enterAction, exitAction);

            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="node"></param>
        /// <param name="traversedNodes"></param>
        /// <param name="enterAction">thisNode,parentNode</param>
        /// <param name="exitAction">thisNode,parentNode</param>
        private static void TraverseFromBottomLeft<TSource>(TSource node,
                                                            ICollection<TSource> traversedNodes,
                                                            Func<TSource, IEnumerable<TSource>> findChildren,
                                                            Action<TSource, TSource> enterAction = null,
                                                            Action<TSource, TSource> exitAction = null)
        {
            var children = findChildren(node);
            foreach (var childNode in children)
            {
                if (enterAction != null) enterAction(childNode, node);
                TraverseFromTopLeft(childNode, traversedNodes, findChildren, enterAction, exitAction);
                if (exitAction != null) exitAction(childNode, node);
            }
            traversedNodes.Add(node);
        }
    }
}