namespace Hdc
{
    using System;

    public class TimeSpaner<TTarget> : IDisposable
    {
        private TimeSpan _startTimespan;

        private TimeSpan _stopTimeSpan;

        public TTarget Container { get; private set; }

        public TimeSpan During { get; private set; }

        public TimeSpaner() : this(default(TTarget))
        {
        }

        public TimeSpaner(TTarget container) : this(container, false)
        {
        }

        public TimeSpaner(TTarget container, bool isStart)
        {
            Container = container;

            if (isStart)
                Start();
        }

        public void Start()
        {
            _startTimespan = new TimeSpan(DateTime.Now.Ticks);
        }

        public TimeSpan Stop()
        {
            _stopTimeSpan = new TimeSpan(DateTime.Now.Ticks);
            During = _stopTimeSpan - _startTimespan;
            return During;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}