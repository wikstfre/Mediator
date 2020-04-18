using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Interface IMessageBehavior
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMessageBehavior<in TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Message behavior handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
        /// </summary>
        /// <param name="message">Incoming message</param>
        /// <param name="context">The context.</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns></returns>
        Task Handle(TMessage message, IMessageContext context, PipelineDelegate next);
    }

    /// <summary>
    /// Interface IMessageBehavior
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IMessageBehavior<in TMessage, TResponse> where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Request behavior handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
        /// </summary>
        /// <param name="message">Incoming message</param>
        /// <param name="context">The context.</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns></returns>
        Task<TResponse> Handle(TMessage message, IMessageContext context, PipelineDelegate<TResponse> next);
    }
}
