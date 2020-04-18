using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Delegate PipelineDelegate
    /// </summary>
    /// <returns></returns>
    public delegate Task PipelineDelegate();

    /// <summary>
    /// Delegate PipelineDelegate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public delegate Task<T> PipelineDelegate<T>();
}
