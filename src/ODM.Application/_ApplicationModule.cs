namespace ODM.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class ApplicationModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
        }
    }
}