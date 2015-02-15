namespace Hdc.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    /// <summary>
    /// This class provides information about the running application.
    /// </summary>
    public static class ApplicationInfo
    {
        private static string productName;

        private static string version;

        private static string copyright;

        /// <summary>
        /// Gets the product name of the entry assembly.
        /// </summary>
        /// <value>The name of the product.</value>
        public static string ProductName
        {
            get
            {
                if (String.IsNullOrEmpty(productName))
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        productName =
                            ((AssemblyProductAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyProductAttribute))).Product;
                    }
                }
                return productName;
            }
        }

        /// <summary>
        /// Gets the version number of the entry assembly.
        /// </summary>
        /// <value>The version number.</value>
        public static string Version
        {
            get
            {
                if (String.IsNullOrEmpty(version))
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        version = entryAssembly.GetName().Version.ToString();
                    }
                }
                return version;
            }
        }

        /// <summary>
        /// Gets the copyright information of the entry asssembly.
        /// </summary>
        /// <value>The copyright information.</value>
        public static string Copyright
        {
            get
            {
                if (String.IsNullOrEmpty(copyright))
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        copyright =
                            ((AssemblyCopyrightAttribute)
                             Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCopyrightAttribute))).Copyright;
                    }
                }
                return copyright;
            }
        }
    }
}