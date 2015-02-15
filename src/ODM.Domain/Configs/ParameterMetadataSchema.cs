using System.Collections.ObjectModel;
using System.Windows.Markup;
using Hdc;
using Hdc.IO;
using Hdc.Reflection;
using Hdc.Serialization;
using PPH;

namespace ODM.Domain.Configs
{
    [ContentProperty("ParameterMetadatas")]
    public class ParameterMetadataSchema
    {
        private static ParameterMetadataSchema _loadedSchema;

        public ParameterMetadataSchema()
        {
            ParameterMetadatas = new Collection<ParameterMetadata>();
        }

        public Collection<ParameterMetadata> ParameterMetadatas { get; set; }

        private static string GetParameterMetadataSchemaFileName()
        {
            var filePath = typeof (ParameterMetadataSchema)
                .Assembly
                .GetAssemblyDirectoryPath()
                .Combine(FileNames.ParameterMetadataSchema + ".xaml");

            return filePath;
        }

        public static ParameterMetadataSchema CreateDefaultSchema()
        {
            if (_loadedSchema != null)
            {
                return _loadedSchema;
            }

            var fileName = GetParameterMetadataSchemaFileName();

            if (fileName.IsFileNotExist())
            {
                return null;
            }
            _loadedSchema = fileName.DeserializeFromXamlFile<ParameterMetadataSchema>();
            return _loadedSchema;
        }
    }
}