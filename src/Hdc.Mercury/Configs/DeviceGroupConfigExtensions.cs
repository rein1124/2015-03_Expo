using System;
using System.Collections.Generic;
using System.Linq;
using Hdc.Collections;

namespace Hdc.Mercury.Configs
{
    public static class DeviceGroupConfigExtensions
    {
        public static IEnumerable<DeviceGroupConfig> TraverseFromTopLeft(this DeviceGroupConfig node,
                                                                         Action<DeviceGroupConfig, DeviceGroupConfig>
                                                                             enterAction = null,
                                                                         Action<DeviceGroupConfig, DeviceGroupConfig>
                                                                             exitAction = null)
        {
            var nodes = new List<DeviceGroupConfig>();

            if (enterAction != null) enterAction(node, null);
            TraverseFromTopLeft(node, nodes, enterAction, exitAction);
            if (exitAction != null) enterAction(node, null);

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
        private static void TraverseFromTopLeft(DeviceGroupConfig node,
                                                ICollection<DeviceGroupConfig> traversedNodes,
                                                Action<DeviceGroupConfig, DeviceGroupConfig> enterAction = null,
                                                Action<DeviceGroupConfig, DeviceGroupConfig> exitAction = null)
        {
            traversedNodes.Add(node);
            foreach (var childNode in node.GroupConfigCollection)
            {
                if (enterAction != null) enterAction(childNode, node);
                TraverseFromTopLeft(childNode, traversedNodes, enterAction, exitAction);
                if (exitAction != null) exitAction(childNode, node);
            }
        }

        public static IEnumerable<DeviceGroupConfig> TraverseFromBottomLeft(this DeviceGroupConfig node,
                                                                            Action<DeviceGroupConfig, DeviceGroupConfig>
                                                                                enterAction = null,
                                                                            Action<DeviceGroupConfig, DeviceGroupConfig>
                                                                                exitAction = null)
        {
            var nodes = new List<DeviceGroupConfig>();

            if (enterAction != null) enterAction(node, null);
            TraverseFromBottomLeft(node, nodes, enterAction, exitAction);
            if (exitAction != null) enterAction(node, null);

            return nodes;
        }

        private static void TraverseFromBottomLeft(this DeviceGroupConfig node,
                                                   ICollection<DeviceGroupConfig> traversedNodes,
                                                   Action<DeviceGroupConfig, DeviceGroupConfig> enterAction = null,
                                                   Action<DeviceGroupConfig, DeviceGroupConfig> exitAction = null)
        {
            foreach (var childNode in node.GroupConfigCollection)
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
        public static DeviceGroupConfig CreateNodes<TPara>(IEnumerable<Tuple<int, TPara>> levelParas,
                                                           Func<DeviceGroupConfig> createTopNode,
                                                           Func<TPara, int, int, DeviceGroupConfig> createNode)
        {
            var stack = new Stack<IList<DeviceGroupConfig>>();

            foreach (var levelPara in levelParas)
            {
                if (!stack.Any())
                {
                    var subNodes = new List<DeviceGroupConfig>();
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
                    var subNodes = new List<DeviceGroupConfig>();
                    int counter = 0;
                    foreach (var parentNode in parentNodes)
                    {
                        for (int i = 0; i < levelPara.Item1; i++)
                        {
                            var sub = createNode(levelPara.Item2, counter, i);
                            parentNode.GroupConfigCollection.Add(sub);
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
                top.GroupConfigCollection.Add(e);
            }
            return top;
        }
    }
}