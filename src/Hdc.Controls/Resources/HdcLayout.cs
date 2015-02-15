using System.Windows;
using Hdc.Windows;

// ReSharper disable CheckNamespace

namespace Hdc.Controls
// ReSharper restore CheckNamespace
{
    public static class LayoutExtensions
    {
        internal static ComponentResourceKey GetResourceKey(this object keyId)
        {
            return keyId.GetResourceKey<HdcLayout>();
        }
    }

    public class HdcLayout
    {
        private static ResourceKey _buttonSideLengthKey;

        public static ResourceKey ButtonSideLengthKey
        {
            get
            {
                return _buttonSideLengthKey ??
                       (_buttonSideLengthKey =
                        Id.ButtonSideLengthResourceKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM1Key;

        public static ResourceKey ButtonSideLengthM1Key
        {
            get
            {
                return _buttonSideLengthM1Key ??
                       (_buttonSideLengthM1Key =
                        Id.ButtonSideLengthM1KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM2Key;

        public static ResourceKey ButtonSideLengthM2Key
        {
            get
            {
                return _buttonSideLengthM2Key ??
                       (_buttonSideLengthM2Key =
                        Id.ButtonSideLengthM2KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM3Key;

        public static ResourceKey ButtonSideLengthM3Key
        {
            get
            {
                return _buttonSideLengthM3Key ??
                       (_buttonSideLengthM3Key =
                        Id.ButtonSideLengthM3KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM4Key;

        public static ResourceKey ButtonSideLengthM4Key
        {
            get
            {
                return _buttonSideLengthM4Key ??
                       (_buttonSideLengthM4Key =
                        Id.ButtonSideLengthM4KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM5Key;

        public static ResourceKey ButtonSideLengthM5Key
        {
            get
            {
                return _buttonSideLengthM5Key ??
                       (_buttonSideLengthM5Key =
                        Id.ButtonSideLengthM5KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthM6Key;

        public static ResourceKey ButtonSideLengthM6Key
        {
            get
            {
                return _buttonSideLengthM6Key ??
                       (_buttonSideLengthM6Key =
                        Id.ButtonSideLengthM6KeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonCornerRadiusKey;

        public static ResourceKey ButtonCornerRadiusKey
        {
            get
            {
                return _buttonCornerRadiusKey ??
                       (_buttonCornerRadiusKey =
                        Id.ButtonCornerRadiusKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderCornerRadiusKey;

        public static ResourceKey BorderCornerRadiusKey
        {
            get
            {
                return _borderCornerRadiusKey ??
                       (_borderCornerRadiusKey =
                        Id.BorderCornerRadiusKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonSideLengthWithBorderKey;

        public static ResourceKey ButtonSideLengthWithBorderKey
        {
            get { return _buttonSideLengthWithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM1WithBorderKey;

        public static ResourceKey ButtonSideLengthM1WithBorderKey
        {
            get { return _buttonSideLengthM1WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM2WithBorderKey;

        public static ResourceKey ButtonSideLengthM2WithBorderKey
        {
            get { return _buttonSideLengthM2WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM3WithBorderKey;

        public static ResourceKey ButtonSideLengthM3WithBorderKey
        {
            get { return _buttonSideLengthM3WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM4WithBorderKey;

        public static ResourceKey ButtonSideLengthM4WithBorderKey
        {
            get { return _buttonSideLengthM4WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM5WithBorderKey;

        public static ResourceKey ButtonSideLengthM5WithBorderKey
        {
            get { return _buttonSideLengthM5WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthM6WithBorderKey;

        public static ResourceKey ButtonSideLengthM6WithBorderKey
        {
            get { return _buttonSideLengthM6WithBorderKey; }
        }

        //
        private static ResourceKey _buttonSideLengthSmallKey;

        public static ResourceKey ButtonSideLengthSmallKey
        {
            get { return _buttonSideLengthSmallKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM1Key;

        public static ResourceKey ButtonSideLengthSmallM1Key
        {
            get { return _buttonSideLengthSmallM1Key; }
        }

        private static ResourceKey _buttonSideLengthSmallM2Key;

        public static ResourceKey ButtonSideLengthSmallM2Key
        {
            get { return _buttonSideLengthSmallM2Key; }
        }

        private static ResourceKey _buttonSideLengthSmallM3Key;

        public static ResourceKey ButtonSideLengthSmallM3Key
        {
            get { return _buttonSideLengthSmallM3Key; }
        }

        private static ResourceKey _buttonSideLengthSmallM4Key;

        public static ResourceKey ButtonSideLengthSmallM4Key
        {
            get { return _buttonSideLengthSmallM4Key; }
        }

        private static ResourceKey _buttonSideLengthSmallM5Key;

        public static ResourceKey ButtonSideLengthSmallM5Key
        {
            get { return _buttonSideLengthSmallM5Key; }
        }

        private static ResourceKey _buttonSideLengthSmallM6Key;

        public static ResourceKey ButtonSideLengthSmallM6Key
        {
            get { return _buttonSideLengthSmallM6Key; }
        }

        //
        private static ResourceKey _buttonSideLengthSmallWithBorderKey;

        public static ResourceKey ButtonSideLengthSmallWithBorderKey
        {
            get { return _buttonSideLengthSmallWithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM1WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM1WithBorderKey
        {
            get { return _buttonSideLengthSmallM1WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM2WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM2WithBorderKey
        {
            get { return _buttonSideLengthSmallM2WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM3WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM3WithBorderKey
        {
            get { return _buttonSideLengthSmallM3WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM4WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM4WithBorderKey
        {
            get { return _buttonSideLengthSmallM4WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM5WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM5WithBorderKey
        {
            get { return _buttonSideLengthSmallM5WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthSmallM6WithBorderKey;

        public static ResourceKey ButtonSideLengthSmallM6WithBorderKey
        {
            get { return _buttonSideLengthSmallM6WithBorderKey; }
        }

        //
        private static ResourceKey _buttonSideLengthTinyKey;

        public static ResourceKey ButtonSideLengthTinyKey
        {
            get { return _buttonSideLengthTinyKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM1Key;

        public static ResourceKey ButtonSideLengthTinyM1Key
        {
            get { return _buttonSideLengthTinyM1Key; }
        }

        private static ResourceKey _buttonSideLengthTinyM2Key;

        public static ResourceKey ButtonSideLengthTinyM2Key
        {
            get { return _buttonSideLengthTinyM2Key; }
        }

        private static ResourceKey _buttonSideLengthTinyM3Key;

        public static ResourceKey ButtonSideLengthTinyM3Key
        {
            get { return _buttonSideLengthTinyM3Key; }
        }

        private static ResourceKey _buttonSideLengthTinyM4Key;

        public static ResourceKey ButtonSideLengthTinyM4Key
        {
            get { return _buttonSideLengthTinyM4Key; }
        }

        private static ResourceKey _buttonSideLengthTinyM5Key;

        public static ResourceKey ButtonSideLengthTinyM5Key
        {
            get { return _buttonSideLengthTinyM5Key; }
        }

        private static ResourceKey _buttonSideLengthTinyM6Key;

        public static ResourceKey ButtonSideLengthTinyM6Key
        {
            get { return _buttonSideLengthTinyM6Key; }
        }

        //
        private static ResourceKey _buttonSideLengthTinyWithBorderKey;

        public static ResourceKey ButtonSideLengthTinyWithBorderKey
        {
            get { return _buttonSideLengthTinyWithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM1WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM1WithBorderKey
        {
            get { return _buttonSideLengthTinyM1WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM2WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM2WithBorderKey
        {
            get { return _buttonSideLengthTinyM2WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM3WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM3WithBorderKey
        {
            get { return _buttonSideLengthTinyM3WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM4WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM4WithBorderKey
        {
            get { return _buttonSideLengthTinyM4WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM5WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM5WithBorderKey
        {
            get { return _buttonSideLengthTinyM5WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthTinyM6WithBorderKey;

        public static ResourceKey ButtonSideLengthTinyM6WithBorderKey
        {
            get { return _buttonSideLengthTinyM6WithBorderKey; }
        }

        //
        private static ResourceKey _buttonSideLengthLargeKey;

        public static ResourceKey ButtonSideLengthLargeKey
        {
            get { return _buttonSideLengthLargeKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM1Key;

        public static ResourceKey ButtonSideLengthLargeM1Key
        {
            get { return _buttonSideLengthLargeM1Key; }
        }

        private static ResourceKey _buttonSideLengthLargeM2Key;

        public static ResourceKey ButtonSideLengthLargeM2Key
        {
            get { return _buttonSideLengthLargeM2Key; }
        }

        private static ResourceKey _buttonSideLengthLargeM3Key;

        public static ResourceKey ButtonSideLengthLargeM3Key
        {
            get { return _buttonSideLengthLargeM3Key; }
        }

        private static ResourceKey _buttonSideLengthLargeM4Key;

        public static ResourceKey ButtonSideLengthLargeM4Key
        {
            get { return _buttonSideLengthLargeM4Key; }
        }

        private static ResourceKey _buttonSideLengthLargeM5Key;

        public static ResourceKey ButtonSideLengthLargeM5Key
        {
            get { return _buttonSideLengthLargeM5Key; }
        }

        private static ResourceKey _buttonSideLengthLargeM6Key;

        public static ResourceKey ButtonSideLengthLargeM6Key
        {
            get { return _buttonSideLengthLargeM6Key; }
        }

        //
        private static ResourceKey _buttonSideLengthLargeWithBorderKey;

        public static ResourceKey ButtonSideLengthLargeWithBorderKey
        {
            get { return _buttonSideLengthLargeWithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM1WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM1WithBorderKey
        {
            get { return _buttonSideLengthLargeM1WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM2WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM2WithBorderKey
        {
            get { return _buttonSideLengthLargeM2WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM3WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM3WithBorderKey
        {
            get { return _buttonSideLengthLargeM3WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM4WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM4WithBorderKey
        {
            get { return _buttonSideLengthLargeM4WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM5WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM5WithBorderKey
        {
            get { return _buttonSideLengthLargeM5WithBorderKey; }
        }

        private static ResourceKey _buttonSideLengthLargeM6WithBorderKey;

        public static ResourceKey ButtonSideLengthLargeM6WithBorderKey
        {
            get { return _buttonSideLengthLargeM6WithBorderKey; }
        }

        //Margin
        private static ResourceKey _marginKey;

        public static ResourceKey MarginKey
        {
            get { return _marginKey; }
        }

        private static ResourceKey _marginLeftKey;

        public static ResourceKey MarginLeftKey
        {
            get { return _marginLeftKey; }
        }

        private static ResourceKey _marginRightKey;

        public static ResourceKey MarginRightKey
        {
            get { return _marginRightKey; }
        }

        private static ResourceKey _marginTopKey;

        public static ResourceKey MarginTopKey
        {
            get { return _marginTopKey; }
        }

        private static ResourceKey _marginBottomKey;

        public static ResourceKey MarginBottomKey
        {
            get { return _marginBottomKey; }
        }

        private static ResourceKey _marginVerticalKey;

        public static ResourceKey MarginVerticalKey
        {
            get { return _marginVerticalKey; }
        }

        private static ResourceKey _marginHorizontalKey;

        public static ResourceKey MarginHorizontalKey
        {
            get { return _marginHorizontalKey; }
        }

        private static ResourceKey _marginLeftTopKey;

        public static ResourceKey MarginLeftTopKey
        {
            get { return _marginLeftTopKey; }
        }

        private static ResourceKey _marginLeftBottomKey;

        public static ResourceKey MarginLeftBottomKey
        {
            get { return _marginLeftBottomKey; }
        }

        private static ResourceKey _marginRightTopKey;

        public static ResourceKey MarginRightTopKey
        {
            get { return _marginRightTopKey; }
        }

        private static ResourceKey _marginRightBottomKey;

        public static ResourceKey MarginRightBottomKey
        {
            get { return _marginRightBottomKey; }
        }

        //MarginLarge
        private static ResourceKey _marginLargeKey;

        public static ResourceKey MarginLargeKey
        {
            get { return _marginLargeKey; }
        }

        private static ResourceKey _marginLargeLeftKey;

        public static ResourceKey MarginLargeLeftKey
        {
            get { return _marginLargeLeftKey; }
        }

        private static ResourceKey _marginLargeRightKey;

        public static ResourceKey MarginLargeRightKey
        {
            get { return _marginLargeRightKey; }
        }

        private static ResourceKey _marginLargeTopKey;

        public static ResourceKey MarginLargeTopKey
        {
            get { return _marginLargeTopKey; }
        }

        private static ResourceKey _marginLargeBottomKey;

        public static ResourceKey MarginLargeBottomKey
        {
            get { return _marginLargeBottomKey; }
        }

        private static ResourceKey _marginLargeVerticalKey;

        public static ResourceKey MarginLargeVerticalKey
        {
            get { return _marginLargeVerticalKey; }
        }

        private static ResourceKey _marginLargeHorizontalKey;

        public static ResourceKey MarginLargeHorizontalKey
        {
            get { return _marginLargeHorizontalKey; }
        }

        private static ResourceKey _marginLargeLeftTopKey;

        public static ResourceKey MarginLargeLeftTopKey
        {
            get { return _marginLargeLeftTopKey; }
        }

        private static ResourceKey _marginLargeLeftBottomKey;

        public static ResourceKey MarginLargeLeftBottomKey
        {
            get { return _marginLargeLeftBottomKey; }
        }

        private static ResourceKey _marginLargeRightTopKey;

        public static ResourceKey MarginLargeRightTopKey
        {
            get { return _marginLargeRightTopKey; }
        }

        private static ResourceKey _marginLargeRightBottomKey;

        public static ResourceKey MarginLargeRightBottomKey
        {
            get { return _marginLargeRightBottomKey; }
        }

        //MarginExtraLarge
        private static ResourceKey _marginExtraLargeKey;

        public static ResourceKey MarginExtraLargeKey
        {
            get { return _marginExtraLargeKey; }
        }

        private static ResourceKey _marginExtraLargeLeftKey;

        public static ResourceKey MarginExtraLargeLeftKey
        {
            get { return _marginExtraLargeLeftKey; }
        }

        private static ResourceKey _marginExtraLargeRightKey;

        public static ResourceKey MarginExtraLargeRightKey
        {
            get { return _marginExtraLargeRightKey; }
        }

        private static ResourceKey _marginExtraLargeTopKey;

        public static ResourceKey MarginExtraLargeTopKey
        {
            get { return _marginExtraLargeTopKey; }
        }

        private static ResourceKey _marginExtraLargeBottomKey;

        public static ResourceKey MarginExtraLargeBottomKey
        {
            get { return _marginExtraLargeBottomKey; }
        }

        private static ResourceKey _marginExtraLargeVerticalKey;

        public static ResourceKey MarginExtraLargeVerticalKey
        {
            get { return _marginExtraLargeVerticalKey; }
        }

        private static ResourceKey _marginExtraLargeHorizontalKey;

        public static ResourceKey MarginExtraLargeHorizontalKey
        {
            get { return _marginExtraLargeHorizontalKey; }
        }

        private static ResourceKey _marginExtraLargeLeftTopKey;

        public static ResourceKey MarginExtraLargeLeftTopKey
        {
            get { return _marginExtraLargeLeftTopKey; }
        }

        private static ResourceKey _marginExtraLargeLeftBottomKey;

        public static ResourceKey MarginExtraLargeLeftBottomKey
        {
            get { return _marginExtraLargeLeftBottomKey; }
        }

        private static ResourceKey _marginExtraLargeRightTopKey;

        public static ResourceKey MarginExtraLargeRightTopKey
        {
            get { return _marginExtraLargeRightTopKey; }
        }

        private static ResourceKey _marginExtraLargeRightBottomKey;

        public static ResourceKey MarginExtraLargeRightBottomKey
        {
            get { return _marginExtraLargeRightBottomKey; }
        }

        //MarginExtraExtraLarge
        private static ResourceKey _marginExtraExtraLargeKey;

        public static ResourceKey MarginExtraExtraLargeKey
        {
            get { return _marginExtraExtraLargeKey; }
        }

        private static ResourceKey _marginExtraExtraLargeLeftKey;

        public static ResourceKey MarginExtraExtraLargeLeftKey
        {
            get { return _marginExtraExtraLargeLeftKey; }
        }

        private static ResourceKey _marginExtraExtraLargeRightKey;

        public static ResourceKey MarginExtraExtraLargeRightKey
        {
            get { return _marginExtraExtraLargeRightKey; }
        }

        private static ResourceKey _marginExtraExtraLargeTopKey;

        public static ResourceKey MarginExtraExtraLargeTopKey
        {
            get { return _marginExtraExtraLargeTopKey; }
        }

        private static ResourceKey _marginExtraExtraLargeBottomKey;

        public static ResourceKey MarginExtraExtraLargeBottomKey
        {
            get { return _marginExtraExtraLargeBottomKey; }
        }

        private static ResourceKey _marginExtraExtraLargeVerticalKey;

        public static ResourceKey MarginExtraExtraLargeVerticalKey
        {
            get { return _marginExtraExtraLargeVerticalKey; }
        }

        private static ResourceKey _marginExtraExtraLargeHorizontalKey;

        public static ResourceKey MarginExtraExtraLargeHorizontalKey
        {
            get { return _marginExtraExtraLargeHorizontalKey; }
        }

        private static ResourceKey _marginExtraExtraLargeLeftTopKey;

        public static ResourceKey MarginExtraExtraLargeLeftTopKey
        {
            get { return _marginExtraExtraLargeLeftTopKey; }
        }

        private static ResourceKey _marginExtraExtraLargeLeftBottomKey;

        public static ResourceKey MarginExtraExtraLargeLeftBottomKey
        {
            get { return _marginExtraExtraLargeLeftBottomKey; }
        }

        private static ResourceKey _marginExtraExtraLargeRightTopKey;

        public static ResourceKey MarginExtraExtraLargeRightTopKey
        {
            get { return _marginExtraExtraLargeRightTopKey; }
        }

        private static ResourceKey _marginExtraExtraLargeRightBottomKey;

        public static ResourceKey MarginExtraExtraLargeRightBottomKey
        {
            get { return _marginExtraExtraLargeRightBottomKey; }
        }

        //Padding
        private static ResourceKey _paddingKey;

        public static ResourceKey PaddingKey
        {
            get { return _paddingKey; }
        }

        private static ResourceKey _paddingLeftKey;

        public static ResourceKey PaddingLeftKey
        {
            get { return _paddingLeftKey; }
        }

        private static ResourceKey _paddingRightKey;

        public static ResourceKey PaddingRightKey
        {
            get { return _paddingRightKey; }
        }

        private static ResourceKey _paddingTopKey;

        public static ResourceKey PaddingTopKey
        {
            get { return _paddingTopKey; }
        }

        private static ResourceKey _paddingBottomKey;

        public static ResourceKey PaddingBottomKey
        {
            get { return _paddingBottomKey; }
        }

        private static ResourceKey _paddingVerticalKey;

        public static ResourceKey PaddingVerticalKey
        {
            get { return _paddingVerticalKey; }
        }

        private static ResourceKey _paddingHorizontalKey;

        public static ResourceKey PaddingHorizontalKey
        {
            get { return _paddingHorizontalKey; }
        }

        private static ResourceKey _paddingLeftTopKey;

        public static ResourceKey PaddingLeftTopKey
        {
            get { return _paddingLeftTopKey; }
        }

        private static ResourceKey _paddingLeftBottomKey;

        public static ResourceKey PaddingLeftBottomKey
        {
            get { return _paddingLeftBottomKey; }
        }

        private static ResourceKey _paddingRightTopKey;

        public static ResourceKey PaddingRightTopKey
        {
            get { return _paddingRightTopKey; }
        }

        private static ResourceKey _paddingRightBottomKey;

        public static ResourceKey PaddingRightBottomKey
        {
            get { return _paddingRightBottomKey; }
        }

        //PaddingLarge
        private static ResourceKey _paddingLargeKey;

        public static ResourceKey PaddingLargeKey
        {
            get { return _paddingLargeKey; }
        }

        private static ResourceKey _paddingLargeLeftKey;

        public static ResourceKey PaddingLargeLeftKey
        {
            get { return _paddingLargeLeftKey; }
        }

        private static ResourceKey _paddingLargeRightKey;

        public static ResourceKey PaddingLargeRightKey
        {
            get { return _paddingLargeRightKey; }
        }

        private static ResourceKey _paddingLargeTopKey;

        public static ResourceKey PaddingLargeTopKey
        {
            get { return _paddingLargeTopKey; }
        }

        private static ResourceKey _paddingLargeBottomKey;

        public static ResourceKey PaddingLargeBottomKey
        {
            get { return _paddingLargeBottomKey; }
        }

        private static ResourceKey _paddingLargeVerticalKey;

        public static ResourceKey PaddingLargeVerticalKey
        {
            get { return _paddingLargeVerticalKey; }
        }

        private static ResourceKey _paddingLargeHorizontalKey;

        public static ResourceKey PaddingLargeHorizontalKey
        {
            get { return _paddingLargeHorizontalKey; }
        }

        private static ResourceKey _paddingLargeLeftTopKey;

        public static ResourceKey PaddingLargeLeftTopKey
        {
            get { return _paddingLargeLeftTopKey; }
        }

        private static ResourceKey _paddingLargeLeftBottomKey;

        public static ResourceKey PaddingLargeLeftBottomKey
        {
            get { return _paddingLargeLeftBottomKey; }
        }

        private static ResourceKey _paddingLargeRightTopKey;

        public static ResourceKey PaddingLargeRightTopKey
        {
            get { return _paddingLargeRightTopKey; }
        }

        private static ResourceKey _paddingLargeRightBottomKey;

        public static ResourceKey PaddingLargeRightBottomKey
        {
            get { return _paddingLargeRightBottomKey; }
        }

        //PaddingExtraLarge
        private static ResourceKey _paddingExtraLargeKey;

        public static ResourceKey PaddingExtraLargeKey
        {
            get { return _paddingExtraLargeKey; }
        }

        private static ResourceKey _paddingExtraLargeLeftKey;

        public static ResourceKey PaddingExtraLargeLeftKey
        {
            get { return _paddingExtraLargeLeftKey; }
        }

        private static ResourceKey _paddingExtraLargeRightKey;

        public static ResourceKey PaddingExtraLargeRightKey
        {
            get { return _paddingExtraLargeRightKey; }
        }

        private static ResourceKey _paddingExtraLargeTopKey;

        public static ResourceKey PaddingExtraLargeTopKey
        {
            get { return _paddingExtraLargeTopKey; }
        }

        private static ResourceKey _paddingExtraLargeBottomKey;

        public static ResourceKey PaddingExtraLargeBottomKey
        {
            get { return _paddingExtraLargeBottomKey; }
        }

        private static ResourceKey _paddingExtraLargeVerticalKey;

        public static ResourceKey PaddingExtraLargeVerticalKey
        {
            get { return _paddingExtraLargeVerticalKey; }
        }

        private static ResourceKey _paddingExtraLargeHorizontalKey;

        public static ResourceKey PaddingExtraLargeHorizontalKey
        {
            get { return _paddingExtraLargeHorizontalKey; }
        }

        private static ResourceKey _paddingExtraLargeLeftTopKey;

        public static ResourceKey PaddingExtraLargeLeftTopKey
        {
            get { return _paddingExtraLargeLeftTopKey; }
        }

        private static ResourceKey _paddingExtraLargeLeftBottomKey;

        public static ResourceKey PaddingExtraLargeLeftBottomKey
        {
            get { return _paddingExtraLargeLeftBottomKey; }
        }

        private static ResourceKey _paddingExtraLargeRightTopKey;

        public static ResourceKey PaddingExtraLargeRightTopKey
        {
            get { return _paddingExtraLargeRightTopKey; }
        }

        private static ResourceKey _paddingExtraLargeRightBottomKey;

        public static ResourceKey PaddingExtraLargeRightBottomKey
        {
            get { return _paddingExtraLargeRightBottomKey; }
        }

        //PaddingExtraExtraLarge
        private static ResourceKey _paddingExtraExtraLargeKey;

        public static ResourceKey PaddingExtraExtraLargeKey
        {
            get { return _paddingExtraExtraLargeKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeLeftKey;

        public static ResourceKey PaddingExtraExtraLargeLeftKey
        {
            get { return _paddingExtraExtraLargeLeftKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeRightKey;

        public static ResourceKey PaddingExtraExtraLargeRightKey
        {
            get { return _paddingExtraExtraLargeRightKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeTopKey;

        public static ResourceKey PaddingExtraExtraLargeTopKey
        {
            get { return _paddingExtraExtraLargeTopKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeBottomKey;

        public static ResourceKey PaddingExtraExtraLargeBottomKey
        {
            get { return _paddingExtraExtraLargeBottomKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeVerticalKey;

        public static ResourceKey PaddingExtraExtraLargeVerticalKey
        {
            get { return _paddingExtraExtraLargeVerticalKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeHorizontalKey;

        public static ResourceKey PaddingExtraExtraLargeHorizontalKey
        {
            get { return _paddingExtraExtraLargeHorizontalKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeLeftTopKey;

        public static ResourceKey PaddingExtraExtraLargeLeftTopKey
        {
            get { return _paddingExtraExtraLargeLeftTopKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeLeftBottomKey;

        public static ResourceKey PaddingExtraExtraLargeLeftBottomKey
        {
            get { return _paddingExtraExtraLargeLeftBottomKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeRightTopKey;

        public static ResourceKey PaddingExtraExtraLargeRightTopKey
        {
            get { return _paddingExtraExtraLargeRightTopKey; }
        }

        private static ResourceKey _paddingExtraExtraLargeRightBottomKey;

        public static ResourceKey PaddingExtraExtraLargeRightBottomKey
        {
            get { return _paddingExtraExtraLargeRightBottomKey; }
        }

        private static ResourceKey _borderThicknessKey;

        public static ResourceKey BorderThicknessKey
        {
            get
            {
                return _borderThicknessKey ??
                       (_borderThicknessKey =
                        Id.BorderThicknessKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessLeftKey;

        public static ResourceKey BorderThicknessLeftKey
        {
            get
            {
                return _borderThicknessLeftKey ??
                       (_borderThicknessLeftKey =
                        Id.BorderThicknessLeftKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessRightKey;

        public static ResourceKey BorderThicknessRightKey
        {
            get
            {
                return _borderThicknessRightKey ??
                       (_borderThicknessRightKey =
                        Id.BorderThicknessRightKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessTopKey;

        public static ResourceKey BorderThicknessTopKey
        {
            get
            {
                return _borderThicknessTopKey ??
                       (_borderThicknessTopKey =
                        Id.BorderThicknessTopKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessBottomKey;

        public static ResourceKey BorderThicknessBottomKey
        {
            get
            {
                return _borderThicknessBottomKey ??
                       (_borderThicknessBottomKey =
                        Id.BorderThicknessBottomKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonBorderThicknessKey;

        public static ResourceKey ButtonBorderThicknessKey
        {
            get
            {
                return _buttonBorderThicknessKey ??
                       (_buttonBorderThicknessKey =
                        Id.ButtonBorderThicknessKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessThickKey;

        public static ResourceKey BorderThicknessThickKey
        {
            get
            {
                return _borderThicknessThickKey ??
                       (_borderThicknessThickKey =
                        Id.BorderThicknessThickKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessThickLeftKey;

        public static ResourceKey BorderThicknessThickLeftKey
        {
            get
            {
                return _borderThicknessThickLeftKey ??
                       (_borderThicknessThickLeftKey =
                        Id.BorderThicknessThickLeftKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessThickRightKey;

        public static ResourceKey BorderThicknessThickRightKey
        {
            get
            {
                return _borderThicknessThickRightKey ??
                       (_borderThicknessThickRightKey =
                        Id.BorderThicknessThickRightKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessLargeTopKey;

        public static ResourceKey BorderThicknessThickTopKey
        {
            get
            {
                return _borderThicknessLargeTopKey ??
                       (_borderThicknessLargeTopKey =
                        Id.BorderThicknessLargeTopKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _borderThicknessThickBottomKey;

        public static ResourceKey BorderThicknessThickBottomKey
        {
            get
            {
                return _borderThicknessThickBottomKey ??
                       (_borderThicknessThickBottomKey =
                        Id.BorderThicknessThickBottomKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _strokeThicknessKey;

        public static ResourceKey StrokeThicknessKey
        {
            get
            {
                return _strokeThicknessKey ??
                       (_strokeThicknessKey =
                        Id.StrokeThicknessKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _strokeThicknessThinKey;

        public static ResourceKey StrokeThicknessThinKey
        {
            get
            {
                return _strokeThicknessThinKey ??
                       (_strokeThicknessThinKey =
                        Id.StrokeThicknessThinKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _strokeThicknessThickKey;

        public static ResourceKey StrokeThicknessThickKey
        {
            get
            {
                return _strokeThicknessThickKey ??
                       (_strokeThicknessThickKey =
                        Id.StrokeThicknessThickKeyId.GetResourceKey());
            }
        }

        private static ResourceKey _buttonPaddingKey;

        public static ResourceKey ButtonPaddingKey
        {
            get
            {
                return _buttonPaddingKey ??
                       (_buttonPaddingKey =
                        Id.ButtonPaddingKeyId.GetResourceKey());
            }
        }

        static HdcLayout()
        {
            Margin();
            MarginLarge();
            MarginExtraLarge();
            MarginExtraExtraLarge();

            Padding();
            PaddingLarge();
            PaddingExtraLarge();
            PaddingExtraExtraLarge();
            //
            ButtonSideLength();
            ButtonSideLengthWithBorder();
            ButtonSideLengthSmall();
            ButtonSideLengthSmallWithBorder();
            ButtonSideLengthTiny();
            ButtonSideLengthTinyWithBorder();
            ButtonSideLengthLarge();
            ButtonSideLengthLargeWithBorder();
        }

        private static void PaddingExtraExtraLarge()
        {
            _paddingExtraExtraLargeKey =
                Id.PaddingExtraExtraLargeKeyId.GetResourceKey();
            _paddingExtraExtraLargeLeftKey =
                Id.PaddingExtraExtraLargeLeftKeyId.GetResourceKey();
            _paddingExtraExtraLargeRightKey =
                Id.PaddingExtraExtraLargeRightKeyId.GetResourceKey();
            _paddingExtraExtraLargeTopKey =
                Id.PaddingExtraExtraLargeTopKeyId.GetResourceKey();
            _paddingExtraExtraLargeBottomKey =
                Id.PaddingExtraExtraLargeBottomKeyId.GetResourceKey();
            _paddingExtraExtraLargeVerticalKey =
                Id.PaddingExtraExtraLargeVerticalKeyId.GetResourceKey();
            _paddingExtraExtraLargeHorizontalKey =
                Id.PaddingExtraExtraLargeHorizontalKeyId.GetResourceKey();
            _paddingExtraExtraLargeLeftTopKey =
                Id.PaddingExtraExtraLargeLeftTopKeyId.GetResourceKey();
            _paddingExtraExtraLargeLeftBottomKey =
                Id.PaddingExtraExtraLargeLeftBottomKeyId.GetResourceKey();
            _paddingExtraExtraLargeRightTopKey =
                Id.PaddingExtraExtraLargeRightTopKeyId.GetResourceKey();
            _paddingExtraExtraLargeRightBottomKey =
                Id.PaddingExtraExtraLargeRightBottomKeyId.GetResourceKey();
        }

        private static void MarginExtraExtraLarge()
        {
            _marginExtraExtraLargeKey =
                Id.MarginExtraExtraLargeKeyId.GetResourceKey();
            _marginExtraExtraLargeLeftKey =
                Id.MarginExtraExtraLargeLeftKeyId.GetResourceKey();
            _marginExtraExtraLargeRightKey =
                Id.MarginExtraExtraLargeRightKeyId.GetResourceKey();
            _marginExtraExtraLargeTopKey =
                Id.MarginExtraExtraLargeTopKeyId.GetResourceKey();
            _marginExtraExtraLargeBottomKey =
                Id.MarginExtraExtraLargeBottomKeyId.GetResourceKey();
            _marginExtraExtraLargeVerticalKey =
                Id.MarginExtraExtraLargeVerticalKeyId.GetResourceKey();
            _marginExtraExtraLargeHorizontalKey =
                Id.MarginExtraExtraLargeHorizontalKeyId.GetResourceKey();
            _marginExtraExtraLargeLeftTopKey =
                Id.MarginExtraExtraLargeLeftTopKeyId.GetResourceKey();
            _marginExtraExtraLargeLeftBottomKey =
                Id.MarginExtraExtraLargeLeftBottomKeyId.GetResourceKey();
            _marginExtraExtraLargeRightTopKey =
                Id.MarginExtraExtraLargeRightTopKeyId.GetResourceKey();
            _marginExtraExtraLargeRightBottomKey =
                Id.MarginExtraExtraLargeRightBottomKeyId.GetResourceKey();
        }

        private static void MarginExtraLarge()
        {
            _marginExtraLargeKey =
                Id.MarginExtraLargeKeyId.GetResourceKey();
            _marginExtraLargeLeftKey =
                Id.MarginExtraLargeLeftKeyId.GetResourceKey();
            _marginExtraLargeRightKey =
                Id.MarginExtraLargeRightKeyId.GetResourceKey();
            _marginExtraLargeTopKey =
                Id.MarginExtraLargeTopKeyId.GetResourceKey();
            _marginExtraLargeBottomKey =
                Id.MarginExtraLargeBottomKeyId.GetResourceKey();
            _marginExtraLargeVerticalKey =
                Id.MarginExtraLargeVerticalKeyId.GetResourceKey();
            _marginExtraLargeHorizontalKey =
                Id.MarginExtraLargeHorizontalKeyId.GetResourceKey();
            _marginExtraLargeLeftTopKey =
                Id.MarginExtraLargeLeftTopKeyId.GetResourceKey();
            _marginExtraLargeLeftBottomKey =
                Id.MarginExtraLargeLeftBottomKeyId.GetResourceKey();
            _marginExtraLargeRightTopKey =
                Id.MarginExtraLargeRightTopKeyId.GetResourceKey();
            _marginExtraLargeRightBottomKey =
                Id.MarginExtraLargeRightBottomKeyId.GetResourceKey();
        }

        private static void PaddingExtraLarge()
        {
            _paddingExtraLargeKey =
                Id.PaddingExtraLargeKeyId.GetResourceKey();
            _paddingExtraLargeLeftKey =
                Id.PaddingExtraLargeLeftKeyId.GetResourceKey();
            _paddingExtraLargeRightKey =
                Id.PaddingExtraLargeRightKeyId.GetResourceKey();
            _paddingExtraLargeTopKey =
                Id.PaddingExtraLargeTopKeyId.GetResourceKey();
            _paddingExtraLargeBottomKey =
                Id.PaddingExtraLargeBottomKeyId.GetResourceKey();
            _paddingExtraLargeVerticalKey =
                Id.PaddingExtraLargeVerticalKeyId.GetResourceKey();
            _paddingExtraLargeHorizontalKey =
                Id.PaddingExtraLargeHorizontalKeyId.GetResourceKey();
            _paddingExtraLargeLeftTopKey =
                Id.PaddingExtraLargeLeftTopKeyId.GetResourceKey();
            _paddingExtraLargeLeftBottomKey =
                Id.PaddingExtraLargeLeftBottomKeyId.GetResourceKey();
            _paddingExtraLargeRightTopKey =
                Id.PaddingExtraLargeRightTopKeyId.GetResourceKey();
            _paddingExtraLargeRightBottomKey =
                Id.PaddingExtraLargeRightBottomKeyId.GetResourceKey();
        }

        private static void PaddingLarge()
        {
            _paddingLargeKey =
                Id.PaddingLargeKeyId.GetResourceKey();
            _paddingLargeLeftKey =
                Id.PaddingLargeLeftKeyId.GetResourceKey();
            _paddingLargeRightKey =
                Id.PaddingLargeRightKeyId.GetResourceKey();
            _paddingLargeTopKey =
                Id.PaddingLargeTopKeyId.GetResourceKey();
            _paddingLargeBottomKey =
                Id.PaddingLargeBottomKeyId.GetResourceKey();
            _paddingLargeVerticalKey =
                Id.PaddingLargeVerticalKeyId.GetResourceKey();
            _paddingLargeHorizontalKey =
                Id.PaddingLargeHorizontalKeyId.GetResourceKey();
            _paddingLargeLeftTopKey =
                Id.PaddingLargeLeftTopKeyId.GetResourceKey();
            _paddingLargeLeftBottomKey =
                Id.PaddingLargeLeftBottomKeyId.GetResourceKey();
            _paddingLargeRightTopKey =
                Id.PaddingLargeRightTopKeyId.GetResourceKey();
            _paddingLargeRightBottomKey =
                Id.PaddingLargeRightBottomKeyId.GetResourceKey();
        }

        private static void Padding()
        {
            _paddingKey =
                Id.PaddingKeyId.GetResourceKey();
            _paddingLeftKey =
                Id.PaddingLeftKeyId.GetResourceKey();
            _paddingRightKey =
                Id.PaddingRightKeyId.GetResourceKey();
            _paddingTopKey =
                Id.PaddingTopKeyId.GetResourceKey();
            _paddingBottomKey =
                Id.PaddingBottomKeyId.GetResourceKey();
            _paddingVerticalKey =
                Id.PaddingVerticalKeyId.GetResourceKey();
            _paddingHorizontalKey =
                Id.PaddingHorizontalKeyId.GetResourceKey();
            _paddingLeftTopKey =
                Id.PaddingLeftTopKeyId.GetResourceKey();
            _paddingLeftBottomKey =
                Id.PaddingLeftBottomKeyId.GetResourceKey();
            _paddingRightTopKey =
                Id.PaddingRightTopKeyId.GetResourceKey();
            _paddingRightBottomKey =
                Id.PaddingRightBottomKeyId.GetResourceKey();
        }

        private static void Margin()
        {
            _marginKey =
                Id.MarginResourceKeyId.GetResourceKey();
            _marginLeftKey =
                Id.MarginLeftKeyId.GetResourceKey();
            _marginRightKey =
                Id.MarginRightKeyId.GetResourceKey();
            _marginTopKey =
                Id.MarginTopKeyId.GetResourceKey();
            _marginBottomKey =
                Id.MarginBottomKeyId.GetResourceKey();
            _marginVerticalKey =
                Id.MarginVerticalKeyId.GetResourceKey();
            _marginHorizontalKey =
                Id.MarginHorizontalKeyId.GetResourceKey();
            _marginLeftTopKey =
                Id.MarginLeftTopKeyId.GetResourceKey();
            _marginLeftBottomKey =
                Id.MarginLeftBottomKeyId.GetResourceKey();
            _marginRightTopKey =
                Id.MarginRightTopKeyId.GetResourceKey();
            _marginRightBottomKey =
                Id.MarginRightBottomKeyId.GetResourceKey();
        }

        private static void MarginLarge()
        {
            _marginLargeKey =
                Id.MarginLargeKeyId.GetResourceKey();
            _marginLargeLeftKey =
                Id.MarginLargeLeftKeyId.GetResourceKey();
            _marginLargeRightKey =
                Id.MarginLargeRightKeyId.GetResourceKey();
            _marginLargeTopKey =
                Id.MarginLargeTopKeyId.GetResourceKey();
            _marginLargeBottomKey =
                Id.MarginLargeBottomKeyId.GetResourceKey();
            _marginLargeVerticalKey =
                Id.MarginLargeVerticalKeyId.GetResourceKey();
            _marginLargeHorizontalKey =
                Id.MarginLargeHorizontalKeyId.GetResourceKey();
            _marginLargeLeftTopKey =
                Id.MarginLargeLeftTopKeyId.GetResourceKey();
            _marginLargeLeftBottomKey =
                Id.MarginLargeLeftBottomKeyId.GetResourceKey();
            _marginLargeRightTopKey =
                Id.MarginLargeRightTopKeyId.GetResourceKey();
            _marginLargeRightBottomKey =
                Id.MarginLargeRightBottomKeyId.GetResourceKey();
        }

        private static void ButtonSideLengthLarge()
        {
            _buttonSideLengthLargeKey =
                Id.ButtonSideLengthLargeKeyId.GetResourceKey();
            _buttonSideLengthLargeM1Key =
                Id.ButtonSideLengthLargeM1KeyId.GetResourceKey();
            _buttonSideLengthLargeM2Key =
                Id.ButtonSideLengthLargeM2KeyId.GetResourceKey();
            _buttonSideLengthLargeM3Key =
                Id.ButtonSideLengthLargeM3KeyId.GetResourceKey();
            _buttonSideLengthLargeM4Key =
                Id.ButtonSideLengthLargeM4KeyId.GetResourceKey();
            _buttonSideLengthLargeM5Key =
                Id.ButtonSideLengthLargeM5KeyId.GetResourceKey();
            _buttonSideLengthLargeM6Key =
                Id.ButtonSideLengthLargeM6KeyId.GetResourceKey();
        }

        private static void ButtonSideLengthTinyWithBorder()
        {
            _buttonSideLengthTinyWithBorderKey =
                Id.ButtonSideLengthTinyWithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM1WithBorderKey =
                Id.ButtonSideLengthTinyM1WithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM2WithBorderKey =
                Id.ButtonSideLengthTinyM2WithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM3WithBorderKey =
                Id.ButtonSideLengthTinyM3WithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM4WithBorderKey =
                Id.ButtonSideLengthTinyM4WithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM5WithBorderKey =
                Id.ButtonSideLengthTinyM5WithBorderKeyId.GetResourceKey();
            _buttonSideLengthTinyM6WithBorderKey =
                Id.ButtonSideLengthTinyM6WithBorderKeyId.GetResourceKey();
        }

        private static void ButtonSideLengthTiny()
        {
            _buttonSideLengthTinyKey =
                Id.ButtonSideLengthTinyKeyId.GetResourceKey();
            _buttonSideLengthTinyM1Key =
                Id.ButtonSideLengthTinyM1KeyId.GetResourceKey();
            _buttonSideLengthTinyM2Key =
                Id.ButtonSideLengthTinyM2KeyId.GetResourceKey();
            _buttonSideLengthTinyM3Key =
                Id.ButtonSideLengthTinyM3KeyId.GetResourceKey();
            _buttonSideLengthTinyM4Key =
                Id.ButtonSideLengthTinyM4KeyId.GetResourceKey();
            _buttonSideLengthTinyM5Key =
                Id.ButtonSideLengthTinyM5KeyId.GetResourceKey();
            _buttonSideLengthTinyM6Key =
                Id.ButtonSideLengthTinyM6KeyId.GetResourceKey();
        }

        private static void ButtonSideLengthSmallWithBorder()
        {
            _buttonSideLengthSmallWithBorderKey =
                Id.ButtonSideLengthSmallWithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM1WithBorderKey =
                Id.ButtonSideLengthSmallM1WithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM2WithBorderKey =
                Id.ButtonSideLengthSmallM2WithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM3WithBorderKey =
                Id.ButtonSideLengthSmallM3WithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM4WithBorderKey =
                Id.ButtonSideLengthSmallM4WithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM5WithBorderKey =
                Id.ButtonSideLengthSmallM5WithBorderKeyId.GetResourceKey();
            _buttonSideLengthSmallM6WithBorderKey =
                Id.ButtonSideLengthSmallM6WithBorderKeyId.GetResourceKey();
        }

        private static void ButtonSideLengthSmall()
        {
            _buttonSideLengthSmallKey =
                Id.ButtonSideLengthSmallKeyId.GetResourceKey();
            _buttonSideLengthSmallM1Key =
                Id.ButtonSideLengthSmallM1KeyId.GetResourceKey();
            _buttonSideLengthSmallM2Key =
                Id.ButtonSideLengthSmallM2KeyId.GetResourceKey();
            _buttonSideLengthSmallM3Key =
                Id.ButtonSideLengthSmallM3KeyId.GetResourceKey();
            _buttonSideLengthSmallM4Key =
                Id.ButtonSideLengthSmallM4KeyId.GetResourceKey();
            _buttonSideLengthSmallM5Key =
                Id.ButtonSideLengthSmallM5KeyId.GetResourceKey();
            _buttonSideLengthSmallM6Key =
                Id.ButtonSideLengthSmallM6KeyId.GetResourceKey();
        }

        private static void ButtonSideLengthLargeWithBorder()
        {
            _buttonSideLengthLargeWithBorderKey =
                Id.ButtonSideLengthLargeWithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM1WithBorderKey =
                Id.ButtonSideLengthLargeM1WithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM2WithBorderKey =
                Id.ButtonSideLengthLargeM2WithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM3WithBorderKey =
                Id.ButtonSideLengthLargeM3WithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM4WithBorderKey =
                Id.ButtonSideLengthLargeM4WithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM5WithBorderKey =
                Id.ButtonSideLengthLargeM5WithBorderKeyId.GetResourceKey();
            _buttonSideLengthLargeM6WithBorderKey =
                Id.ButtonSideLengthLargeM6WithBorderKeyId.GetResourceKey();
        }

        private static void ButtonSideLengthWithBorder()
        {
            _buttonSideLengthWithBorderKey =
                Id.ButtonSideLengthWithBorderKeyId.GetResourceKey();
            _buttonSideLengthM1WithBorderKey =
                Id.ButtonSideLengthM1WithBorderKeyId.GetResourceKey();
            _buttonSideLengthM2WithBorderKey =
                Id.ButtonSideLengthM2WithBorderKeyId.GetResourceKey();
            _buttonSideLengthM3WithBorderKey =
                Id.ButtonSideLengthM3WithBorderKeyId.GetResourceKey();
            _buttonSideLengthM4WithBorderKey =
                Id.ButtonSideLengthM4WithBorderKeyId.GetResourceKey();
            _buttonSideLengthM5WithBorderKey =
                Id.ButtonSideLengthM5WithBorderKeyId.GetResourceKey();
            _buttonSideLengthM6WithBorderKey =
                Id.ButtonSideLengthM6WithBorderKeyId.GetResourceKey();
        }

        private static void ButtonSideLength()
        {
            _buttonSideLengthKey =
                Id.ButtonSideLengthResourceKeyId.GetResourceKey();
            _buttonSideLengthM1Key =
                Id.ButtonSideLengthM1KeyId.GetResourceKey();
            _buttonSideLengthM2Key =
                Id.ButtonSideLengthM2KeyId.GetResourceKey();
            _buttonSideLengthM3Key =
                Id.ButtonSideLengthM3KeyId.GetResourceKey();
            _buttonSideLengthM4Key =
                Id.ButtonSideLengthM4KeyId.GetResourceKey();
            _buttonSideLengthM5Key =
                Id.ButtonSideLengthM5KeyId.GetResourceKey();
            _buttonSideLengthM6Key =
                Id.ButtonSideLengthM6KeyId.GetResourceKey();
        }


        private enum Id
        {
            //ButtonSideLength
            ButtonSideLengthResourceKeyId,
            ButtonSideLengthM1KeyId,
            ButtonSideLengthM2KeyId,
            ButtonSideLengthM3KeyId,
            ButtonSideLengthM4KeyId,
            ButtonSideLengthM5KeyId,
            ButtonSideLengthM6KeyId,
            //ButtonSideLengthWithBorder
            ButtonSideLengthWithBorderKeyId,
            ButtonSideLengthM1WithBorderKeyId,
            ButtonSideLengthM2WithBorderKeyId,
            ButtonSideLengthM3WithBorderKeyId,
            ButtonSideLengthM4WithBorderKeyId,
            ButtonSideLengthM5WithBorderKeyId,
            ButtonSideLengthM6WithBorderKeyId,
            //ButtonSideLengthTiny
            ButtonSideLengthTinyKeyId,
            ButtonSideLengthTinyM1KeyId,
            ButtonSideLengthTinyM2KeyId,
            ButtonSideLengthTinyM3KeyId,
            ButtonSideLengthTinyM4KeyId,
            ButtonSideLengthTinyM5KeyId,
            ButtonSideLengthTinyM6KeyId,
            //ButtonSideLengthTinyWithBorder
            ButtonSideLengthTinyWithBorderKeyId,
            ButtonSideLengthTinyM1WithBorderKeyId,
            ButtonSideLengthTinyM2WithBorderKeyId,
            ButtonSideLengthTinyM3WithBorderKeyId,
            ButtonSideLengthTinyM4WithBorderKeyId,
            ButtonSideLengthTinyM5WithBorderKeyId,
            ButtonSideLengthTinyM6WithBorderKeyId,
            //ButtonSideLengthSmall
            ButtonSideLengthSmallKeyId,
            ButtonSideLengthSmallM1KeyId,
            ButtonSideLengthSmallM2KeyId,
            ButtonSideLengthSmallM3KeyId,
            ButtonSideLengthSmallM4KeyId,
            ButtonSideLengthSmallM5KeyId,
            ButtonSideLengthSmallM6KeyId,
            //ButtonSideLengthSmallWithBorder
            ButtonSideLengthSmallWithBorderKeyId,
            ButtonSideLengthSmallM1WithBorderKeyId,
            ButtonSideLengthSmallM2WithBorderKeyId,
            ButtonSideLengthSmallM3WithBorderKeyId,
            ButtonSideLengthSmallM4WithBorderKeyId,
            ButtonSideLengthSmallM5WithBorderKeyId,
            ButtonSideLengthSmallM6WithBorderKeyId,
            //ButtonSideLengthLarge
            ButtonSideLengthLargeKeyId,
            ButtonSideLengthLargeM1KeyId,
            ButtonSideLengthLargeM2KeyId,
            ButtonSideLengthLargeM3KeyId,
            ButtonSideLengthLargeM4KeyId,
            ButtonSideLengthLargeM5KeyId,
            ButtonSideLengthLargeM6KeyId,
            //ButtonSideLengthLargeWithBorder
            ButtonSideLengthLargeWithBorderKeyId,
            ButtonSideLengthLargeM1WithBorderKeyId,
            ButtonSideLengthLargeM2WithBorderKeyId,
            ButtonSideLengthLargeM3WithBorderKeyId,
            ButtonSideLengthLargeM4WithBorderKeyId,
            ButtonSideLengthLargeM5WithBorderKeyId,
            ButtonSideLengthLargeM6WithBorderKeyId,
            //Margin
            MarginResourceKeyId,
            MarginLeftKeyId,
            MarginRightKeyId,
            MarginTopKeyId,
            MarginBottomKeyId,
            MarginVerticalKeyId,
            MarginHorizontalKeyId,
            MarginLeftTopKeyId,
            MarginLeftBottomKeyId,
            MarginRightTopKeyId,
            MarginRightBottomKeyId,
            //MarginLarge
            MarginLargeKeyId,
            MarginLargeLeftKeyId,
            MarginLargeRightKeyId,
            MarginLargeTopKeyId,
            MarginLargeBottomKeyId,
            MarginLargeVerticalKeyId,
            MarginLargeHorizontalKeyId,
            MarginLargeLeftTopKeyId,
            MarginLargeLeftBottomKeyId,
            MarginLargeRightTopKeyId,
            MarginLargeRightBottomKeyId,
            //MarginExtraLarge
            MarginExtraLargeKeyId,
            MarginExtraLargeLeftKeyId,
            MarginExtraLargeRightKeyId,
            MarginExtraLargeTopKeyId,
            MarginExtraLargeBottomKeyId,
            MarginExtraLargeVerticalKeyId,
            MarginExtraLargeHorizontalKeyId,
            MarginExtraLargeLeftTopKeyId,
            MarginExtraLargeLeftBottomKeyId,
            MarginExtraLargeRightTopKeyId,
            MarginExtraLargeRightBottomKeyId,
            //MarginExtraExtraLarge
            MarginExtraExtraLargeKeyId,
            MarginExtraExtraLargeLeftKeyId,
            MarginExtraExtraLargeRightKeyId,
            MarginExtraExtraLargeTopKeyId,
            MarginExtraExtraLargeBottomKeyId,
            MarginExtraExtraLargeVerticalKeyId,
            MarginExtraExtraLargeHorizontalKeyId,
            MarginExtraExtraLargeLeftTopKeyId,
            MarginExtraExtraLargeLeftBottomKeyId,
            MarginExtraExtraLargeRightTopKeyId,
            MarginExtraExtraLargeRightBottomKeyId,
            //Padding
            PaddingKeyId,
            PaddingLeftKeyId,
            PaddingRightKeyId,
            PaddingTopKeyId,
            PaddingBottomKeyId,
            PaddingVerticalKeyId,
            PaddingHorizontalKeyId,
            PaddingLeftTopKeyId,
            PaddingLeftBottomKeyId,
            PaddingRightTopKeyId,
            PaddingRightBottomKeyId,
            //PaddingLarge
            PaddingLargeKeyId,
            PaddingLargeLeftKeyId,
            PaddingLargeRightKeyId,
            PaddingLargeTopKeyId,
            PaddingLargeBottomKeyId,
            PaddingLargeVerticalKeyId,
            PaddingLargeHorizontalKeyId,
            PaddingLargeLeftTopKeyId,
            PaddingLargeLeftBottomKeyId,
            PaddingLargeRightTopKeyId,
            PaddingLargeRightBottomKeyId,
            //PaddingExtraLarge
            PaddingExtraLargeKeyId,
            PaddingExtraLargeLeftKeyId,
            PaddingExtraLargeRightKeyId,
            PaddingExtraLargeTopKeyId,
            PaddingExtraLargeBottomKeyId,
            PaddingExtraLargeVerticalKeyId,
            PaddingExtraLargeHorizontalKeyId,
            PaddingExtraLargeLeftTopKeyId,
            PaddingExtraLargeLeftBottomKeyId,
            PaddingExtraLargeRightTopKeyId,
            PaddingExtraLargeRightBottomKeyId,
            //PaddingExtraExtraLarge
            PaddingExtraExtraLargeKeyId,
            PaddingExtraExtraLargeLeftKeyId,
            PaddingExtraExtraLargeRightKeyId,
            PaddingExtraExtraLargeTopKeyId,
            PaddingExtraExtraLargeBottomKeyId,
            PaddingExtraExtraLargeVerticalKeyId,
            PaddingExtraExtraLargeHorizontalKeyId,
            PaddingExtraExtraLargeLeftTopKeyId,
            PaddingExtraExtraLargeLeftBottomKeyId,
            PaddingExtraExtraLargeRightTopKeyId,
            PaddingExtraExtraLargeRightBottomKeyId,
            //BorderThickness
            BorderThicknessKeyId,
            BorderThicknessLeftKeyId,
            BorderThicknessRightKeyId,
            BorderThicknessTopKeyId,
            BorderThicknessBottomKeyId,
            //BorderThicknessLarge
            BorderThicknessThickKeyId,
            BorderThicknessThickLeftKeyId,
            BorderThicknessThickRightKeyId,
            BorderThicknessLargeTopKeyId,
            BorderThicknessThickBottomKeyId,
            ButtonBorderThicknessKeyId,
            //StrokeThickness
            StrokeThicknessKeyId,
            StrokeThicknessThinKeyId,
            StrokeThicknessThickKeyId,
            // CornerRadius
            ButtonCornerRadiusKeyId,
            BorderCornerRadiusKeyId,
            // Padding
            ButtonPaddingKeyId,
        }
    }
}