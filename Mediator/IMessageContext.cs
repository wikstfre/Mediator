using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Interface IMessageContext
    /// </summary>
    public interface IMessageContext
    {
        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        IContextBag Extensions { get; }

        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task Send<TMessage>(TMessage message) where TMessage : IMessage;

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<TResponse> Send<TResponse>(IMessage<TResponse> message);
    }
}
