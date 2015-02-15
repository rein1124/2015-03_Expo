using System;
using System.Collections.Generic;
using System.Linq;
using Hdc.Linq;

namespace Hdc
{
    public static class StringExtensions2
    {
        public static string CreateNextOrderedName(this IEnumerable<string> names, string defaultName)
        {
            var indexs = names.Select(
                x =>
                    {
                        if (x.IsNullOrEmpty()) return 0;

                        if (!x.StartsWith(defaultName)) return 0;

                        var retain = x.Substring(defaultName.Length,
                                                 x.Length -
                                                 defaultName.Length);
                        int index;
                        var can = Int32.TryParse(retain, out index);
                        if (can)
                        {
                            return index;
                        }
                        else
                        {
                            return 0;
                        }
                    });
            int newIndex;
            if (indexs.IsEmpty())
            {
                newIndex = 1;
            }
            else
            {
                newIndex = indexs.Max() + 1;
            }
            var newName = defaultName + newIndex;
            return newName;
        }

        public static string CreateNextOrderedName(this string newName,
                                                   Func<string, bool> checkIsNameExistFunc,
                                                   string suffix)
        {
            if (!checkIsNameExistFunc(newName))
            {
                return newName;
            }

            int counter = 0;
            bool isExist = true;
            string nextName = string.Empty;
            while (isExist)
            {
                counter++;
                nextName = newName;
                nextName += (" " + suffix);
                nextName += (" (" + counter + ")");
                isExist = checkIsNameExistFunc(nextName);
            }

            return nextName;
        }
    }
}