using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator
{
    /// <summary>
    /// Class ServiceCollectionExtentions
    /// </summary>
    public static class ServiceCollectionExtentions
    {
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IMessageBuilder<TMessage> AddMessage<TMessage>(this IServiceCollection services)
            where TMessage : IMessage
        {
            AddMediator(services);

            var messageName = typeof(TMessage).Name;

            return new DefaultMessageBuilder<TMessage>(services, messageName);
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IMessageBuilder<TMessage, TResponse> AddMessage<TMessage, TResponse>(this IServiceCollection services)
            where TMessage : IMessage<TResponse>
        {
            AddMediator(services);

            var messageName = typeof(TMessage).Name;

            return new DefaultMessageBuilder<TMessage, TResponse>(services, messageName);
        }

        /// <summary>
        /// Adds the mediator.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddMediator(this IServiceCollection services)
        {
            services
                .AddOptions();

            services
                .TryAddScoped<IMediator, DefaultMediator>();
        }
    }
}
