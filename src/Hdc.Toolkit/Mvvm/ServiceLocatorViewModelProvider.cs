using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mvvm
{
    public class ServiceLocatorViewModelProvider : IViewModelRegister, IViewModelProvider
    {
        private readonly IServiceLocator _serviceLocator;

        private readonly IDictionary<string, ViewModelMetadata> _types =
            new ConcurrentDictionary<string, ViewModelMetadata>();

        public ServiceLocatorViewModelProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public object GetViewModel(string viewModelName)
        {
            if (string.IsNullOrEmpty(viewModelName))
                throw new ArgumentNullException("viewModelName");

            ViewModelMetadata vmm;

            var isExist = _types.TryGetValue(viewModelName, out vmm);
            if (!isExist)
            {
                Debug.WriteLine("ViewModel Named: " + viewModelName + " , is not registered");
                return null;
            }

//            if (!_types.ContainsKey(viewModelName))
//            {
//               
//            }
//
//            var vmType = _types[viewModelName];
//            if (string.IsNullOrEmpty(vmType.Key))
//                return _serviceLocator.GetInstance(vmType.Type);
            try
            {
                return _serviceLocator.GetInstance(vmm.Type, vmm.Key);
            }
            catch (ActivationException ex)
            {
                var message = string.Format("ViewModel:{0}, not found", viewModelName);
                throw new ViewModelNotFoundExcepton(message, ex);
            }
        }

        public void Register(string viewModelName, Type viewModelType)
        {
            _types.Add(viewModelName, new ViewModelMetadata() {Type = viewModelType});
        }

        public void Register(string viewModelName, Type viewModelType, string viewModelKey)
        {
            _types.Add(viewModelName, new ViewModelMetadata() {Type = viewModelType, Key = viewModelKey});
        }

        public void Register<T>(string viewModelName)
        {
            Register(viewModelName, typeof (T));
        }

        public void Register<T>(string viewModelName, string viewModelKey)
        {
            Register(viewModelName, typeof (T), viewModelKey);
        }

        private class ViewModelMetadata
        {
            public Type Type { get; set; }
            public string Key { get; set; }
        }
    }
}