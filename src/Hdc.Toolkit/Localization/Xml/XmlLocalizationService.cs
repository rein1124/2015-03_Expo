using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hdc.Localization.Xml
{
    public class XmlLocalizationService : ILocalizationService
    {
        private string _fileName;

        private IDictionary<string, string> _entries;

        public void Update(string LCIDString)
        {
            _entries = new Dictionary<string, string>();

            var entries = LoadAllEntries(LCIDString);

            foreach (var entry in entries)
            {
                _entries.Add(entry.Original, entry.Translation);
            }
        }

        public string TranslateFrom(string name)
        {
            string translation;
            var ok = _entries.TryGetValue(name, out translation);
            return ok
                       ? translation
                       : name;
        }

        private IEnumerable<LanguageEntry> LoadAllEntries(string LCIDString)
        {
            _fileName = @"language\" + LCIDString + ".xml";

            if (!File.Exists(_fileName))
            {
                return new List<LanguageEntry>();
                throw new FileNotFoundException(_fileName + " cannot be found!");
            }

            var xgc = XLanguage.LoadFromFile(_fileName);
            return xgc.Entries.Select(entry => new LanguageEntry()
                                                   {
                                                       Original = entry.Original,
                                                       Translation = entry.Translation
                                                   });
        }
    }
}