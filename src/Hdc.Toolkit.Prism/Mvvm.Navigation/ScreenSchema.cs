using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Resources;

namespace Hdc.Mvvm.Navigation
{
    [ContentProperty("TopScreenInfo")]
    public class ScreenSchema
    {
        public ScreenInfo TopScreenInfo { get; set; }

        public ScreenSchema()
        {
        }

        public ScreenSchema(ScreenInfo topScreenInfo)
        {
            TopScreenInfo = topScreenInfo;
        }

        public static ScreenSchema CreateFromXaml(Stream xamlStream)
        {
            if (xamlStream != null)
            {
                goto Label_000E;
            }
            throw new ArgumentNullException("xamlStream");
            Label_000E:
            return (XamlReader.Load(xamlStream) as ScreenSchema);
        }

        public static ScreenSchema CreateFromXaml(Uri builderResourceUri)
        {
            StreamResourceInfo info;
            info = Application.GetResourceStream(builderResourceUri);
            if (info == null)
            {
                goto Label_001E;
            }
            if (info.Stream == null)
            {
                goto Label_001E;
            }
            return CreateFromXaml(info.Stream);
            Label_001E:
            return null;
        }
    }
}