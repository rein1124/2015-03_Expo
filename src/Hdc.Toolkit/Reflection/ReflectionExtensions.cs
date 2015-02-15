using System.IO;

namespace Hdc.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using IO;

    using System.Xml.Linq;

    public static class ReflectionExtensions
    {
        public static Type GetTypeFromFullName(this string full)
        {
            if (string.IsNullOrEmpty(full))
            {
                return null;
            }
            var strs = full.Split(',');
            var fullName = strs[0].Trim();

            var asmFullName = full.Remove(0, strs[0].Length + 1).Trim();

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in asms)
            {
                if (asm.FullName == asmFullName)
                {
                    return asm.GetType(fullName);
                }
            }
            return null;
        }

        public static Type FindType(string typeFullName, string assemblyName)
        {
            if (assemblyName == null)
            {
                return Type.GetType(typeFullName);
            }

            //搜索当前域中已加载的程序集
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in asms)
            {
                string[] names = asm.FullName.Split(',');
                if (names[0].Trim() == assemblyName.Trim())
                {
                    return asm.GetType(typeFullName);
                }
            }

            //加载目标程序集
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
            {
                return tarAssem.GetType(typeFullName);
            }

            return null;
        }

        public static Type FindType(string full)
        {
            if (string.IsNullOrEmpty(full))
            {
                return null;
            }
            var strs = full.Split(',');
            var fullName = strs[0].Trim();

            var asmFullName = full.Remove(0, strs[0].Length + 1).Trim();

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in asms)
            {
                if (asm.FullName == asmFullName)
                {
                    return asm.GetType(fullName);
                }
            }
            return null;
        }

        /// <exception cref="InvalidOperationException">resource name cannot be found</exception>
        public static XDocument LoadXDocumentFromEmbeddedResource(this Assembly assembly, string resourceName)
        {
            using (var rcsResourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (rcsResourceStream == null)
                {
                    throw new InvalidOperationException("resource name cannot be found");
                }
                {
                    using (var reader = new StreamReader(rcsResourceStream))
                    {
                        return XDocument.Load(reader);
                    }
                }
            }
        }

        /// <exception cref="InvalidOperationException">resource name cannot be found</exception>
        public static string LoadStringFromEmbeddedResource(this Assembly assembly, string resourceName)
        {
            using (var rcsResourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (rcsResourceStream == null)
                {
                    throw new InvalidOperationException("resource name cannot be found");
                }
                {
                    using (var reader = new StreamReader(rcsResourceStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static string GetAssemblyDirectoryPath(this Assembly assembly)
        {
            var fullPath = assembly.Location;
            var fileInfo = new FileInfo(fullPath);
            return fileInfo.Directory.FullName;
        }
    }
}