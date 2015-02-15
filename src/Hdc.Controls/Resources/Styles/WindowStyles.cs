using System.Windows;
using Hdc.Windows;

namespace Hdc.Controls.Styles
{
    public class WindowStyles
    {
        internal static ComponentResourceKey GetResourceKey(object keyId)
        {
            return keyId.GetResourceKey<WindowStyles>();
        }

        private static ResourceKey _windowRegionStyleKey;

        public static ResourceKey WindowRegionStyleKey
        {
            get
            {
                return _windowRegionStyleKey ??
                       (_windowRegionStyleKey =
                        GetResourceKey(Id.WindowRegionStyle));
            }
        }

        private static ResourceKey _transparentWindowRegionStyleKey;

        public static ResourceKey TransparentWindowRegionStyleKey
        {
            get
            {
                return _transparentWindowRegionStyleKey ??
                       (_transparentWindowRegionStyleKey =
                        GetResourceKey(Id.TransparentWindowRegionStyle));
            }
        }

        private static ResourceKey _transparentMaximizedWindowRegionStyleKey;

        public static ResourceKey TransparentMaximizedWindowRegionStyleKey
        {
            get
            {
                return _transparentMaximizedWindowRegionStyleKey ??
                       (_transparentMaximizedWindowRegionStyleKey =
                        GetResourceKey(Id.TransparentMaximizedWindowRegionStyle));
            }
        }

        private enum Id
        {
            WindowRegionStyle,
            TransparentWindowRegionStyle,
            TransparentMaximizedWindowRegionStyle,
        }
    }
}