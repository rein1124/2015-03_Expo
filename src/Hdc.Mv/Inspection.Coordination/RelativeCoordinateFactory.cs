using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Hdc.Mv.Halcon;
using Hdc.Mv.Inspection;
using Hdc.Windows;

namespace Hdc.Mv
{
    public static class RelativeCoordinateFactory
    {
        public static IRelativeCoordinate CreateCoordinate(IList<CircleSearchingResult> circleDefinitions)
        {
            List<Vector> actualVectors = circleDefinitions.Select(x => x.Circle.GetCenterVector()).ToList();

            var expectVectors = new List<Vector>(
                circleDefinitions.Select(x => new Vector(
                    x.Definition.BaselineX*1000.0/16.0,
                    x.Definition.BaselineY*1000.0/16.0))) {};

            //            var expectVectors = expertVectors.Select(x => new Vector(
            //                x.X*1000.0/16.0,
            //                x.Y*1000.0/16.0)).ToList();

            var coordinate = CreateCoordinate(actualVectors, expectVectors);
            return coordinate;
        }

        public static IRelativeCoordinate CreateCoordinate(IList<Vector> actualVectors,
                                                           IList<Vector> expectVectors)
        {
            if (actualVectors.Count == 0 || expectVectors.Count == 0)
            {
                return new MockRelativeCoordinate();
            }

            //            var actualVectors = actualVectors2.ToList();
            //            var expectVectors = expectVectors2.Select(x => new Vector(
            //                x.X * 1000.0 / 16.0,
            //                x.Y * 1000.0 / 16.0)).ToList();

            // expect
            var expertAvgX = expectVectors.Average(x => x.X);
            var expertAvgY = expectVectors.Average(x => x.Y);
            var expertAvgVector = new Vector(expertAvgX, expertAvgY);
            var expertAvgVectorAngle = new Vector(100, 0).GetAngleTo(expertAvgVector);

            var expertMidToEachCircles = expectVectors.Select(x => expertAvgVector - x).ToList();

            // actual
            Vector actualAvgVectorOK; // origin OK
            double actualAvgVectorOKAngle;

            var actualAvgX = actualVectors.Average(x => x.X);
            var actualAvgY = actualVectors.Average(x => x.Y);
            actualAvgVectorOK = new Vector(actualAvgX, actualAvgY);

            var actualMidToCs = actualVectors.Select(x => actualAvgVectorOK - x).ToList();

            List<double> angleDiffCs = new List<double>();
            for (int i = 0; i < actualMidToCs.Count; i++)
            {
                var actualMidToC = actualMidToCs[i];
                var expertMidToC = expertMidToEachCircles[i];
                var angleDiff = actualMidToC.GetAngleTo(expertMidToC);
                angleDiffCs.Add(angleDiff);
            }

            var angleDiffAvg = angleDiffCs.Average();

            //
            var coord = new RelativeCoordinate(actualAvgVectorOK, -angleDiffAvg)
                        {
                            OriginOffset = -expertAvgVector
                        };

            //            var origin = coord.GetRelativeVector(actualVectors.First());
            //            coord.OriginOffset = origin;

            return coord;
        }

        public static IRelativeCoordinate CreateCoordinateUsingBorder(IList<EdgeSearchingResult> results)
        {
//            foreach (var edgeSearchingResult in results)
//            {
//                if (edgeSearchingResult.EdgeLine.GetLength() <= 1)
//                {
//                    throw new CreateCoordinateFailedException("CreateCoordinateUsingBorder failed. Edge: " + edgeSearchingResult.Name);
//                }
//            }

            List<EdgeSearchingResult> topEdges = new List<EdgeSearchingResult>();
            List<EdgeSearchingResult> bottomEdges = new List<EdgeSearchingResult>();
            List<EdgeSearchingResult> leftEdges = new List<EdgeSearchingResult>();
            List<EdgeSearchingResult> rightEdges = new List<EdgeSearchingResult>();
            List<Line> horizontalMiddleLines = new List<Line>();
            List<Line> verticalMiddleLines = new List<Line>();
            List<double> horizontalAngles = new List<double>();
            List<double> vertialAngles = new List<double>();
            Vector axisXVector = new Vector(10000, 0);

            foreach (var result in results)
            {
                if (result.Definition.Name.StartsWith("T"))
                    topEdges.Add(result);
                if (result.Definition.Name.StartsWith("B"))
                    bottomEdges.Add(result);
                if (result.Definition.Name.StartsWith("L"))
                    leftEdges.Add(result);
                if (result.Definition.Name.StartsWith("R"))
                    rightEdges.Add(result);
            }

            topEdges = topEdges.OrderBy(x => x.Name).ToList();
            bottomEdges = bottomEdges.OrderBy(x => x.Name).ToList();
            leftEdges = leftEdges.OrderBy(x => x.Name).ToList();
            rightEdges = rightEdges.OrderBy(x => x.Name).ToList();

            for (int index = 0; index < topEdges.Count; index++)
            {
                var topEdge = topEdges[index];
                var bottomEdge = bottomEdges[index];

                if (topEdge.EdgeLine.GetLength() < 1 || bottomEdge.EdgeLine.GetLength() < 1)
                {
                    topEdges.RemoveAt(index);
                    bottomEdges.RemoveAt(index);
                }
            }

            for (int index = 0; index < leftEdges.Count; index++)
            {
                var leftEdge = leftEdges[index];
                var rightEdge = rightEdges[index];

                if (leftEdge.EdgeLine.GetLength() < 1 || rightEdge.EdgeLine.GetLength() < 1)
                {
                    leftEdges.RemoveAt(index);
                    rightEdges.RemoveAt(index);
                }
            }

            if (!topEdges.Any() || !bottomEdges.Any() || !leftEdges.Any() || !rightEdges.Any())
            {
                throw new CreateCoordinateFailedException("CreateCoordinateUsingBorder failed.");
            }

            for (int i = 0; i < topEdges.Count; i++)
            {
                var topEdge = topEdges[i];
                var bottomEdge = bottomEdges[i];

                Line topLine = topEdge.EdgeLine.X1 < topEdge.EdgeLine.X2
                    ? topEdge.EdgeLine
                    : topEdge.EdgeLine.Reverse();

                Line bottomLine = bottomEdge.EdgeLine.X1 < bottomEdge.EdgeLine.X2
                    ? bottomEdge.EdgeLine
                    : bottomEdge.EdgeLine.Reverse();

                var middleLine = topLine.GetMiddleLineUsingAngle(bottomLine);
                horizontalMiddleLines.Add(middleLine);

                var topVectorFrom1To2 = topLine.GetVectorFrom2To1();
                var topAngle = axisXVector.GetAngleTo(topVectorFrom1To2);

                var bottomVectorFrom1To2 = bottomLine.GetVectorFrom2To1();
                var bottomAngle = axisXVector.GetAngleTo(bottomVectorFrom1To2);

                horizontalAngles.Add(topAngle);
                horizontalAngles.Add(bottomAngle);
            }

            for (int i = 0; i < leftEdges.Count; i++)
            {
                var leftEdge = leftEdges[i];
                var rightEdge = rightEdges[i];

                Line leftLine = leftEdge.EdgeLine.Y1 < leftEdge.EdgeLine.Y2
                    ? leftEdge.EdgeLine
                    : leftEdge.EdgeLine.Reverse();

                Line rightLine = rightEdge.EdgeLine.Y1 < rightEdge.EdgeLine.Y2
                    ? rightEdge.EdgeLine
                    : rightEdge.EdgeLine.Reverse();

                var middleLine = leftLine.GetMiddleLineUsingAngle(rightLine);
                verticalMiddleLines.Add(middleLine);

                var leftVectorFrom1To2 = leftLine.GetVectorFrom2To1();
                var leftAngle = axisXVector.GetAngleTo(leftVectorFrom1To2);

                var rightVectorFrom1To2 = rightLine.GetVectorFrom2To1();
                var rightAngle = axisXVector.GetAngleTo(rightVectorFrom1To2);

                vertialAngles.Add(leftAngle - 90.0);
                vertialAngles.Add(rightAngle - 90.0);
            }

            List<Vector> vectors = new List<Vector>();

            for (int i = 0; i < horizontalMiddleLines.Count; i++)
            {
                var hori = horizontalMiddleLines[i];

                for (int j = 0; j < verticalMiddleLines.Count; j++)
                {
                    var vertial = verticalMiddleLines[j];

                    var p = hori.IntersectionWith(vertial);
                    vectors.Add(new Vector(p.X, p.Y));
                }
            }

            var avgHorizontalAngle = horizontalAngles.Average();
            var avgVerticalAngle = vertialAngles.Average();
            var angle = (avgHorizontalAngle + avgVerticalAngle)/2.0;

            var vectorCenter = vectors.GetCenterVector();

            var coord = new RelativeCoordinate(vectorCenter, angle);
            return coord;
        }
    }
}