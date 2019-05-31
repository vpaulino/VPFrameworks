using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.DDD
{
    /// <summary>
    /// Subscribes to some event TEvent
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventSubscriber<TEvent>
    {
        /// <summary>
        /// Handles the event 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Handle(TEvent @event);
    }
}
