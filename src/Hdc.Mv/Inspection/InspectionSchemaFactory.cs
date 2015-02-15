using System.Windows;

namespace Hdc.Mv.Inspection
{
    public static class InspectionSchemaFactory
    {
        public static InspectionSchema CreateDefaultSchema()
        {
            var defaultSchema = new InspectionSchema();
            defaultSchema.CircleSearchingDefinitions.Add(new CircleSearchingDefinition());
            defaultSchema.EdgeSearchingDefinitions.Add(new EdgeSearchingDefinition(){RelativeLine = new Line(1,2,3,4)});
//            defaultSchema.CropRect = new Int32Rect(11, 22, 33, 44);
//            defaultSchema.CircleSearchingEnable = true;
//            defaultSchema.EdgeSearchingEnable = true;
            

            return defaultSchema;
        }
    }
}