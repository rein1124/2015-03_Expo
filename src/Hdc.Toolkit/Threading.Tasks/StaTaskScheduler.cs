using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hdc.Windows.Threading.Tasks
{
    public class StaTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly List<Thread> _threads;

        private BlockingCollection<Task> _tasks;

        public StaTaskScheduler(int numberOfThreads)
        {
            if (numberOfThreads < 1)

                throw new ArgumentOutOfRangeException("concurrencyLevel");


            _tasks = new BlockingCollection<Task>();


            _threads = Enumerable.Range(0, numberOfThreads)
                .Select(i =>
                            {
                                var thread = new Thread(() =>
                                                            {
                                                                foreach (var t in

                                                                    _tasks.GetConsumingEnumerable())
                                                                {
                                                                    TryExecuteTask(t);
                                                                }
                                                            });

                                thread.IsBackground = true;

                                thread.SetApartmentState(ApartmentState.STA);

                                return thread;
                            }).ToList();


            _threads.ForEach(t => t.Start());
        }

        protected override void QueueTask(Task task)
        {
            _tasks.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return
                Thread.CurrentThread.GetApartmentState() ==
                ApartmentState.STA &&
                TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks.ToArray();
        }

        public void Dispose()
        {
            if (_tasks != null)
            {
                _tasks.CompleteAdding();


                foreach (var thread in _threads) thread.Join();


                _tasks.Dispose();

                _tasks = null;
            }
        }
    }
}