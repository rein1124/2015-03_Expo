using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Modularity
{
    public class ComponentManager : IComponentManager
    {
        private readonly IComponentRegionUpdater _componentRegionUpdater;

        private static IComponentManager _instance;
        public static IComponentManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = ServiceLocator.Current.GetInstance<IComponentManager>();
                return _instance;
            }
            set { _instance = value; }
        }

        private readonly Subject<bool> _itemActivationChangedEvent = new Subject<bool>();

        public ComponentManager(IComponentRegionUpdater componentRegionUpdater)
        {
            _componentRegionUpdater = componentRegionUpdater;

            ComponentRegions = new ConcurrentDictionary<string, IComponentRegion>();
            ComponentItems = new ConcurrentDictionary<string, IComponentItem>();

            _itemActivationChangedEvent
                .Subscribe(
                    x =>
                        {
                            UpdateRegionStates();
                        });
        }


        public ConcurrentDictionary<string, IComponentRegion> ComponentRegions { get; set; }

        public ConcurrentDictionary<string, IComponentItem> ComponentItems { get; set; }

        public bool TryGetElement(string key, out IComponentRegion value)
        {
            return ComponentRegions.TryGetValue(key, out value);
        }

        public IComponentItem GetComponentItem(string itemName)
        {
            IComponentItem oldRegion;

            var isExist = ComponentItems.TryGetValue(itemName, out oldRegion);

            if (isExist)
                return oldRegion;


            IComponentItem newElement = new ComponentItem(itemName);
            ComponentItems.TryAdd(itemName, newElement);


            newElement.Subscribe(_itemActivationChangedEvent);

            var element = ComponentItems.GetOrAdd(
                itemName,
                x =>
                    {
                        var e = new ComponentItem(itemName);
                        return e;
                    });
            return element;
        }

        public IComponentRegion GetComponentRegion(string regionName)
        {
            IComponentRegion oldRegion;

            var isExist = ComponentRegions.TryGetValue(regionName, out oldRegion);

            if (isExist)
                return oldRegion;

            var newElement = new ComponentRegion(regionName);
            ComponentRegions.TryAdd(regionName, newElement);

            var element = ComponentRegions.GetOrAdd(
                regionName,
                x =>
                    {
                        var e = new ComponentRegion(regionName);
                        return e;
                    });

            UpdateRegionStates();
            return element;
        }

        private void UpdateRegionStates()
        {
            _componentRegionUpdater.UpdateRegionStates(ComponentItems.Values, ComponentRegions.Values);
        }
    }
}