using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Prism;

namespace Hdc.Prism.Regions
{
    public class NavigationUriBuilder : IEnumerable<KeyValuePair<string, string>>
    {
        private string _viewName;
        private readonly List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationParameters"/> class.
        /// </summary>
        public NavigationUriBuilder(string viewName)
        {
            _viewName = viewName;
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value>The value for the specified key, or <see langword="null"/> if the query does not contain such a key.</value>
        public object this[string key]
        {
            get
            {
                foreach (var kvp in this.entries)
                {
                    if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                    {
                        return kvp.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, string value)
        {
            this.entries.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Uri GetUri()
        {
            var uriQuery = new NavigationParameters();

            foreach (var parameter in entries)
            {
                uriQuery.Add(parameter.Key, parameter.Value);
            }

            var uri = new Uri(_viewName + uriQuery, UriKind.Relative);
            return uri;
        }
    }
}