using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hdc.Linq;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication
{
    public static class AccessChannelExtensions
    {
        public static IDevice Register(this IAccessChannel channel,
                                       DeviceConfig accessItemConfig)
        {
            return channel.Register(accessItemConfig.ToEnumerable()).First();
        }

        [Obsolete("to rmoved, No need")]
        private static void AddToUpdateList(this IAccessChannel channel,
                                            IEnumerable<IAccessItemRegistration> registrations)
        {
            channel.AddToUpdateList(registrations.Select(x => x.ClientAlias));
        }

        public static void AddToUpdateList(this IAccessChannel channel,
                                           IEnumerable<IDevice> devices)
        {
            channel.AddToUpdateList(devices.Select(d => d.Registration.ClientAlias));
        }

        [Obsolete("to removed, No need")]
        private static Task AddToUpdateListAsync(this IAccessChannel channel,
                                                 IEnumerable<IAccessItemRegistration> registrations)
        {
            return channel.AddToUpdateListAsync(registrations.Select(x => x.ClientAlias));
        }

        public static Task AddToUpdateListAsync(this IAccessChannel channel,
                                                IEnumerable<IDevice> devices)
        {
            return channel.AddToUpdateListAsync(devices.Select(d => d.Registration.ClientAlias));
        }

        public static void Read(this IAccessChannel channel,
                                uint serverAlias)
        {
            channel.Read(serverAlias.ToEnumerable());
        }

        public static void Read(this IAccessChannel channel, IAccessItemRegistration reg)
        {
            channel.Read(reg.ToEnumerable());
        }

        public static void Read(this IAccessChannel channel, IEnumerable<IAccessItemRegistration> regs)
        {
            channel.Read(regs.Select(x => x.ServerAlias));
        }

        public static Task ReadAsync(this IAccessChannel channel, IAccessItemRegistration reg)
        {
            return channel.ReadAsync(reg.ToEnumerable());
        }

        public static Task ReadAsync(this IAccessChannel channel, IEnumerable<IAccessItemRegistration> regs)
        {
            return channel.ReadAsync(regs.Select(x => x.ServerAlias));
        }

        public static IObservable<ReadData> DirectAsyncRead(this IAccessChannel channel,
                                                            IAccessItemRegistration registration)
        {
            return channel.DirectAsyncRead(registration.ToEnumerable()).Select(rds => rds.First());
        }

        public static IObservable<IList<ReadData>> DirectAsyncRead(this IAccessChannel channel,
                                                                   IEnumerable<IAccessItemRegistration> registrations)
        {
            return channel.DirectAsyncRead(registrations.Select(x => x.ServerAlias));
        }

        public static void Write(this IAccessChannel channel, WriteData writeData)
        {
            channel.Write(writeData.ToEnumerable());
        }

        public static Task WriteAsync(this IAccessChannel channel, WriteData writeData)
        {
            return channel.WriteAsync(writeData.ToEnumerable());
        }
    }
}