

namespace Hdc.Patterns
{
    using System;
    using System.Collections.Generic;

    public class Factory<TKey, T>
    {
        /// <summary>
        /// Dictionary of creators for a specific key.
        /// </summary>
        public static IDictionary<TKey, Func<T>> _creators = new Dictionary<TKey, Func<T>>();
        public static Func<T> _defaultCreator;
        public static T _default;


        /// <summary>
        /// Register a key to implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="result"></param>
        public static void Register(TKey key, T result)
        {
            _creators[key] = new Func<T>(() => result);
        }        


        /// <summary>
        /// Registers the default implementation.
        /// </summary>
        /// <param name="result"></param>
        public static void Register(TKey key, Func<T> creator)
        {
            _creators[key] = creator;
        }


        /// <summary>
        /// Registers the default implementation.
        /// </summary>
        /// <param name="result"></param>
        public static void RegisterDefault(T result)
        {
            _default = result;
            _defaultCreator = new Func<T>( () => _default);
        }
        
        
        /// <summary>
        /// Register default implementation using creator func provided.
        /// </summary>
        /// <param name="creator"></param>
        public static void RegisterDefault(Func<T> creator)
        {
            _defaultCreator = creator;
        }


        /// <summary>
        /// Create an instance of type T using the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Create(TKey key)
        {
            if (!_creators.ContainsKey(key))
                return default(T);

            return _creators[key]();
        }


        /// <summary>
        /// Create default instance.
        /// </summary>
        /// <returns></returns>
        public static T Create()
        {
            return _defaultCreator();
        }
    }
}