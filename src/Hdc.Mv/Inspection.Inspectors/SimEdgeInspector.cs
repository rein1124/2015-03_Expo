using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    class SimEdgeInspector : IEdgeInspector
    {
        public IList<EdgeSearchingResult> SearchEdges(HImage image, IList<EdgeSearchingDefinition> edgeSearchingDefinitions)
        {
            var sr = new EdgeSearchingResultCollection();

            int i = 0;

            foreach (var esd in edgeSearchingDefinitions)
            {
                var esr = new EdgeSearchingResult(i, esd)
                          {
                          };

                var startVector = new Vector(esd.StartX, esd.StartY);
                var endVector = new Vector(esd.EndX, esd.EndY);

                var linkVector = startVector - endVector;
                var centerVector = new Vector((startVector.X + endVector.X) / 2.0, (startVector.Y + endVector.Y) / 2.0);
                var newLengthRatio = (esd.ROIWidth * 1.0) / linkVector.Length;

                var matrix = new Matrix();
                matrix.Rotate(90);
                var verticalVector = linkVector * matrix;
                verticalVector *= newLengthRatio;
                var newStartVector = centerVector + verticalVector;

                var matrix2 = new Matrix();
                matrix2.Rotate(-90);
                var verticalVector2 = linkVector * matrix2;
                verticalVector2 *= newLengthRatio;
                var newEndVector = centerVector + verticalVector2;

                esr.EdgeLine = new Line(newStartVector.X, newStartVector.Y, newEndVector.X, newEndVector.Y);

                sr.Add(esr);

                i++;
            }

            return sr;
        }

        public EdgeSearchingResult SearchEdge(HImage image, EdgeSearchingDefinition definition)
        {
            var esr = new EdgeSearchingResult()
            {
                Definition = definition.DeepClone()
            };

            var startVector = new Vector(definition.StartX, definition.StartY);
            var endVector = new Vector(definition.EndX, definition.EndY);

            var linkVector = startVector - endVector;
            var centerVector = new Vector((startVector.X + endVector.X) / 2.0, (startVector.Y + endVector.Y) / 2.0);
            var newLengthRatio = (definition.ROIWidth * 1.0) / linkVector.Length;

            var matrix = new Matrix();
            matrix.Rotate(90);
            var verticalVector = linkVector * matrix;
            verticalVector *= newLengthRatio;
            var newStartVector = centerVector + verticalVector;

            var matrix2 = new Matrix();
            matrix2.Rotate(-90);
            var verticalVector2 = linkVector * matrix2;
            verticalVector2 *= newLengthRatio;
            var newEndVector = centerVector + verticalVector2;

            esr.EdgeLine = new Line(newStartVector.X, newStartVector.Y, newEndVector.X, newEndVector.Y);
            return esr;
        }
    }
}