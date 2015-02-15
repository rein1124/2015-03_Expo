using System;

namespace Hdc.Mercury
{
    public static class ExecutorExtensions
    {
        public static ICommandExecutor GetCommandExecutor(this IDevice<bool> device,
                                                          Func<bool> filter = null,
                                                          Action alternativeAction = null)
        {
            return new CommandExecutor(device, filter, alternativeAction);
        }

        public static IStepExecutor GetStepExecutor(this IDevice<bool> device,
                                                    Func<bool> condition = null,
                                                    Action alternativeAfterBeganAction = null,
                                                    Action alternativeAfterEndedAction = null)
        {
            return new StepExecutor(device, condition, alternativeAfterBeganAction, alternativeAfterEndedAction);
        }

        public static IValueCommitExecutor<T> GetValueCommitExecutor<T>(this IDevice<bool> commitDevice,
                                                                        IDevice<T> stageDevice,
                                                                        Func<bool> filter = null,
                                                                        Action<T> alternativeAction = null)
        {
            return new ValueCommitExecutor<T>(commitDevice, stageDevice, filter, alternativeAction);
        }
    }
}