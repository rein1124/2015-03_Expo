using System;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public class ValueCommitExecutor<T> : IValueCommitExecutor<T>
    {
        public IDevice<T> StageDevice { get; set; }
        public IDevice<bool> CommandDevice { get; set; }
        public Func<bool> Filter { get; private set; }
        public Action<T> AlternativeAction { get; set; }

        public ValueCommitExecutor()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandDevice"></param>
        /// <param name="stageDevice"></param>
        /// <param name="filter">Do action when filter = true, or do alternative action.</param>
        /// <param name="alternativeAction"></param>
        public ValueCommitExecutor(IDevice<bool> commandDevice,
                                   IDevice<T> stageDevice,
                                   Func<bool> filter = null,
                                   Action<T> alternativeAction = null)
        {
            StageDevice = stageDevice;
            CommandDevice = commandDevice;
            AlternativeAction = alternativeAction;
            Filter = filter;
        }

        public void Commit(T value)
        {
            if (StageDevice == null || CommandDevice == null)
                throw new NullReferenceException("Device cannot be null");

            if (Filter == null || Filter())
            {
                StageDevice.Write(value);
                CommandDevice.WriteTrue();
            }
            else
            {
                if (AlternativeAction != null)
                    AlternativeAction(value);
            }
        }
    }
}