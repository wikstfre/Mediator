namespace Mediator
{
    /// <summary>
    /// Class ExtendableOptions
    /// </summary>
    public class ExtendableOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendableOptions" /> class.
        /// </summary>
        public ExtendableOptions()
        {
            Context = new ContextBag();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public IContextBag Context { get; }
    }
}
