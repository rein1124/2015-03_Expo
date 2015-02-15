using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hdc.Collections.Generic;
using Hdc.Reactive;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class DeviceGroup : Node<IDeviceGroup>, IDeviceGroup
    {
        private IList<IDevice> _devices = new List<IDevice>();

        private string _name;

        private IDeviceGroup _parent;

        private readonly IDictionary<string, IDevice> _sharedDevices = new ConcurrentDictionary<string, IDevice>();

        private readonly IDictionary<string, object> _sharedObjects = new ConcurrentDictionary<string, object>();

        private readonly IDictionary<string, object> _sharedSubjects = new ConcurrentDictionary<string, object>();


        [InjectionConstructor]
        public DeviceGroup()
        {
        }

        public DeviceGroup(string name, IEnumerable<IDevice> devices, IEnumerable<IDeviceGroup> deviceGroups)
        {
            _name = name;
            foreach (var deviceGroup in deviceGroups)
            {
                DeviceGroups.Add(deviceGroup);
            }

            foreach (var device in devices)
            {
                Devices.Add(device);
            }
        }

        public DeviceGroup(IEnumerable<IDevice> devices, IEnumerable<IDeviceGroup> deviceGroups)
            : this(null, devices, deviceGroups)
        {
        }

        public IDeviceGroup Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public IList<IDeviceGroup> DeviceGroups
        {
            get { return Nodes; }
            set { Nodes = value; }
        }

        public IList<IDevice> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public  static int MissedDeviceCount;

        void NotifyMissedDeviceName(string deviceName)
        {
            Debug.WriteLine("deviceName is not be found: " + deviceName);
            MissedDeviceCount++;
        }

        public IDevice<T> GetDevice<T>(string deviceName)
        {
            IDevice device = Devices.SingleOrDefault(x => x != null && x.Name == deviceName);
            var deviceT = device as IDevice<T>;
            if (deviceT != null)
            {
                return deviceT;
            }

            NotifyMissedDeviceName(deviceName);
            MissedDeviceCount++;

            IDevice sharedDevice;

            var isExist = _sharedDevices.TryGetValue(deviceName, out sharedDevice);
            if (isExist)
                return sharedDevice as IDevice<T>;

//            var newDevice = ServiceLocator.Current.GetInstance<ISimpleDevice<T>>();
            var newDevice = new SimpleDevice<T>();
            _sharedDevices.Add(deviceName, newDevice);

            return newDevice;
        }

        public IRetainedSubject<T> GetSharedSubject<T>(string deviceName)
        {
            object obj;
            var isExist = _sharedSubjects.TryGetValue(deviceName, out obj);
            if (isExist)
                return obj as IRetainedSubject<T>;

            var newObj = new RetainedSubject<T>();
            _sharedSubjects.Add(deviceName, newObj);
            return newObj;
        }
    }
}