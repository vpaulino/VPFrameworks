

using System.Threading.Tasks;

namespace VPFrameworks.Messaging.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task Publish<T>(T data);
    }
}
