using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.DDD
{
    public interface IEventSubscriber<TEvent>
    {
        Task Handle(TEvent @event);
    }
}
