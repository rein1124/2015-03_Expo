using System.Collections.Generic;

namespace Hdc.Mvvm.Navigation
{
    public interface IScreenProvider
    {
        IList<IScreen> Screens { get; }

        IScreen TopScreen { get; }

        IScreen FindScreen(string screenName);
    }
}