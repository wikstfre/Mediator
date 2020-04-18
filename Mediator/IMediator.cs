using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Interface IMediator
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task Send<TMessage>(TMessage message, ExtendableOptions options, CancellationToken cancellationToken = default) where TMessage : IMessage;

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<TResponse> Send<TResponse>(IMessage<TResponse> message, ExtendableOptions options, CancellationToken cancellationToken = default);
    }
}
