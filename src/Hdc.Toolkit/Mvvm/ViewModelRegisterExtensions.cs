using Hdc.Unity;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm
{
    public static class ViewModelRegisterExtensions
    {
        public static IUnityContainer RegisterViewModelFromContainer<TViewModel>(this IUnityContainer container,
                                                                                 string viewModelName,
                                                                                 string key)
        {
            var vmr = container.Resolve<IViewModelRegister>();
            vmr.Register<TViewModel>(viewModelName, key);
            return container;
        }

        public static IUnityContainer RegisterViewModelFromContainer<TViewModel>(this IUnityContainer container,
                                                                                 string viewModelName)
        {
            var vmr = container.Resolve<IViewModelRegister>();
            vmr.Register<TViewModel>(viewModelName);
            return container;
        }

        public static IUnityContainer RegisterViewModelWithLifetimeManager<TViewModel>(this IUnityContainer container,
                                                                                       string viewModelName)
        {
            container.RegisterTypeWithLifetimeManager<TViewModel>();

            if (typeof (TViewModel).IsImplementationOf<IViewModel>())
            {
                container.RegisterType(typeof (IViewModel), typeof (TViewModel), viewModelName,
                                       new ContainerControlledLifetimeManager());
            }

            return container.RegisterViewModelFromContainer<TViewModel>(viewModelName);
        }

        // TBD, 2013-01-09, rein
        public static IUnityContainer RegisterViewModelWithLifetimeManager<TViewModelInterface, TViewModelImplement>(
            this IUnityContainer container,
            string viewModelName) where TViewModelImplement : TViewModelInterface
        {
            container.RegisterTypeWithLifetimeManager<TViewModelInterface, TViewModelImplement>();

            if (typeof(TViewModelImplement).IsImplementationOf<IViewModel>())
            {
                container.RegisterType(typeof(IViewModel), typeof(TViewModelImplement), viewModelName,
                                       new ContainerControlledLifetimeManager());
            }

            return container.RegisterViewModelFromContainer<TViewModelInterface>(viewModelName);
        }
    }
}