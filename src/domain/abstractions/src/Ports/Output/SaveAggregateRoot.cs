using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class SaveAggregateRoot : IOutputPort
    {
        public SaveAggregateRoot(Guid commandId, IAggregateRoot aggregateRoot)
        {
            if (aggregateRoot is null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            CommandId = commandId;
            AggregateRoot = aggregateRoot;
        }

        public Guid CommandId { get; }
        public IAggregateRoot AggregateRoot { get; }
    }
}