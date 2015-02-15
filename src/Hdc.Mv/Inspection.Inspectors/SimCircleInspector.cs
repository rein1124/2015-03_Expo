using System;
using System.Collections.Generic;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    class SimCircleInspector : ICircleInspector
    {
        public IList<CircleSearchingResult> SearchCircles(HImage image, IList<CircleSearchingDefinition> circleSearchingDefinitions)
        {
            var searchingResult = new CircleSearchingResultCollection();
            Random random = new Random();

            for (int i = 0; i < circleSearchingDefinitions.Count; i++)
            {
                var definition = circleSearchingDefinitions[i];
                int randomFactor = 0;
                var csr = new CircleSearchingResult()
                          {
                              Index = i,
                              Name = definition.Name,
                              Circle =
                                  new Circle(definition.CenterX + random.NextDouble() * randomFactor,
                                  definition.CenterY + random.NextDouble() * randomFactor,
                                  (definition.InnerRadius + definition.OuterRadius) / 2.0 + random.NextDouble() * randomFactor),
                              Definition = definition.DeepClone(),
                          };
                //                if (i == 2)
                //                    csr.IsNotFound = true;

                searchingResult.Add(csr);
            }

            return searchingResult;
        }

        public CircleSearchingResult SearchCircle(HImage image, CircleSearchingDefinition definition)
        {
            Random random = new Random();
            int randomFactor = 0;
            var csr = new CircleSearchingResult()
            {
//                Index = i,
                Name = definition.Name,
                Circle =
                    new Circle(definition.CenterX + random.NextDouble() * randomFactor,
                    definition.CenterY + random.NextDouble() * randomFactor,
                    (definition.InnerRadius + definition.OuterRadius) / 2.0 + random.NextDouble() * randomFactor),
                Definition = definition.DeepClone(),
            };

            return csr;
        }
    }
}