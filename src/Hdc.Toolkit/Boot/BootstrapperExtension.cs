using Microsoft.Practices.Unity;

namespace Hdc.Boot
{
    public interface IBootstrapperExtension
    {
        void Initialize(IUnityContainer container);
    }

    public class BootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IUnityContainer container)
        {
            throw new System.NotImplementedException();
        }
    }
}