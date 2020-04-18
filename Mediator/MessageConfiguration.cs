using System;
using System.Collections.Generic;

namespace Mediator
{
    /// <summary>
    /// Class MessageConfiguration
    /// </summary>
    public class MessageConfiguration
    {
        /// <summary>
        /// Gets the behaviors.
        /// </summary>
        /// <value>
        /// The behaviors.
        /// </value>
        public IList<Type> Behaviors { get; } = new List<Type>();
    }
}
