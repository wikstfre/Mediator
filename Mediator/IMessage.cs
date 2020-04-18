namespace Mediator
{
    /// <summary>
    /// Interface IMessage
    /// </summary>
    public interface IMessage
    {
        // empty interface
    }

    /// <summary>
    /// Class IMessage
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IMessage<out TResponse>
    {
        // empty interface
    }
}
