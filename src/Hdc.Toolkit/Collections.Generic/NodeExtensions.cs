using System;
using System.Collections.Generic;
using System.Linq;

namespace Hdc.Collections.Generic
{
    public static class NodeExtensions
    {
        public static IList<TNode> GetAllNodesUsingTraverseFromTopLeft<TNode>(this TNode node)
            where TNode : INode<TNode>
        {
            var count = 0;

            IList<TNode> ids = new List<TNode>();

            TraverseFromTopLeft(node,
                                (t, p) =>
                                    {
                                        count++;
                                        ids.Add(t);
                                    });

            return ids;
        }


        public static IEnumerable<TNode> TraverseFromTopLeft<TNode>(this TNode node,
                                                                    Action<TNode, TNode> enterAction = null,
                                                                    Action<TNode, TNode> exitAction = null)
            where TNode : INode<TNode>
        {
            var nodes = new List<TNode>();

            if (enterAction != null) enterAction(node, default(TNode));
            TraverseFromTopLeft(node, nodes, enterAction, exitAction);
            if (exitAction != null) exitAction(node, default(TNode));

            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="traversedNodes"></param>
        /// <param name="enterAction">thisNode,parentNode</param>
        /// <param name="exitAction">thisNode,parentNode</param>
        private static void TraverseFromTopLeft<TNode>(TNode node,
                                                       ICollection<TNode> traversedNodes,
                                                       Action<TNode, TNode> enterAction = null,
                                                       Action<TNode, TNode> exitAction = null)
            where TNode : INode<TNode>
        {
            traversedNodes.Add(node);
            foreach (var childNode in node.Nodes)
            {
                if (enterAction != null) enterAction(childNode, node);
                TraverseFromTopLeft(childNode, traversedNodes, enterAction, exitAction);
                if (exitAction != null) exitAction(childNode, node);
            }
        }

        public static IEnumerable<TNode> TraverseFromBottomLeft<TNode>(this TNode node,
                                                                       Action<TNode, TNode> enterAction = null,
                                                                       Action<TNode, TNode> exitAction = null)
            where TNode : INode<TNode>
        {
            var nodes = new List<TNode>();

            if (enterAction != null) enterAction(node, default(TNode));
            TraverseFromBottomLeft(node, nodes, enterAction, exitAction);
            if (exitAction != null) exitAction(node, default(TNode));

            return nodes;
        }

        private static void TraverseFromBottomLeft<TNode>(TNode node,
                                                          ICollection<TNode> traversedNodes,
                                                          Action<TNode, TNode> enterAction = null,
                                                          Action<TNode, TNode> exitAction = null)
            where TNode : INode<TNode>
        {
            foreach (var childNode in node.Nodes)
            {
                if (enterAction != null) enterAction(childNode, node);
                TraverseFromBottomLeft(childNode, traversedNodes, enterAction, exitAction);
                if (exitAction != null) exitAction(childNode, node);
            }
            traversedNodes.Add(node);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <param name="levelParas"></param>
        /// <param name="createTopNode"></param>
        /// <param name="createNode">para,globalIndex,index:node</param>
        /// <returns></returns>
        public static TNode CreateNodes<TNode, TPara>(IEnumerable<Tuple<int, TPara>> levelParas,
                                                      Func<TNode> createTopNode,
                                                      Func<TPara, int, int, TNode> createNode)
            where TNode : INode<TNode>
        {
            var stack = new Stack<IList<TNode>>();

            foreach (var levelPara in levelParas)
            {
                if (stack.Count() == 0)
                {
                    var subNodes = new List<TNode>();
                    for (int i = 0; i < levelPara.Item1; i++)
                    {
                        var node = createNode(levelPara.Item2, i, i);
                        subNodes.Add(node);
                    }
                    stack.Push(subNodes);

                    continue;
                }
                else
                {
                    var parentNodes = stack.Peek();
                    var subNodes = new List<TNode>();
                    int counter = 0;
                    foreach (var parentNode in parentNodes)
                    {
                        for (int i = 0; i < levelPara.Item1; i++)
                        {
                            var sub = createNode(levelPara.Item2, counter, i);
                            parentNode.Nodes.Add(sub);
                            subNodes.Add(sub);
                            counter++;
                        }
                    }

                    stack.Push(subNodes);
                }
            }

            var top = createTopNode();
            foreach (var e in stack.Last())
            {
                top.Nodes.Add(e);
            }
            return top;
        }
    }
}