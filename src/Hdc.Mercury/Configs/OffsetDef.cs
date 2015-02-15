using System.ComponentModel;

namespace Hdc.Mercury.Configs
{
    public class OffsetDef
    {
        public string GroupName { get; set; }

        [DefaultValue(0)]
        public int Prefix { get; set; }

        [DefaultValue(0)]
        public int Suffix { get; set; }

        public OffsetDef()
        {
        }

        public OffsetDef(string groupName, int prefix = 0, int suffix = 0)
        {
            GroupName = groupName;
            Prefix = prefix;
            Suffix = suffix;
        }
    }
}