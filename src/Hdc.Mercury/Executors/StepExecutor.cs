using System;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public class StepExecutor : IStepExecutor
    {
        public IDevice<bool> Device { get; set; }
//        public Action AfterBeganAction { get; set; }
        public Action AlternativeAfterBeganAction { get; set; }
//        public Action AfterEndedAction { get; set; }
        public Action AlternativeAfterEndedAction { get; set; }
        public Func<bool> Filter { get; private set; }

        public StepExecutor()
        {
        }

        public StepExecutor(IDevice<bool> device,
//                            Action afterBeganAction = null,
//                            Action afterEndedAction = null,
                            Func<bool> filter = null,
                            Action alternativeAfterBeganAction = null,
                            Action alternativeAfterEndedAction = null)
        {
            Device = device;
//            AfterBeganAction = afterBeganAction;
//            AfterEndedAction = afterEndedAction;
            AlternativeAfterBeganAction = alternativeAfterBeganAction;
            AlternativeAfterEndedAction = alternativeAfterEndedAction;
            Filter = filter;
        }

        public void Begin()
        {
            if (Device == null)
                throw new NullReferenceException("Device cannot be null");

            if (Filter == null || Filter())
            {
                Device.WriteTrue();
            }
            else
            {
                if (AlternativeAfterBeganAction != null)
                    AlternativeAfterBeganAction();
            }

//            if (AfterBeganAction != null)
//                AfterBeganAction();
        }

        public void End()
        {
            if (Device == null)
                throw new NullReferenceException("Device cannot be null");

            if (Filter == null || Filter())
            {
                Device.WriteFalse();
            }
            else
            {
                if (AlternativeAfterEndedAction != null)
                    AlternativeAfterEndedAction();
            }

//            if (AfterEndedAction != null)
//                AfterEndedAction();
        }
    }
}