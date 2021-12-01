﻿namespace Domain.Abstractions.UseCases;

public class AnswerQuestionUseCase : Command, IHasUserTask
{
    public AnswerQuestionUseCase(Guid commandId, AnswerQuestionId questionId, IUserTask userTask, string answer) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        QuestionId = questionId;
        UserTask = userTask;
        Answer = answer;
    }

    public AnswerQuestionId QuestionId { get; }
    public IUserTask UserTask { get; }
    public string Answer { get; }
}
