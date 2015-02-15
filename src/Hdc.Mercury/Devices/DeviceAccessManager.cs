using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hdc.Linq;
using Hdc.Mercury.Communication;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class DeviceAccessManager : IDeviceAccessManager
    {
        public void ReadDevices(IEnumerable<IDevice> devices)
        {
            if (devices == null)
                return;

            if (!devices.Any())
                return;

            var devices2 = devices.ToList();

            var devicesGroupByChannel = devices2.GroupBy(d => d.Registration.Channel, x => x);

            foreach (var deviceGroup in devicesGroupByChannel)
            {
                IEnumerable<IAccessItemRegistration> regs = deviceGroup.Select(d => d.Registration);
                var channel = deviceGroup.Key;
                channel.Read(regs);
            }
        }

        public Task ReadDevicesAsync(IEnumerable<IDevice> devices)
        {
            var deviceGroupsByChannel = devices.GroupBy(d => d.Registration.Channel, x => x);

            var firstdeviceGroupByChannel = deviceGroupsByChannel.First();

            var identifiers = firstdeviceGroupByChannel.Select(x => x.Registration);

            var channel = firstdeviceGroupByChannel.Key;

            return channel.ReadAsync(identifiers.Select(x => x.ServerAlias));
        }

        public Task ReadDevicesAsync<T>(IEnumerable<IDevice<T>> devices)
        {
            return ReadDevicesAsync(devices.Cast<IDevice>());
        }

        public void WriteDevices(IEnumerable<Tuple<IDevice, dynamic>> deviceValuePairs)
        {
            var valuePairs = deviceValuePairs.ToList();
            if (!valuePairs.Any()) return;
            var deviceGroupsByChannel = valuePairs.GroupBy(x => x.Item1.Registration.Channel, x => x);

            foreach (var deviceGroupByChannel in deviceGroupsByChannel)
            {
                var writeDatas = GetWriteDatas(deviceGroupByChannel);
                var channel = deviceGroupByChannel.Key;
                channel.Write(writeDatas);
            }
        }

        public async Task WriteDevicesAsync(IEnumerable<Tuple<IDevice, dynamic>> deviceValuePairs)
        {
            var valuePairs = deviceValuePairs.ToList();
            if (!valuePairs.Any()) return;

            var groups = valuePairs.GroupBy(x => x.Item1.Registration.Channel, x => x);

            await groups.ForEachAsync(group =>
                                      {
                                          var writeDatas = GetWriteDatas(group);
                                          var channel = group.Key;
                                          return channel.WriteAsync(writeDatas);
                                      });
        }

        public async Task WriteDevicesAsync(IEnumerable<IDevice> devices)
        {
            var valuePairs = devices.ToList();
            if (!valuePairs.Any()) return;

            var groups = valuePairs.GroupBy(x => x.Registration.Channel, x => x);

            await groups.ForEachAsync(group =>
                                      {
                                          var writeDatas = group.GetWriteDatas();
                                          var channel = group.Key;
                                          return channel.WriteAsync(writeDatas);
                                      });
        }

        private IList<WriteData> GetWriteDatas(IEnumerable<Tuple<IDevice, dynamic>> tuples)
        {
            return tuples.Select(x => new WriteData()
                                      {
                                          Registration = x.Item1.Registration,
                                          Value = x.Item2,
                                      })
                .ToList();
        }

//        private IList<WriteData> GetWriteDatas(IEnumerable<IDevice> devices)
//        {
//            return devices.Select(x => new WriteData()
//                                      {
//                                          Registration = x.Registration,
//                                          Value = x.Stage,
//                                      })
//                .ToList();
//        }
    }
}