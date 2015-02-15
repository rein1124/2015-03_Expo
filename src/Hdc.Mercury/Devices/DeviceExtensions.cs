using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Hdc.Mercury.Communication;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public static class DeviceExtensions
    {
        public static IDevice<bool> WriteInverse(this IDevice<bool> device)
        {
            var oldValue = device.Value;
            device.Write(oldValue);
            return device;
        }

        public static IDevice<bool> WriteTrue(this IDevice<bool> device)
        {
            device.Write(true);
            return device;
        }

        public static Task WriteTrueAsync(this IDevice<bool> device)
        {
            return Task.Run(() => device.Write(true));
        }

        public static IDevice<bool> WriteFalse(this IDevice<bool> device)
        {
            device.Write(false);
            return device;
        }

        public static Task WriteFalseAsync(this IDevice<bool> device)
        {
            return Task.Run(() => device.Write(false));
        }

        [Obsolete]
        public static void Pulse(this IDevice<bool> device, int delay = 1000)
        {
            device.WriteTrue();
            var t = new Task(() =>
                             {
                                 Thread.Sleep(delay);
                                 device.WriteFalse();
                             });
            t.Start();
        }

        public static async Task PulseAsync(this IDevice<bool> device, int delay = 1000)
        {
            await device.WriteAsync(true);
            Thread.Sleep(delay);
            await device.WriteAsync(false);
        }

        public static IObservable<Unit> AsyncWriteInverse(this IDevice<bool> device)
        {
            var oldValue = device.Value;
            return device.AsyncWrite(oldValue);
        }

        public static IObservable<Unit> AsyncWriteTrue(this IDevice<bool> device)
        {
            return device.AsyncWrite(true);
        }

        public static IObservable<Unit> AsyncWriteFalse(this IDevice<bool> device)
        {
            return device.AsyncWrite(false);
        }

        public static IObservable<Unit> AsyncPulse(this IDevice<bool> device)
        {
            //TODO rein
            var ob = new Subject<Unit>();
            device
                .AsyncWriteTrue()
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(x => device
                    .AsyncWriteFalse()
                    .Subscribe(y => ob.OnNext(new Unit())));
            return ob.Take(1);
        }

        public static IObservable<Unit> AsyncWrite(this IDevice<bool> device)
        {
            return device.AsyncWrite(device.Value);
        }

        public static IObservable<Unit> AsyncWrite<T>(this IDevice<T> device)
        {
            return device.AsyncWrite(device.Value);
        }

        public static void WriteUsingAsyncWrite<T>(this IDevice<T> device, T value)
        {
            var wait = new AutoResetEvent(false);
            device
                .AsyncWrite(value)
                .Subscribe((x) => wait.Set());
            wait.WaitOne();
            wait.Dispose();
        }

        public static void Sync<T>(this IDevice<T> ob, IValueMonitor<T> valueMonitor)
        {
            valueMonitor.Observe(ob.Select(arg => arg), ob.Value, x => { ob.Value = x; });
        }


        public static List<WriteData> GetWriteDatas(this IEnumerable<IDevice> devices)
        {
            return devices.Select(x => new WriteData()
                                       {
                                           Registration = x.Registration,
                                           Value = x.Stage,
                                       })
                .ToList();
        }
    }
}