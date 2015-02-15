using System;
using Hdc.Patterns;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Navigation
{
    public class ScreenEventPublisher
    {
        [Dependency]
        public IEventBus EventBus { get; set; }

        [Dependency]
        public IScreenProvider ScreenProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            foreach (var screen in ScreenProvider.Screens)
            {
                IScreen screen1 = screen;

                screen
                    .OnEnterEvent
                    .Subscribe(
                        (evt) => EventBus.Publish(new ScreenOnEnterEvent()
                                                      {
                                                          Screen = screen1,
                                                          ScreenName = screen1.Name,
                                                      }));

                screen
                    .OnExitEvent
                    .Subscribe(
                        (evt) => EventBus.Publish(new ScreenOnExitEvent()
                                                      {
                                                          Screen = screen1,
                                                          ScreenName = screen1.Name,
                                                      }));
            }
        }
    }
}