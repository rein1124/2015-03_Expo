namespace Hdc.Windows.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public abstract class SuspendableThread
    {
        #region Data

        private ManualResetEvent suspendChangedEvent = new ManualResetEvent(false);

        private ManualResetEvent terminateEvent = new ManualResetEvent(false);

        private long suspended;

        private Thread thread;

        private readonly string _threadName;

        private System.Threading.ThreadState failsafeThreadState = System.Threading.ThreadState.Unstarted;

        #endregion Data

        protected SuspendableThread() : this("SuspendableThread")
        {
        }

        protected SuspendableThread(string threadName)
        {
            _threadName = threadName;
        }

        private void ThreadEntry()
        {
            failsafeThreadState = System.Threading.ThreadState.Stopped;

            OnDoWork();
        }

        protected abstract void OnDoWork();

        #region Protected methods

        protected Boolean SuspendIfNeeded()
        {
            Boolean suspendEventChanged = suspendChangedEvent.WaitOne(0, true);

            if (suspendEventChanged)
            {
                Boolean needToSuspend = Interlocked.Read(ref suspended) != 0;

                suspendChangedEvent.Reset();

                if (needToSuspend)
                {
                    /// Suspending...

                    if (1 == WaitHandle.WaitAny(new WaitHandle[] {suspendChangedEvent, terminateEvent}))
                        return true;

                    /// ...Waking
                }
            }

            return false;
        }

        protected bool HasTerminateRequest()
        {
            return terminateEvent.WaitOne(0, true);
        }

        #endregion Protected methods

        public void Start()
        {
            OnStarting();
            thread = new Thread(new ThreadStart(ThreadEntry));
            thread.Name = _threadName;

            // make sure this thread won't be automaticaly

            // terminated by the runtime when the

            // application exits

            thread.IsBackground = false;

            thread.Start();

            OnStarted();
        }

        protected virtual void OnStarting()
        {
        }

        protected virtual void OnStarted()
        {
        }

        public void Join()
        {
            if (thread != null)
                thread.Join();
        }

        public Boolean Join(Int32 milliseconds)
        {
            if (thread != null)
                return thread.Join(milliseconds);

            return true;
        }

        public Boolean IsSuspended
        {
            get { return suspended == 1; }
        }

        /// <remarks>Not supported in .NET Compact Framework</remarks>
        public Boolean Join(TimeSpan timeSpan)
        {
            if (thread != null)
                return thread.Join(timeSpan);

            return true;
        }

        public void Terminate()
        {
            OnStopping();

            terminateEvent.Set();
        }

        public void TerminateAndWait()
        {
            OnStopping();

            terminateEvent.Set();

            thread.Join();

            OnStopped();
        }

        protected virtual void OnStopping()
        {
        }

        protected virtual void OnStopped()
        {
        }

        public void Suspend()
        {
            OnSuspending();
            while (1 != Interlocked.Exchange(ref suspended, 1))
            {
            }

            suspendChangedEvent.Set();
            OnSuspended();
        }

        protected virtual void OnSuspending()
        {
        }

        protected virtual void OnSuspended()
        {
        }

        public void Resume()
        {
            OnResumeing();
            while (0 != Interlocked.Exchange(ref suspended, 0))
            {
            }

            suspendChangedEvent.Set();
            OnResumed();
        }

        protected virtual void OnResumeing()
        {
        }

        protected virtual void OnResumed()
        {
        }

        public System.Threading.ThreadState ThreadState
        {
            get
            {
                if (null != thread)
                    return thread.ThreadState;

                return failsafeThreadState;
            }
        }

        //        public Boolean IsSuspended { get { return suspended==0; } }
    }
}