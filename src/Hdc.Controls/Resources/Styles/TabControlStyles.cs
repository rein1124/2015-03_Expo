using System.Windows;
using Hdc.Windows;

namespace Hdc.Controls.Styles
{
    public class TabControlStyles
    {
        internal static ComponentResourceKey GetResourceKey(object keyId)
        {
            return keyId.GetResourceKey<WindowStyles>();
        }

        private static ResourceKey _noHeaderTabControlStyleKey;

        public static ResourceKey NoHeaderTabControlStyleKey
        {
            get
            {
                return _noHeaderTabControlStyleKey ??
                       (_noHeaderTabControlStyleKey =
                        GetResourceKey(Id.NoHeaderTabControlStyle));
            }
        }

        private enum Id
        {
            NoHeaderTabControlStyle,
        }
    }
}