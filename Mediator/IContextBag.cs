using System.Collections.Generic;

namespace Mediator
{
    /// <summary>
    /// Interface IContextBag
    /// </summary>
    public interface IContextBag : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Gets value by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();

        /// <summary>
        /// Gets value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Sets the specified value by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        IContextBag Set<T>(T value);

        /// <summary>
        /// Sets the specified value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        IContextBag Set<T>(string key, T value);

        /// <summary>
        /// Merges the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IContextBag Merge(IContextBag context);
    }
}
