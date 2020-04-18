using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Mediator
{
    /// <summary>
    /// Class DefaultMediator
    /// </summary>
    public class DefaultMediator : IMediator
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptionsSnapshot<MessageConfiguration> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMediator"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">
        /// provider
        /// or
        /// options
        /// </exception>
        public DefaultMediator(IServiceProvider provider, IOptionsSnapshot<MessageConfiguration> options)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task Send<TMessage>(TMessage message, ExtendableOptions options, CancellationToken cancellationToken = default) where TMessage : IMessage
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var context = new DefaultMessageContext(
                _provider,
                _options,
                options.Context,
                cancellationToken);

            var messageType = message.GetType();
            var configuration = _options.Get(messageType.Name);

            await MessageOperations
                .Send(
                    message,
                    context,
                    _provider,
                    configuration)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TResponse> Send<TResponse>(IMessage<TResponse> message, ExtendableOptions options, CancellationToken cancellationToken = default)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var context = new DefaultMessageContext(
                _provider,
                _options,
                options.Context,
                cancellationToken);

            var messageType = message.GetType();
            var configuration = _options.Get(messageType.Name);

            return await MessageOperations
                .Send(
                    message,
                    context,
                    _provider,
                    configuration)
                .ConfigureAwait(false);
        }
    }
}
