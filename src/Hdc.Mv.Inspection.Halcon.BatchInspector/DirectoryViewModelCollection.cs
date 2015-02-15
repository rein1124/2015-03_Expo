using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hdc.Mv.Inspection.Halcon.BatchInspector.Annotations;

namespace Hdc.Mv.Inspection.Halcon.BatchInspector
{
    public class DirectoryViewModelCollection : Collection<DirectoryViewModel>
    {
        public DirectoryViewModelCollection()
        {
        }

        public DirectoryViewModelCollection([NotNull] IList<DirectoryViewModel> list) : base(list)
        {
        }
    }
}