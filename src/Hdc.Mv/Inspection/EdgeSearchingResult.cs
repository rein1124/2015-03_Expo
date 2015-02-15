using System;
using System.Windows;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EdgeSearchingResult
    {
        public int Index { get; set; }

        public string Name
        {
            get { return Definition.Name; }
            set { Definition.Name = value; }
        }

        public bool HasError { get; set; }

        public bool IsNotFound { get; set; }

        public Line EdgeLine { get; set; }

        public Point IntersectionPoint { get; set; }

        public EdgeSearchingDefinition Definition { get; set; }

        public EdgeSearchingResult()
        {
        }

        public EdgeSearchingResult(int index)
        {
            Index = index;
        }

        public EdgeSearchingResult(int index, EdgeSearchingDefinition definition)
        {
            Index = index;
            Definition = definition;
        }

        public EdgeSearchingResult(string name)
        {
            Name = name;
        }
    }
}