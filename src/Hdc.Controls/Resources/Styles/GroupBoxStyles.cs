using System.Windows;
using Hdc.Windows;

namespace Hdc.Controls.Styles
{
    public class GroupBoxStyles
    {
        internal static ComponentResourceKey GetResourceKey(object keyId)
        {
            return keyId.GetResourceKey<GroupBoxStyles>();
        }

        private static ResourceKey _groupBoxStyleKey;

        public static ResourceKey GroupBoxStyleKey
        {
            get
            {
                return _groupBoxStyleKey ??
                       (_groupBoxStyleKey =
                        GetResourceKey(Id.GroupBoxStyle));
            }
        }

        private static ResourceKey _dialogGroupBoxStyleKey;

        public static ResourceKey DialogGroupBoxStyleKey
        {
            get
            {
                return _dialogGroupBoxStyleKey ??
                       (_dialogGroupBoxStyleKey =
                        GetResourceKey(Id.DialogGroupBoxStyle));
            }
        }

        private enum Id
        {
            GroupBoxStyle,
            DialogGroupBoxStyle,
        }
    }
}