﻿using System;
using Common.CQRS.Abstractions.Commands;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.Ports.Input
{
    public class ModifyAnswerUseCase : ICommand, IUserTask
    {
        public ModifyAnswerUseCase(Guid commandId, Guid questionId, string userTaskId, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            CommandId = commandId;
            QuestionId = questionId;
            UserTaskId = userTaskId;
            Answer = answer;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public string UserTaskId { get; set; }
        public string Answer { get; set; }
    }
}
