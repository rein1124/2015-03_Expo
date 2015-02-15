namespace Hdc.Mv.Inspection
{
    public class CircleSearchingResult
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public bool HasError { get; set; }

        public bool IsNotFound { get; set; }

        public Circle Circle { get; set; }

        public Circle RelativeCircle { get; set; }

        public double Roundness { get; set; }

        public CircleSearchingDefinition Definition { get; set; }

        public CircleSearchingResult()
        {
        }

        public CircleSearchingResult(int index)
        {
            Index = index;
        }

        public CircleSearchingResult(int index, CircleSearchingDefinition definition)
        {
            Index = index;
            Definition = definition;
        }

        public CircleSearchingResult(int index, Circle circle)
        {
            Index = index;
            Circle = circle;
        }

        public CircleSearchingResult(int index, CircleSearchingDefinition definition, Circle circle)
        {
            Index = index;
            Circle = circle;
            Definition = definition;
        }

        public CircleSearchingResult(string name)
        {
            Name = name;
        }
    }
}