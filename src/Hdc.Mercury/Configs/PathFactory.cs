using System.ComponentModel.Composition;
using System.Globalization;

namespace Hdc.Mercury.Configs
{
    [Export(typeof (IPathFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PathFactory : IPathFactory
    {
        public string Create(string path, int tagIndex, int tagIndexLength)
        {
            var tagIndexString = GetTagIndexString(tagIndex, tagIndexLength);

            string tagString = "/" + path + tagIndexString + "/";
            
            return tagString;
        }

        public string Create(string path, int tagIndex, int tagIndexLength, int generationIndex)
        {
//            if (generationIndex <= 0)
//            {
//                return Create(path, tagIndex, tagIndexLength);
//            }

            var injectGenerationIndex = path.Replace("@", generationIndex.ToString(CultureInfo.InvariantCulture));
            var generationIndexInjected = injectGenerationIndex;
            return Create(generationIndexInjected, tagIndex, tagIndexLength);
        }

        private static string GetTagIndexString(int tagIndex, int tagIndexLength)
        {
            string tagIndexString = string.Empty;
            if (tagIndexLength > 0)
            {
                var formatString = "{0:D" + tagIndexLength + "}";
                tagIndexString = string.Format(formatString, tagIndex);
            }
            return tagIndexString;
        }

        public string CreateArrayPath(string path, int tagIndex)
        {
            string tagString = "/" + path + "[" + tagIndex + "]" + "/";

            return tagString;
        }

        public string CreateArrayPath(string path, int tagIndex, int generationIndex)
        {
//            if (generationIndex <= 0)
//            {
//                return CreateArrayPath(path, tagIndex);
//            }

            var injectGenerationIndex = path.Replace("@", "[" + generationIndex +"]");
            var generationIndexInjected = injectGenerationIndex;
            return CreateArrayPath(generationIndexInjected, tagIndex);
        }
    }
}