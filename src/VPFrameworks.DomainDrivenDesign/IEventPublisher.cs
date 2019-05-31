using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.DDD
{
    /// <summary>
    /// Publisher of data of type T
    /// </summary>
    /// <typeparam name="T">Type of data that represents the event</typeparam>
    public interface IEventPublisher<T>
    {
        /// <summary>
        /// Sends and forgets the data to some messaging channel
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Publish(T @event);
    }
}
