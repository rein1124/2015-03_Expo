namespace Hdc.Mvvm.Navigation
{
    public static class ScreenProviderExtensions
    {
        public static void Activate(this IScreenProvider screenProvider, string screenName)
        {
            screenProvider.FindScreen(screenName).Activate();
        }
    }
}