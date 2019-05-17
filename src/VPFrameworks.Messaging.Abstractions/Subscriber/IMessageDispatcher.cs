
using System.Threading;
using System.Threading.Tasks;

namespace VPFrameworks.Messaging.Abstractions
{
    /// <summary>
    /// Dispatch messages 
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageReceived"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> TryDispatch(MessageReceived messageReceived, CancellationToken token);
    }
}