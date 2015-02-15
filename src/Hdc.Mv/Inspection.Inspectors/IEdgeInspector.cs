using System.Collections.Generic;
using HalconDotNet;
using Hdc.Mv.Inspection;

namespace Hdc.Mv.Inspection
{
    public interface IEdgeInspector
    {
//        IList<EdgeSearchingResult> SearchEdges(HImage image, IList<EdgeSearchingDefinition> edgeSearchingDefinitions);
        EdgeSearchingResult SearchEdge(HImage image, EdgeSearchingDefinition definition);
    }
}