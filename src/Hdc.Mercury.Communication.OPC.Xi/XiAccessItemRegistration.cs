using Advosol.Paxi;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication
{
    public class XiAccessItemRegistration : AccessItemRegistration
    {
//        public XiAccessItemRegistration(AccessItemConfig config) : base(config)
//        {
//         
//        }

        public ListInstanceDef ListInstanceDef { get; private set; }

        public XiDataList DataList { get; set; }

        public bool IsAddedToDataList
        {
            get { return DataList != null; }
        }

        public XiAccessItemRegistration(DeviceConfig config, IAccessChannel channel)
            : base(config, channel)
        {
            ListInstanceDef = new ListInstanceDef(Config.Tag, ClientAlias, true);
        }
    }
}