using System;
using System.Threading.Tasks;
using System.Transactions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;
using Reference.Domain.Core;

namespace Reference.Domain.UseCases
{
    public class AcceptAnswerUseCase : IAcceptAnswerUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;
        private readonly IRegisterCommandPort registerCommandPort;
        private readonly IAggregateRootStore aggregateRootStore;
        private readonly IGetIdentityPort getIdentityPort;

        public AcceptAnswerUseCase(
            IHasPermissonPort hasPermissionPort,
            IRegisterCommandPort registerCommandPort,
            IAggregateRootStore aggregateRootStore,
            IGetIdentityPort getIdentityPort
        )
        {
            if (hasPermissionPort is null)
            {
                throw new ArgumentNullException(nameof(hasPermissionPort));
            }

            if (registerCommandPort is null)
            {
                throw new ArgumentNullException(nameof(registerCommandPort));
            }

            if (aggregateRootStore is null)
            {
                throw new ArgumentNullException(nameof(aggregateRootStore));
            }

            this.hasPermissionPort = hasPermissionPort;
            this.registerCommandPort = registerCommandPort;
            this.aggregateRootStore = aggregateRootStore;
            this.getIdentityPort = getIdentityPort ?? throw new ArgumentNullException(nameof(getIdentityPort));
        }

        public async Task Execute(IAcceptAnswerUseCase.Command command)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    await CheckPermission();

                    var identity = await getIdentityPort.Execute(new IGetIdentityPort.Query());

                    await registerCommandPort.Execute(command);
                    
                    var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

                    aggregateRoot.AcceptAnswer(command.TaskId, identity.Id);

                    await aggregateRootStore.Save(aggregateRoot);
                    
                    scope.Complete();
                }
            }
            catch (CommandAlreadyExistsException)
            {
                return;
            }
        }

        private async Task CheckPermission()
        {
            var hasPermission = await hasPermissionPort.Execute(new IHasPermissonPort.Query("a permission"));
            if (!hasPermission)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}