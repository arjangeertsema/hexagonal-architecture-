using System;
using Common.DDD.Abstractions;

namespace Domain.Abstractions.Events
{
    public class AnswerSentEvent : VersionedDomainEvent
    {

        public AnswerSentEvent(Guid aggregateId, DateTime sent)
            : base(aggregateId)

        {
            Sent = sent;
        }

        public DateTime Sent { get; }
    }
}