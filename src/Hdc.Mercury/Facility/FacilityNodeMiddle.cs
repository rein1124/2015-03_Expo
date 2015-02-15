using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hdc.Collections.Generic;
using Hdc.Mercury;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public abstract class FacilityNodeMiddle<TThis,
                                             TParent,
                                             TChild> :
                                                 ViewModelContextNodeMiddle<
                                                     TThis,
                                                     IDeviceGroup,
                                                     TParent,
                                                     IDeviceGroup,
                                                     TChild,
                                                     IDeviceGroup>,
                                                 IFacilityNodeMiddle<
                                                     TThis,
                                                     TParent,
                                                     TChild>
        where TThis : class, IFacilityNodeMiddle<TThis, TParent, TChild>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TChild : IFacilityNodeChild<TChild, TThis>
    {
//        private readonly IDictionary<string, IDevice> _sharedDevices = new Dictionary<string, IDevice>();

//        private readonly IDictionary<string, object> _sharedObjects= new Dictionary<string, object>();

//        private int _missedDeviceCount = 0;

        public int DisplayIndex
        {
            get { return Index + 1; }
        }

//        void NotifyMissedDeviceName(string deviceName)
//        {
//            Debug.WriteLine("deviceName is not be found: " + deviceName);
//            _missedDeviceCount++;
//        }

//        public IDevice<T> GetDevice<T>(string deviceName)
//        {
//            var device = Context.Devices.SingleOrDefault(x => x.Name == deviceName);
//            var deviceT = device as IDevice<T>;
//            if (deviceT != null) return deviceT;
//
//            NotifyMissedDeviceName(deviceName);
//
//            IDevice sharedDevice;
//            var isExist = _sharedDevices.TryGetValue(deviceName, out sharedDevice);
//            if (isExist)
//                return sharedDevice as IDevice<T>;
//
//            var newDevice = ServiceLocator.GetInstance<ISimpleDevice<T>>();
//            _sharedDevices.Add(deviceName, newDevice);
//
//            return newDevice;
//        }

//        public IObjectWrapper<T> GetSharedObject<T>(string deviceName)
//        {
//            object obj;
//            var isExist = _sharedObjects.TryGetValue(deviceName, out obj);
//            if (isExist)
//                return obj as IObjectWrapper<T>;
//
//            var newObj = new NotifyPropertyChangedObjectWrapper<T>();
//            _sharedObjects.Add(deviceName, newObj);
//            return newObj;
//        }
    }
}