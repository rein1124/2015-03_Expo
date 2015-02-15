namespace Hdc.Mvvm
{
    public static class WindowControllerExtensions
    {
        public static bool ShowAndActivate(this IWindowController windowController)
        {
            windowController.Show();
            return windowController.Activate();
        }
    }
}