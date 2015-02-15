using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.Domain.Configs
{
    using Microsoft.Practices.Unity;

    public class MachineConfigProvider : IMachineConfigProvider
    {
        [Dependency]
        public IMachineConfigSaveLoadService MachineConfigSaveLoadService { get; set; }

        [InjectionMethod]
        public void Init()
        {
            MachineConfig = MachineConfigSaveLoadService.Output();
        }

        public MachineConfig MachineConfig { get; set; }

        public void Commit()
        {
            MachineConfigSaveLoadService.Input(MachineConfig);
        }
    }
}
