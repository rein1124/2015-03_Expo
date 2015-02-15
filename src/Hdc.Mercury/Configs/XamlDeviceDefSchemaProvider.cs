/*using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs.Batch
{
    public class XamlDeviceDefSchemaProvider : IDeviceDefSchemaProvider
    {
        private const string FileName = "DeviceDefStore.xml";

        private DeviceDefSchema _schema;

        public DeviceDefSchema Schema
        {
            get { return _schema; }
        }

        [Dependency]
        public IDeviceDefSchemaRepository DeviceDefSchemaRepository { get; set; }

        public void Update()
        {
            _schema = DeviceDefSchemaRepository.Load(FileName);
        }
    }
}*/