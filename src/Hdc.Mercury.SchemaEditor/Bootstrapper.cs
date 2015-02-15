namespace Hdc.Mercury.SchemaEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.UnityExtensions;
    using Microsoft.Practices.Unity;

    internal class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ModuleCatalog();
            moduleCatalog.AddModule(typeof (ExecuteModule));
            return moduleCatalog;
        }

        protected override DependencyObject CreateShell()
        {
            var view = Container.Resolve<MainWindow>();
            view.Show();
            return view as DependencyObject;
        }
    }
}
