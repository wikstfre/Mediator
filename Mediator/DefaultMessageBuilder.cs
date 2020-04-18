using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator
{
    /// <summary>
    /// Class DefaultMessageBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class DefaultMessageBuilder<TMessage> : IMessageBuilder<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessageBuilder{TMessage}"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="name">The name.</param>
        public DefaultMessageBuilder(IServiceCollection services, string name)
        {
            Services = services;
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Adds the default message handler.
        /// </summary>
        /// <returns></returns>
        public IMessageHandlerBuilder<TMessage> AddDefaultMessageHandler()
        {
            return new DefaultMessageHandlerBuilder<TMessage>(Services, Name);
        }

        /// <summary>
        /// Adds the message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the message handler.</typeparam>
        /// <returns></returns>
        public IMessageHandlerBuilder<TMessage> AddMessageHandler<TMessageHandler>() where TMessageHandler : class, IMessageHandler<TMessage>
        {
            Services
                .TryAddTransient<IMessageHandler<TMessage>, TMessageHandler>();

            return new DefaultMessageHandlerBuilder<TMessage>(Services, Name);
        }
    }

    /// <summary>
    /// Class DefaultMessageBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class DefaultMessageBuilder<TMessage, TResponse> : IMessageBuilder<TMessage, TResponse>
        where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessageBuilder{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="name">The name.</param>
        public DefaultMessageBuilder(IServiceCollection services, string name)
        {
            Services = services;
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Adds the message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the message handler.</typeparam>
        /// <returns></returns>
        public IMessageHandlerBuilder<TMessage, TResponse> AddMessageHandler<TMessageHandler>()
            where TMessageHandler : class, IMessageHandler<TMessage, TResponse>
        {
            Services
                .TryAddTransient<IMessageHandler<TMessage, TResponse>, TMessageHandler>();

            return new DefaultMessageHandlerBuilder<TMessage, TResponse>(Services, Name);
        }
    }
}
