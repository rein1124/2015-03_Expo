using System.Collections.Generic;
using Hdc.Collections.Generic;
using Hdc.Mvvm.Navigation;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Hdc.Mvvm.Navigation
{
    public class ScreenProvider : IScreenProvider
    {
        private ScreenSchema _screenSchema;
        private IScreen _screen;

//        [Dependency]
//        public IScreenSchemaLoader ScreenSchemaLoader { get; set; }
//
//        [InjectionMethod]
//        public void Init()
//        {
//          
//        }

        public void Initialize(ScreenSchema screenSchema)
        {
            _screenSchema = screenSchema;
            _screen = Create(_screenSchema.TopScreenInfo);
            Screens = _screen.Traverse();
        }

        Screen Create(ScreenInfo screenInfo)
        {
            var mainPage = new Screen(screenInfo.Name)
                               {
                                   IsActive = screenInfo.IsActive,
                                   IsMutual = screenInfo.IsMutual,
                                   IsEnabled = screenInfo.IsEnabled,
                               };

            foreach (var subSi in screenInfo.ScreenInfos)
            {
                var s = Create(subSi);
                mainPage.AddChildScreen(s);
            }

            return mainPage;
        }

        public IList<IScreen> Screens { get; set; }

        public IScreen TopScreen
        {
            get { return _screen; }
        }

        public IScreen FindScreen(string screenName)
        {
            return Screens.Single(x => x.Name == screenName);
        }
    }
}