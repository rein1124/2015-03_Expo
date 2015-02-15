using System;

namespace Hdc.Mercury
{
    public interface IStepExecutor
    {
        void Begin();

        //void Begin(Action afterBegan);

        void End();

        //void End(Action afterEnded);
    }
}