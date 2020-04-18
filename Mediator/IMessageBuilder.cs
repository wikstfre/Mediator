using Microsoft.Extensions.DependencyInjection;

namespace Mediator
{
    /// <summary>
    /// Interface IMessageBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMessageBuilder<out TMessage> where TMessage : IMessage
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
        /// Adds the default message handler.
        /// </summary>
        /// <returns></returns>
        IMessageHandlerBuilder<TMessage> AddDefaultMessageHandler();

        /// <summary>
        /// Adds the message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the message handler.</typeparam>
        /// <returns></returns>
        IMessageHandlerBuilder<TMessage> AddMessageHandler<TMessageHandler>()
            where TMessageHandler : class, IMessageHandler<TMessage>;
    }

    /// <summary>
    /// Interface IMessageBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IMessageBuilder<out TMessage, TResponse> where TMessage : IMessage<TResponse>
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
        /// Adds the message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the message handler.</typeparam>
        /// <returns></returns>
        IMessageHandlerBuilder<TMessage, TResponse> AddMessageHandler<TMessageHandler>()
            where TMessageHandler : class, IMessageHandler<TMessage, TResponse>;
    }
}
