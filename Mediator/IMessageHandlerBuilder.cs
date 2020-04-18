using Microsoft.Extensions.DependencyInjection;

namespace Mediator
{
    /// <summary>
    /// Interface IMessageHandlerBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMessageHandlerBuilder<out TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }

        /// <summary>
        /// Adds the message behavior.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <returns></returns>
        IMessageHandlerBuilder<TMessage> AddMessageBehavior<TBehavior>()
            where TBehavior : IMessageBehavior<TMessage>;
    }

    /// <summary>
    /// Interface IMessageHandlerBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IMessageHandlerBuilder<out TMessage, TResponse> where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }

        /// <summary>
        /// Adds the message behavior.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <returns></returns>
        IMessageHandlerBuilder<TMessage, TResponse> AddMessageBehavior<TBehavior>()
            where TBehavior : IMessageBehavior<TMessage, TResponse>;
    }
}
