using System.Windows;
using Hdc.Windows;

namespace Hdc.Controls
{
    public class HdcFonts
    {
        internal static ComponentResourceKey GetResourceKey(object keyId)
        {
            return keyId.GetResourceKey<HdcFonts>();
        }

        private static ResourceKey _fontSizeHugeKey;

        public static ResourceKey FontSizeHugeKey
        {
            get
            {
                return _fontSizeHugeKey ??
                       (_fontSizeHugeKey =
                        GetResourceKey(Id.FontSizeHuge));
            }
        }

        private static ResourceKey _fontSizeExtraLargeKey;

        public static ResourceKey FontSizeExtraLargeKey
        {
            get
            {
                return _fontSizeExtraLargeKey ??
                       (_fontSizeExtraLargeKey =
                        GetResourceKey(Id.FontSizeExtraLarge));
            }
        }

        private static ResourceKey _fontSizeLargeKey;

        public static ResourceKey FontSizeLargeKey
        {
            get
            {
                return _fontSizeLargeKey ??
                       (_fontSizeLargeKey =
                        GetResourceKey(Id.FontSizeLarge));
            }
        }

        private static ResourceKey _fontSizeMediumKey;

        public static ResourceKey FontSizeMediumKey
        {
            get
            {
                return _fontSizeMediumKey ??
                       (_fontSizeMediumKey =
                        GetResourceKey(Id.FontSizeMedium));
            }
        }

        private static ResourceKey _fontSizeSmallKey;

        public static ResourceKey FontSizeSmallKey
        {
            get
            {
                return _fontSizeSmallKey ??
                       (_fontSizeSmallKey =
                        GetResourceKey(Id.FontSizeSmall));
            }
        }

        private static ResourceKey _fontSizeTinyKey;

        public static ResourceKey FontSizeTinyKey
        {
            get
            {
                return _fontSizeTinyKey ??
                       (_fontSizeTinyKey =
                        GetResourceKey(Id.FontSizeTiny));
            }
        }

        private static ResourceKey _fontFamilyNormalKey;

        public static ResourceKey FontFamilyNormalKey
        {
            get
            {
                return _fontFamilyNormalKey ??
                       (_fontFamilyNormalKey =
                        GetResourceKey(Id.FontFamilyNormal));
            }
        }

        private static ResourceKey _fontFamilyNumberKey;

        public static ResourceKey FontFamilyNumberKey
        {
            get
            {
                return _fontFamilyNumberKey ??
                       (_fontFamilyNumberKey =
                        GetResourceKey(Id.FontFamilyNumber));
            }
        }

        private static ResourceKey _fontFamilyMonoKey;

        public static ResourceKey FontFamilyMonoKey
        {
            get
            {
                return _fontFamilyMonoKey ??
                       (_fontFamilyMonoKey =
                        GetResourceKey(Id.FontFamilyMono));
            }
        }

        private enum Id
        {
            FontSizeHuge,
            FontSizeExtraLarge,
            FontSizeLarge,
            FontSizeMedium,
            FontSizeSmall,
            FontSizeTiny,
            FontFamilyNormal,
            FontFamilyNumber,
            FontFamilyMono,
        }
    }
}