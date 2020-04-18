using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Mediator
{
    /// <summary>
    /// Class DefaultMessageContext
    /// </summary>
    internal class DefaultMessageContext : IMessageContext
    {
        /// <summary>
        /// The provider
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptionsSnapshot<MessageConfiguration> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessageContext" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="options">The options.</param>
        /// <param name="extensions">The extensions.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException">extensions</exception>
        public DefaultMessageContext(
            IServiceProvider provider,
            IOptionsSnapshot<MessageConfiguration> options,
            IContextBag extensions,
            CancellationToken cancellationToken)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            Extensions = extensions ?? throw new ArgumentNullException(nameof(extensions));
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        public IContextBag Extensions { get; }

        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            var messageType = message.GetType();
            var configuration = _options.Get(messageType.Name);

            await MessageOperations
                .Send(
                    message,
                    this,
                    _provider,
                    configuration)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<TResponse> Send<TResponse>(IMessage<TResponse> message)
        {
            var messageType = message.GetType();
            var configuration = _options.Get(messageType.Name);

            return await MessageOperations
                .Send(
                    message,
                    this,
                    _provider,
                    configuration)
                .ConfigureAwait(false);
        }
    }
}
