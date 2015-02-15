using Hdc.Patterns;

namespace Hdc.Mvvm.Navigation
{
    public class ScreenEnteredEvent:IEvent
    {
        public string ScreenName { get; set; }

        public IScreen Screen { get; set; }
    }
}