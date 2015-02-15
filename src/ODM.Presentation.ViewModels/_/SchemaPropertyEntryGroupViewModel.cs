/*using Hdc.Collections.ObjectModel;

namespace ODM.Presentation.ViewModels.Schemas
{
    public class SchemaPropertyEntryGroupViewModel
    {
        public BindableCollection<ParameterEntryViewModel> Entries { get; set; }

        public string Name { get; set; }

        public SchemaPropertyEntryGroupViewModel()
        {
            Entries = new BindableCollection<ParameterEntryViewModel>();
        }

        public SchemaPropertyEntryGroupViewModel Add(SchemaPropertyName schemaPropertyName, SchemaPropertyDataType schemaPropertyDataType)
        {
            Entries.Add(new ParameterEntryViewModel()
                            {
                                Name = schemaPropertyName,
                                DataType = schemaPropertyDataType,
                            });
            return this;
        }
    }
}*/