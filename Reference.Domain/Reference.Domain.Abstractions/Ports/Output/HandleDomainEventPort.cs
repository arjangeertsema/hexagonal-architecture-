using System;
using Synion.DDD.Abstractions;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class HandleDomainEventPort<TEvent> : IOutputPort
            where TEvent : IDomainEvent
    {
        public HandleDomainEventPort(Guid commandId, TEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            CommandId = commandId;
            Event = @event;
        }

        public Guid CommandId { get; }
        public TEvent Event { get; }
    }
}