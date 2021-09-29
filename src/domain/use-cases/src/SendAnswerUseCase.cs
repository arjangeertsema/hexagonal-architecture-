using System;
using System.Threading.Tasks;
using System.Transactions;
using example.domain.abstractions.ports.input;
using example.domain.abstractions.ports.output;
using example.domain.abstractions.ports.output.exceptions;

namespace example.domain.use_cases
{
    public class SendAnswerUseCase : ISendAnswerUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;
        private readonly IRegisterCommandPort registerCommandPort;

        public SendAnswerUseCase(
            IHasPermissonPort hasPermissionPort,
            IRegisterCommandPort registerCommandPort
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

            this.hasPermissionPort = hasPermissionPort;
            this.registerCommandPort = registerCommandPort;
        }

        public async Task Execute(ISendAnswerUseCase.Command command)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    await CheckPermission();

                    await registerCommandPort.Execute(command);

                    throw new NotImplementedException();

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