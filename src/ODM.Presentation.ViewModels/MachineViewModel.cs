using System;
using System.Linq;
using Hdc;
using Hdc.Collections.Generic;
using Hdc.Mercury;
using Hdc.Reactive;
using Hdc.Reflection;
using Microsoft.Practices.Unity;
using ODM.Domain;

namespace ODM.Presentation.ViewModels
{
    // ReSharper disable InconsistentNaming
    public class MachineViewModel : ViewModelContextNodeMiddleTerminal<
        IMachineViewModel,
        IMachine>,
        IMachineViewModel
    {
        [ValueMonitor]
        public IValueMonitor<int> ProductionSpeedMonitor { get; private set; }

        [ValueMonitor]
        public IValueMonitor<int> TotalCountMonitor { get; private set; }

        [ValueMonitor]
        public IValueMonitor<int> TotalRejectCountMonitor { get; private set; }

        [ValueMonitor]
        public IValueMonitor<int> JobCountMonitor { get; private set; }

        [ValueMonitor]
        public IValueMonitor<int> JobRejectCountMonitor { get; private set; }


        // ReSharper restore InconsistentNaming
        protected override void OnInitialized(IMachine context)
        {
            base.OnInitialized(context);

            this
                .RaisePropertyChangedOnUsingDispatcher(
                    TotalCountMonitor,
                    () => TotalAcceptedCount,
                    () => TotalRejectRate)
                .RaisePropertyChangedOnUsingDispatcher(
                    TotalRejectCountMonitor,
                    () => TotalAcceptedCount,
                    () => TotalRejectRate)
                .RaisePropertyChangedOnUsingDispatcher(
                    JobCountMonitor,
                    () => JobAcceptedCount,
                    () => JobRejectRate)
                .RaisePropertyChangedOnUsingDispatcher(
                    JobRejectCountMonitor,
                    () => JobAcceptedCount,
                    () => JobRejectRate);
        }

        protected override void OnBindingTo(IMachine context)
        {
            base.OnBindingTo(context);


/*            var apis = this.GetAttributePropertyInfos<ValueMonitorAttribute>(false);
            foreach (var api in apis)
            {
                var value = this.GetPropertyValue(api.PropertyInfo.Name) as IDeviceValueMonitor;
                var deviceName = api.PropertyInfo.Name.Replace("Monitor", "");

                var deviceProperty = context.GetPropertyValue(deviceName + "Device") as IDevice;
                if (deviceProperty != null)
                    value.Sync(deviceProperty);
            }*/

            // ProductionInfo
            ProductionSpeedMonitor.Sync(context.Production_ProductionSpeedDevice);
            TotalCountMonitor.Sync(context.Production_TotalCountDevice);
            TotalRejectCountMonitor.Sync(context.Production_TotalRejectCountDevice);
            JobCountMonitor.Sync(context.Production_JobCountDevice);
            JobRejectCountMonitor.Sync(context.Production_JobRejectCountDevice);
        }

        public int ProductionSpeed
        {
            get { return ProductionSpeedMonitor.Value; }
        }

        public int TotalCount
        {
            get { return TotalCountMonitor.Value; }
        }

        public int TotalRejectCount
        {
            get { return TotalRejectCountMonitor.Value; }
        }

        public int TotalAcceptedCount
        {
            get { return TotalCount - TotalRejectCount; }
        }

        public int TotalRejectRate
        {
            get
            {
                if (TotalCount == 0)
                    return 0;
                return (int)((decimal)TotalRejectCount / TotalCount * 100);
            }
        }

        public int JobCount
        {
            get { return JobCountMonitor.Value; }
        }

        public int JobRejectCount
        {
            get { return JobRejectCountMonitor.Value; }
        }

        public int JobAcceptedCount
        {
            get { return JobCount - JobRejectCount; }
        }

        public int JobRejectRate
        {
            get
            {
                if (JobCount == 0)
                    return 0;
                return (int)((decimal)JobRejectCount / JobCount * 100);
            }
        }
    }
}