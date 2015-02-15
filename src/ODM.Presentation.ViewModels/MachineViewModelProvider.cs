using Microsoft.Practices.Unity;
using ODM.Domain;

namespace ODM.Presentation.ViewModels
{
    class MachineViewModelProvider : IMachineViewModelProvider
    {
        [Dependency]
        public IMachineViewModel Machine { get; set; }

        [Dependency]
        public IMachineProvider MachineProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Machine.Initialize(MachineProvider.Machine);
            Machine.BindingTo(MachineProvider.Machine);
        }
    }
}