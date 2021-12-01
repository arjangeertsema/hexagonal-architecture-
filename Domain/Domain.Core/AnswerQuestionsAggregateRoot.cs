namespace Domain.Core;

public class AnswerQuestionsAggregateRoot : EventSourcedAggregateRoot<AnswerQuestionId>, IAnswerQuestionsAggregateRoot
{
    public static AnswerQuestionsAggregateRoot Create(AnswerQuestionId questionId, string subject, string question, string askedBy)
    {
        return new AnswerQuestionsAggregateRoot
        (
            id: questionId,
            subject: subject,
            question: question,
            askedBy: askedBy
        );
    }

    private AnswerQuestionsAggregateRoot(AnswerQuestionId id, string subject, string question, string askedBy) : base()
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
        }

        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
        }

        if (string.IsNullOrWhiteSpace(askedBy))
        {
            throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or whitespace.", nameof(askedBy));
        }

        RaiseEvent(new QuestionRecievedEvent(Id, subject, question, askedBy, DateTime.Now));
    }

    internal void Apply(QuestionRecievedEvent @event)
    {
        Subject = @event.Subject;
        Question = @event.Question;
        AskedBy = @event.AskedBy;
        Asked = @event.Asked;
    }

    public void AnswerQuestion(IUserTask userTask, string answer, string answeredBy)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        if (string.IsNullOrWhiteSpace(answeredBy))
        {
            throw new ArgumentException($"'{nameof(answeredBy)}' cannot be null or whitespace.", nameof(answeredBy));
        }

        if (Answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is already answered.");
        }

        RaiseEvent(new QuestionAnsweredEvent(Id, userTask, answer, answeredBy, DateTime.Now));
    }

    internal void Apply(QuestionAnsweredEvent @event)
    {
        Answer = @event.Answer;
        AnsweredBy = @event.AnsweredBy;
        Answered = @event.Answered;
    }

    public void AcceptAnswer(IUserTask userTask, string acceptedBy)
    {
        if (string.IsNullOrWhiteSpace(acceptedBy))
        {
            throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
        }

        if (!Answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (Sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has been sent.");
        }

        if (AnsweredBy == acceptedBy)
        {
            throw new AnswerQuestionsException("Answer may not be reviewed by the same person.");
        }

        RaiseEvent(new AnswerAcceptedEvent(Id, userTask, acceptedBy, DateTime.Now));
    }

    internal void Apply(AnswerAcceptedEvent @event)
    {
        AcceptedBy = @event.AcceptedBy;
        Accepted = @event.Accepted;
    }

    public void RejectAnswer(IUserTask userTask, string rejection, string rejectedBy)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        if (string.IsNullOrWhiteSpace(rejectedBy))
        {
            throw new ArgumentException($"'{nameof(rejectedBy)}' cannot be null or whitespace.", nameof(rejectedBy));
        }

        if (!Answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (Sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has been sent.");
        }

        if (AnsweredBy == rejectedBy)
        {
            throw new AnswerQuestionsException("Answer may not be reviewed by the same person.");
        }

        RaiseEvent(new AnswerRejectedEvent(Id, userTask, rejection, rejectedBy, DateTime.Now));
    }

    internal void Apply(AnswerRejectedEvent @event)
    {
        Rejection = @event.Rejection;
        RejectedBy = @event.RejectedBy;
        Rejected = @event.Rejected;
    }

    public void ModifyAnswer(IUserTask userTask, string answer, string modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            throw new ArgumentException($"'{nameof(modifiedBy)}' cannot be null or whitespace.", nameof(modifiedBy));
        }

        if (!Answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (Sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has already been sent.");
        }

        if (!Rejected.HasValue)
        {
            throw new AnswerQuestionsException("Answer has not been rejected.");
        }

        if (AnsweredBy != modifiedBy)
        {
            throw new AnswerQuestionsException("Answer must be modified by the same person.");
        }

        RaiseEvent(new AnswerModifiedEvent(Id, userTask, answer, modifiedBy, DateTime.Now));
    }

    internal void Apply(AnswerModifiedEvent @event)
    {
        Answer = @event.Answer;
        ModifiedBy = @event.ModifiedBy;
        Modified = @event.Modified;
    }

    public Message SendAnswer()
    {
        if (!Accepted.HasValue)
        {
            throw new AnswerQuestionsException("Answer is not accepted.");
        }

        if (Sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has already been sent.");
        };

        RaiseEvent(new AnswerSentEvent(Id, DateTime.Now));

        throw new NotImplementedException();
    }

    internal void Apply(AnswerSentEvent @event)
    {
        Sent = @event.Sent;
    }

    public QuestionAnswerdIntegrationEvent SendEvent()
    {
        throw new NotImplementedException();
    }

    public void EndQuestion(string endedBy)
    {
        throw new NotImplementedException();
    }

    private string Subject { get; set; }
    private string Question { get; set; }
    private string AskedBy { get; set; }
    private DateTime Asked { get; set; }
    private string Answer { get; set; }
    private string AnsweredBy { get; set; }
    private DateTime? Answered { get; set; }
    private string AcceptedBy { get; set; }
    private DateTime? Accepted { get; set; }
    private string Rejection { get; set; }
    private string RejectedBy { get; set; }
    private DateTime? Rejected { get; set; }
    private string ModifiedBy { get; set; }
    private DateTime? Modified { get; set; }
    private DateTime? Sent { get; set; }
}
