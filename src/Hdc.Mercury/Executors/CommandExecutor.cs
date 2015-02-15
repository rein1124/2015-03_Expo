using System;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public class CommandExecutor : ICommandExecutor
    {
        public IDevice<bool> CommandDevice { get; set; }
        public Func<bool> Filter { get; private set; }
        public Action AlternativeAction { get; set; }

        public CommandExecutor()
        {
        }

        public CommandExecutor(IDevice<bool> commandDevice,
                               Func<bool> filter = null,
                               Action alternativeAction = null)
        {
            CommandDevice = commandDevice;
            AlternativeAction = alternativeAction;
            Filter = filter;
        }

        public void Commit()
        {
            if (CommandDevice == null)
                throw new NullReferenceException("Device cannot be null");

            if (Filter == null || Filter())
            {
                CommandDevice.WriteTrue();
            }
            else
            {
                if (AlternativeAction != null)
                    AlternativeAction();
            }
        }
    }
}