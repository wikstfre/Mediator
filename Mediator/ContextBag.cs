using System.Collections;
using System.Collections.Generic;

namespace Mediator
{
    /// <summary>
    /// Class ContextBag
    /// </summary>
    internal class ContextBag : IContextBag
    {
        /// <summary>
        /// The stash
        /// </summary>
        private readonly IDictionary<string, object> _stash = new Dictionary<string, object>();

        /// <summary>
        /// Gets value by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return Get<T>(typeof(T).FullName);
        }

        /// <summary>
        /// Gets value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return (T)_stash[key];
        }

        /// <summary>
        /// Sets the specified value by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        public IContextBag Set<T>(T value)
        {
            Set(typeof(T).FullName, value);

            return this;
        }

        /// <summary>
        /// Sets the specified value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public IContextBag Set<T>(string key, T value)
        {
            _stash[key] = value;

            return this;
        }

        /// <summary>
        /// Merges the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IContextBag Merge(IContextBag context)
        {
            foreach (var extension in context)
            {
                Set(extension);
            }

            return this;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _stash.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _stash.GetEnumerator();
        }
    }
}
