using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator
{
    /// <summary>
    /// Class Pipeline
    /// </summary>
    internal abstract class Pipeline
    {
        /// <summary>
        /// Invokes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public abstract Task Invoke(IMessage message, IMessageContext context, IServiceProvider provider, MessageConfiguration configuration);
    }

    /// <summary>
    /// Class Pipeline
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    internal abstract class Pipeline<TResponse>
    {
        /// <summary>
        /// Invokes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public abstract Task<TResponse> Invoke(IMessage<TResponse> message, IMessageContext context, IServiceProvider provider, MessageConfiguration configuration);
    }

    /// <summary>
    /// Class DefaultMessageHandler
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    internal class DefaultPipeline<TMessage> : Pipeline where TMessage : IMessage
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public override async Task Invoke(IMessage message, IMessageContext context, IServiceProvider provider, MessageConfiguration configuration)
        {
            var handler = GetMessageHandler(message, context, provider);

            await configuration
                .Behaviors
                .Distinct()
                .Reverse()
                .Select(_ => (IMessageBehavior<TMessage>)provider.GetRequiredService(_))
                .Aggregate(handler, (next, pipeline) => async () => await pipeline.Handle((TMessage)message, context, next).ConfigureAwait(false))()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the message handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        private static PipelineDelegate GetMessageHandler(IMessage message, IMessageContext context, IServiceProvider provider)
        {
            var handler = provider.GetService<IMessageHandler<TMessage>>();

            return () => handler?.Handle((TMessage)message, context) ?? Task.CompletedTask;
        }
    }

    /// <summary>
    /// Class DefaultMessageHandler
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    internal class DefaultPipeline<TMessage, TResponse> : Pipeline<TResponse> where TMessage : IMessage<TResponse>
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public override async Task<TResponse> Invoke(IMessage<TResponse> message, IMessageContext context, IServiceProvider provider, MessageConfiguration configuration)
        {
            var handler = GetMessageHandler(message, context, provider);

            return await configuration
                .Behaviors
                .Distinct()
                .Reverse()
                .Select(_ => (IMessageBehavior<TMessage, TResponse>)provider.GetRequiredService(_))
                .Aggregate(handler, (next, pipeline) => async () => await pipeline.Handle((TMessage)message, context, next).ConfigureAwait(false))()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the message handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        private static PipelineDelegate<TResponse> GetMessageHandler(IMessage<TResponse> message, IMessageContext context, IServiceProvider provider)
        {
            var handler = provider.GetService<IMessageHandler<TMessage, TResponse>>();

            return () => handler?.Handle((TMessage)message, context) ?? Task.FromResult(default(TResponse));
        }
    }
}
