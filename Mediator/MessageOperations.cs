using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Class MessageOperations
    /// </summary>
    internal static class MessageOperations
    {
        /// <summary>
        /// The pipeline cache
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> PipelineCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static async Task Send<TMessage>(
            TMessage message,
            IMessageContext context,
            IServiceProvider provider,
            MessageConfiguration configuration) where TMessage : IMessage
        {
            var messageType = message.GetType();

            var pipeline = (Pipeline)PipelineCache
                .GetOrAdd(messageType, _ => Activator.CreateInstance(typeof(DefaultPipeline<>).MakeGenericType(_)));

            await pipeline
                .Invoke(
                    message,
                    context,
                    provider,
                    configuration)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static async Task<TResponse> Send<TResponse>(
            IMessage<TResponse> message,
            IMessageContext context,
            IServiceProvider provider,
            MessageConfiguration configuration)
        {
            var messageType = message.GetType();

            var pipeline = (Pipeline<TResponse>)PipelineCache
                .GetOrAdd(messageType, _ => Activator.CreateInstance(typeof(DefaultPipeline<,>).MakeGenericType(_, typeof(TResponse))));

            return await pipeline
                .Invoke(
                    message,
                    context,
                    provider,
                    configuration)
                .ConfigureAwait(false);
        }
    }
}
