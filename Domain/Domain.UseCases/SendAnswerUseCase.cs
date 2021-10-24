using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Commands;
using Common.DDD.Abstractions.Queries;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
    {
        private readonly IMediator mediator;

        public SendAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("SEND_ANSWER")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(SendAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            aggregateRoot.SendAnswer();

            await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot>(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}