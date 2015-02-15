using System.Collections.Concurrent;

namespace Hdc.Mvvm.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ResourceLoaderManager : IResourceLoaderManager
    {
        private const string DefaultHdcResourcesDirectory = ".";

        private IDictionary<string, ResourceLoader> _resourceLoaders;

        public ResourceLoaderManager(params string[] folders)
        {
            AddFolder(DefaultHdcResourcesDirectory);

            if (folders != null)
            {
                foreach (string folder  in folders)
                {
                    AddFolder(folder);
                }
            }
        }

        public static string DefaultPath
        {
            get { return DefaultHdcResourcesDirectory; }
        }

        public void AddFolder(string folder)
        {
            if (_resourceLoaders == null)
                _resourceLoaders = new ConcurrentDictionary<string, ResourceLoader>();

            DirectoryInfo directoryInfoVerified;

            try
            {
                if (Directory.Exists(folder))
                {
                    var directoryInfo1 = new DirectoryInfo(folder);

                    directoryInfoVerified = !_resourceLoaders.ContainsKey(directoryInfo1.FullName)
                                                    ? directoryInfo1
                                                    : null;
                }
                else if (Directory.Exists(DefaultHdcResourcesDirectory + folder))
                {
                    var directoryInfo1 = new DirectoryInfo(DefaultHdcResourcesDirectory + folder);

                    directoryInfoVerified = !_resourceLoaders.ContainsKey(directoryInfo1.FullName)
                                                    ? directoryInfo1
                                                    : null;
                }
                else
                    directoryInfoVerified = null;
            }
            catch (Exception)
            {
                directoryInfoVerified = null;
            }

            if (directoryInfoVerified != null)
                _resourceLoaders.Add(directoryInfoVerified.FullName, new ResourceLoader(directoryInfoVerified));
        }

        public object LoadXaml(string resourceName)
        {
            for (int i = _resourceLoaders.Count - 1; i > -1; i--)
            {
                ResourceLoader resourceLoader = _resourceLoaders.ToList()[i].Value;
                object resource = resourceLoader.LoadXaml(resourceName);
                if (resource != null)
                    return resource;
            }
            return resourceName;
        }
    }
}