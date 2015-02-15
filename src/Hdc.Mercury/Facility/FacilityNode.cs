using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class FacilityNode<TParent> : IFacilityNode<TParent>
    {
        private IDeviceGroup _context;

//        private readonly IDictionary<string, IDevice> _sharedDevices = new Dictionary<string, IDevice>();

//        private readonly IDictionary<string, object> _sharedObjects = new Dictionary<string, object>();

//        public static int MissedDeviceCount;

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public int Index { get; set; }

        public TParent Parent { get; set; }

        public IDeviceGroup Context
        {
            get { return _context; }
        }

//        private void NotifyMissedDeviceName(string deviceName)
//        {
//            Debug.WriteLine("deviceName is not be found: " + deviceName);
//            MissedDeviceCount++;
//        }
//
//        public IDevice<T> GetDevice<T>(string deviceName)
//        {
//            var device = Context.Devices.SingleOrDefault(x => x.Name == deviceName);
//            var deviceT = device as IDevice<T>;
//            if (deviceT != null) return deviceT;
//
//            NotifyMissedDeviceName(deviceName);
//            MissedDeviceCount++;
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

        public void Initialize(IDeviceGroup context)
        {
            OnInitializing(context);

            _context = context;

            OnInitialized(context);
        }

        protected virtual void OnInitialized(IDeviceGroup context)
        {
        }

        protected virtual void OnInitializing(IDeviceGroup context)
        {
        }

        public void Reset()
        {
            OnResetting(Context);

            var context = _context;
            _context = null;

            OnReset(context);
        }

        protected virtual void OnResetting(IDeviceGroup context)
        {
        }

        protected virtual void OnReset(IDeviceGroup context)
        {
        }


        public void BindingTo(IDeviceGroup context)
        {
            _context = context;

            OnBindingTo(context);
        }

        protected virtual void OnBindingTo(IDeviceGroup context)
        {
        }


        protected IList<TChild> CreateChildren<TChild>(
            Func<IDeviceGroup, IEnumerable<IDeviceGroup>> getChildDeviceGroups)
            where TChild : IFacilityNode<IFacilityNode<TParent>>
        {
            var childContexts = getChildDeviceGroups(Context);

            int counter = 0;

            var children = new List<TChild>();

            foreach (var childContext in childContexts)
            {
                var child = ServiceLocator.GetInstance<TChild>();

                children.Add(child);
                child.Parent = this;
                child.Index = counter;
                child.Initialize(childContext);

                counter++;
            }

            return children;
        }

        protected IList<TChild> CreateChildren<TChild>(
            params IDeviceGroup[] deviceGroups)
             where TChild : IFacilityNode<IFacilityNode<TParent>>
        {
            throw new NotImplementedException();
        }
    }
}