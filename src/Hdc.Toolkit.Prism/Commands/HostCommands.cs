using Microsoft.Practices.Prism.Commands;

namespace Hdc.Prism.Commands
{
    public static class HostCommands
    {
        private static readonly CompositeCommand Shutdown = new CompositeCommand();

        public static CompositeCommand ShutdownCommand
        {
            get { return Shutdown; }
        }
    }
}
