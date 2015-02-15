using System.Collections.Generic;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface ICircleInspector
    {
        CircleSearchingResult SearchCircle(HImage image, CircleSearchingDefinition definition);
    }
}