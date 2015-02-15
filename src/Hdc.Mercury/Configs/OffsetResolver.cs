using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Hdc.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    [Export(typeof (IOffsetResolver))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OffsetResolver : IOffsetResolver
    {
        private class MarkNode : Node<MarkNode>
        {
            public MarkNode()
            {
                OffsetMark = new OffsetMark();
            }

            public int Offset { get; set; }
            public int Index { get; set; }
            public int GlobalIndex { get; set; }

            public OffsetMark OffsetMark { get; set; }
        }

        public IEnumerable<int> GetOffsets(IEnumerable<OffsetMark> offsetMarks)
        {
            var node = CreateNodeMark(offsetMarks);

            int counter = 0;
            Travel(node, ref counter);

            var leafs = new List<MarkNode>();
            GetLeafNodes(node, leafs);

            var offsets = leafs.Select(x => x.Offset);
            return offsets;
        }

        private void GetLeafNodes(MarkNode markNode, IList<MarkNode> leafNodes)
        {
            if (markNode.Nodes.Count() == 0)
            {
                leafNodes.Add(markNode);
                return;
            }
            else
            {
                foreach (var node in markNode.Nodes)
                {
                    GetLeafNodes(node, leafNodes);
                }
            }
        }


        private void Travel(MarkNode markNode, ref int counter)
        {
            counter += markNode.OffsetMark.Prefix;
            markNode.Offset = counter;
            foreach (var subNode in markNode.Nodes)
            {
                Travel(subNode, ref counter);
            }
            counter += markNode.OffsetMark.Suffix;
        }


        private MarkNode CreateNodeMark(IEnumerable<OffsetMark> offsetMarks)
        {

            IList<Tuple<int, OffsetMark>> paras;
            if(!offsetMarks.Any())
            paras = new List<Tuple<int, OffsetMark>>()
                        {
                            
                        };
            else
            {
            paras = offsetMarks.Select(x => new Tuple<int, OffsetMark>(x.Total, x)).ToList();    
            }
            var top = NodeExtensions.CreateNodes(
                paras,
                () => new MarkNode(),
                (offsetMark, globalIndex, index) => new MarkNode()
                                                        {
                                                            OffsetMark = offsetMark,
                                                            GlobalIndex = globalIndex,
                                                            Index = index
                                                        });
            return top;
        }
    }
}