using System;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SurfaceDefinition
    {
        public SurfaceDefinition()
        {
            IncludeParts = new Collection<SurfacePartDefinition>();

            ExcludeParts = new Collection<SurfacePartDefinition>();
        }

        public string Name { get; set; }

        public string GroupName { get; set; }

        public bool SaveCacheImageEnabled { get; set; }

        public bool SaveAllCacheImageEnabled { get; set; }

        public Collection<SurfacePartDefinition> ExcludeParts { get; set; }

        public Collection<SurfacePartDefinition> IncludeParts { get; set; }
    }
}