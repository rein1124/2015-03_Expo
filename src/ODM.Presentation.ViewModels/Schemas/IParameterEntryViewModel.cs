using Hdc.Mercury;
using Hdc.Reactive;
using ODM.Domain.Schemas;

namespace ODM.Presentation.ViewModels.Schemas
{
    public interface IParameterEntryViewModel
    {
        long Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int Value { get; set; }

        string GroupName { get; set; }

        string GroupDescription { get; set; }

        string CatalogName { get; set; }

        string CatalogDescription { get; set; }

//        IValueMonitor<int> Monitor { get; }

        int PlcValue { get; }

        bool IsPlcValueEnabled { get; }

        void InitMonitor(IDevice<int> device);

        bool IsPlcDevice { get; set; }
    }
}