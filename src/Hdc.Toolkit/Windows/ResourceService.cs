using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;

// TODO: When using the attached property MergeSharedDictionaries then you are not allowed to assign a new ResourcesDictionary instance to the same control in XAML!
namespace Hdc.Windows
{
    /// <summary>
    /// This class provides helper methods to handle resources in WPF.
    /// </summary>
    public static class ResourceService
    {
        /// <summary>
        /// Identifies the attached MergeSharedDictionaries property. This property loads the specified dictionaries and merges them with the
        /// controls ResourceDictionary. By using this property the dictionaries are loaded only once.
        /// </summary>
        /// <remarks>
        /// You can specify more than one ResourceDictionary through this property. Use the '|' character to separate the source path of them.
        /// </remarks>
        public static readonly DependencyProperty MergeSharedDictionariesProperty =
            DependencyProperty.RegisterAttached("MergeSharedDictionaries", typeof(string), typeof(ResourceService),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, MergeSharedDictionariesChangedCallback));


        private static readonly Dictionary<Uri, ResourceDictionary> cachedDictionaries = new Dictionary<Uri, ResourceDictionary>();


        /// <summary>
        /// Gets the pack URI from an resource path.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns></returns>
        public static Uri GetPackUri(string resourcePath)
        {
            return new Uri("pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";Component/" + resourcePath);
        }

        /// <summary>
        /// Gets the ResourceDictionary. Using this method loads a ResourceDictionary only once.
        /// </summary>
        /// <param name="source">The source that defines where the ResourceDictionary can be found.</param>
        /// <returns></returns>
        public static ResourceDictionary GetSharedDictionary(Uri source)
        {
            if (source.IsAbsoluteUri && source.Scheme == "pack")
            {
                source = new Uri(source.AbsolutePath, UriKind.Relative);
            }

            ResourceDictionary dictionary;
            if (!cachedDictionaries.TryGetValue(source, out dictionary))
            {
                dictionary = (ResourceDictionary)Application.LoadComponent(source);
                cachedDictionaries.Add(source, dictionary);
            }
            return dictionary;
        }

/*        /// <summary>
        /// Creates the WPF BitmapSource from an Windows Forms Bitmap
        /// </summary>
        /// <param name="bitmap">The legacy bitmap object.</param>
        /// <returns></returns>
        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static BitmapSource CreateBitmapSource(System.Drawing.Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                new Int32Rect(0, 0, bitmap.Width, bitmap.Height), BitmapSizeOptions.FromEmptyOptions());
        }*/

        /// <summary>
        /// Gets the value of the MergeSharedDictionaries attached property of the specified element.
        /// </summary>
        /// <param name="element">The element to read the value from.</param>
        /// <returns></returns>
        public static string GetMergeSharedDictionaries(DependencyObject element)
        {
            return (string)element.GetValue(MergeSharedDictionariesProperty);
        }

        /// <summary>
        /// Sets the value of the MergeSharedDictionaries attached property of the specified element.
        /// </summary>
        /// <param name="element">The element to set the value.</param>
        /// <param name="value">The value.</param>
        public static void SetMergeSharedDictionaries(DependencyObject element, string value)
        {
            element.SetValue(MergeSharedDictionariesProperty, value);
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private static void MergeSharedDictionariesChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            string dictionaries = e.NewValue as string;
            if (!string.IsNullOrEmpty(dictionaries))
            {
                foreach (string dictionaryPath in dictionaries.Split('|'))
                {
                    ResourceDictionary dictionary = GetSharedDictionary(new Uri(dictionaryPath, UriKind.Relative));

                    if (obj is FrameworkElement)
                    {
                        ((FrameworkElement)obj).Resources.MergedDictionaries.Add(dictionary);
                    }
                    else if (obj is FrameworkContentElement)
                    {
                        ((FrameworkContentElement)obj).Resources.MergedDictionaries.Add(dictionary);
                    }
                }
            }
        }
    }
}
