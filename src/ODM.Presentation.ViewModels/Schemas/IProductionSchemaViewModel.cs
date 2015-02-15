using System;
using System.Collections.Generic;

namespace ODM.Presentation.ViewModels.Schemas
{
    public interface IProductionSchemaViewModel
    {
        long Id { get; set; }

        bool IsActive { get; set; }

        int Index { get; set; }

        string Name { get; set; }

        string Comment { get; set; }

        DateTime ModifiedDateTime { get; set; }

        DateTime DownloadDateTime { get; set; }

        IList<IParameterEntryViewModel> ParameterEntries { get; set; }
    }
}