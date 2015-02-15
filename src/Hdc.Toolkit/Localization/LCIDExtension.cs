using System.Globalization;

namespace Hdc.Localization
{
    public static class LCIDExtension
    {
        public static LCID ToLCID(this string str)
        {
            switch (str)
            {
                case LCIDStrings.enUS:
                    return LCID.enUS;

                case LCIDStrings.zhCHS:
                    return LCID.zhCHS;
                default:
                    return LCID.enUS;
            }
        }

        public static string ToLCIDString(this LCID lcid)
        {
            switch (lcid)
            {
                case LCID.enUS:
                    return LCIDStrings.enUS;
                case LCID.zhCHS:
                    return LCIDStrings.zhCHS;
                default:
                    return LCIDStrings.enUS;
            }
        }

        public static CultureInfo ToCultureInfo(this LCID lcid)
        {
            string lcidString = lcid.ToLCIDString();
            return new CultureInfo(lcidString);
        }

        public static CultureInfo ToCultureInfo(this string cultureName)
        {
            return new CultureInfo(cultureName);
        }
    }
}