using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Interface IMessageHandler
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMessageHandler<in TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task Handle(TMessage message, IMessageContext context);
    }

    /// <summary>
    /// Interface IMessageHandler
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IMessageHandler<in TMessage, TResponse> where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task<TResponse> Handle(TMessage message, IMessageContext context);
    }
}
