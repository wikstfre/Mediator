using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator
{
    /// <summary>
    /// Class DefaultMessageHandlerBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class DefaultMessageHandlerBuilder<TMessage> : IMessageHandlerBuilder<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessageHandlerBuilder{TMessage}"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="name">The name.</param>
        public DefaultMessageHandlerBuilder(IServiceCollection services, string name)
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
        /// Adds the message behavior.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <returns></returns>
        public IMessageHandlerBuilder<TMessage> AddMessageBehavior<TBehavior>()
            where TBehavior : IMessageBehavior<TMessage>
        {
            var behaviorType = typeof(TBehavior);

            Services.TryAddTransient(behaviorType);
            Services.Configure<MessageConfiguration>(Name, _ => _.Behaviors.Add(behaviorType));

            return this;
        }
    }

    /// <summary>
    /// Class DefaultMessageHandlerBuilder
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class DefaultMessageHandlerBuilder<TMessage, TResponse> : IMessageHandlerBuilder<TMessage, TResponse>
        where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessageHandlerBuilder{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="name">The name.</param>
        public DefaultMessageHandlerBuilder(IServiceCollection services, string name)
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
        /// Adds the message behavior.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <returns></returns>
        public IMessageHandlerBuilder<TMessage, TResponse> AddMessageBehavior<TBehavior>()
            where TBehavior : IMessageBehavior<TMessage, TResponse>
        {
            var behaviorType = typeof(TBehavior);

            Services.TryAddTransient(behaviorType);
            Services.Configure<MessageConfiguration>(Name, _ => _.Behaviors.Add(behaviorType));

            return this;
        }
    }
}
