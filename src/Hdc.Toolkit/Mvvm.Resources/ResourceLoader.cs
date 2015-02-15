using System.Collections.Concurrent;
using Hdc.Serialization;

namespace Hdc.Mvvm.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Markup;
    using System.Xml;

    internal class ResourceLoader //: FrameworkElement
    {
        private readonly List<string> _loadedXamlFiles = new List<string>();

        private readonly IDictionary<string, string> _xamlFiles = new ConcurrentDictionary<string, string>();

        private ResourceDictionary _resourceDictionary;

        public ResourceDictionary ResourceDictionary
        {
            get { return _resourceDictionary ?? (_resourceDictionary = new ResourceDictionary()); }
        }

        public ResourceLoader(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;

            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                string fullName = fileInfo.FullName;
                string shortName = Path.GetFileNameWithoutExtension(fullName);
                string extention = Path.GetExtension(fullName);

                if (String.Equals(".xaml", extention, StringComparison.OrdinalIgnoreCase))
                {
                    _xamlFiles.Add(shortName, fullName);
                }
            }
        }

        public DirectoryInfo DirectoryInfo { get; private set; }

//        public static object CloneByXaml(object source)
//        {
//            if (source == null)
//            {
//                return (null);
//            }
//
//            string s = XamlWriter.Save(source);
//
//            var stringReader = new StringReader(s);
//
//            XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());
//
//            return XamlReader.Load(xmlReader);
//        }

        public object LoadXaml(string resourceName)
        {
            try
            {
//                object resource = TryFindResource(resourceName);
                object resource = ResourceDictionary[resourceName];

                if (resource != null)
                {
//                    return resource.CloneUsingXamlSerialization();
                    return resource;
                }

                if (!_xamlFiles.ContainsKey(resourceName))
                {
                    return null;
                }

                if (!_loadedXamlFiles.Contains(resourceName))
                {
//                    using (var fileStream = new FileStream(_xamlFiles[resourceName], FileMode.Open))
//                    {
//                        var resourceDictionary = XamlReader.Load(fileStream) as ResourceDictionary;
//                    }
                    var resourceDictionary = _xamlFiles[resourceName].LoadResourceDictionary();
                    ResourceDictionary.MergedDictionaries.Add(resourceDictionary);

                    _loadedXamlFiles.Add(resourceName);

                    resource = ResourceDictionary[resourceName];
                    return resource;
//                    if (resource != null)
//                    {
//                        return resource.CloneUsingXamlSerialization();
//                    }
                }

                return null;
            }
            catch (Exception e)
            {
                throw new ResourceLoadingFailedException("cannot load resource", e);
                //return null;
            }
        }
    }
}