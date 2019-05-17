using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.DDD
{
    public interface IEventPublisher<T>
    {
        Task Publish(T @event);
    }
}
