using System;
using System.Reflection;

namespace Hdc
{
    public static class UriExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        /// <remarks>
        /// code from EffectsLibrary.Global.MakePackUri in Catel 1.2
        /// usage: _pixelShader.UriSource = Global.MakePackUri("Transparency.ps");
        /// </remarks>
        public static Uri MakePackUri<T>(string relativeFile)
        {
            var assemblyShortName = GetAssemblyShortName<T>();

            string uriString = "pack://application:,,,/" + assemblyShortName + ";component/" + relativeFile;
            return new Uri(uriString);
        }

        private static string GetAssemblyShortName(this Assembly assembly)
        {
            var assemblyShortName = assembly.ToString().Split(',')[0];

            return assemblyShortName;
        }

        private static string GetAssemblyShortName<T>()
        {
            Assembly a = typeof (T).Assembly;

            return GetAssemblyShortName(a);
        }
    }
}