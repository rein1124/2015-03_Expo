using System.IO;
using Hdc.Collections.Generic;
using Hdc.Reflection;
using Hdc.Serialization;

namespace Hdc.Mv.Inspection
{
    public static class InspectionSchemaExtensions
    {
        public static InspectionSchema LoadFromAssemblyDir(this string shortFileName)
        {
            var dir = typeof(InspectionSchemaExtensions).Assembly.GetAssemblyDirectoryPath();
            var fileName = Path.Combine(dir, shortFileName);
            if (!File.Exists(fileName))
            {
                var ds = InspectionSchemaFactory.CreateDefaultSchema();
                ds.SerializeToXamlFile(fileName);
            }
            var schema = fileName.DeserializeFromXamlFile<InspectionSchema>();
            return schema;
        }

        public static InspectionSchema LoadFromFile(this string fileName)
        {
            if (!File.Exists(fileName))
            {
                var ds = InspectionSchemaFactory.CreateDefaultSchema();
                ds.SerializeToXamlFile(fileName);
            }
            var schema = fileName.DeserializeFromXamlFile<InspectionSchema>();
            return schema;
        }

        public static void Merge(this InspectionSchema masterInspectionSchema, InspectionSchema slaveInspectionSchema)
        {
            masterInspectionSchema.CoordinateCircles.AddRange(slaveInspectionSchema.CoordinateCircles);
            masterInspectionSchema.CoordinateEdges.AddRange(slaveInspectionSchema.CoordinateEdges);
            masterInspectionSchema.EdgeSearchingDefinitions.AddRange(slaveInspectionSchema.EdgeSearchingDefinitions);
            masterInspectionSchema.PartSearchingDefinitions.AddRange(slaveInspectionSchema.PartSearchingDefinitions);
            masterInspectionSchema.CircleSearchingDefinitions.AddRange(slaveInspectionSchema.CircleSearchingDefinitions);
            masterInspectionSchema.DistanceBetweenLinesDefinitions.AddRange(slaveInspectionSchema.DistanceBetweenLinesDefinitions);
            masterInspectionSchema.DistanceBetweenIntersectionPointsDefinitions.AddRange(slaveInspectionSchema.DistanceBetweenIntersectionPointsDefinitions);
            masterInspectionSchema.SurfaceDefinitions.AddRange(slaveInspectionSchema.SurfaceDefinitions);
            masterInspectionSchema.DefectDefinitions.AddRange(slaveInspectionSchema.DefectDefinitions);
            masterInspectionSchema.RegionTargetDefinitions.AddRange(slaveInspectionSchema.RegionTargetDefinitions);
        }
    }
}