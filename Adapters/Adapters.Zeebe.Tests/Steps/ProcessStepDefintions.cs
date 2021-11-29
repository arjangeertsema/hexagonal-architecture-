namespace Adapters.Zeebe.Tests.Steps;

/// TODO: move this implementation to the living documentation build pipeline. Linting and Testing BPMN files is a business responsability.

[Binding]
public class ProcessStepDefintions
{
    private readonly IBPMNTestBuilder testBuilder;
    private readonly Random random;
    private IBPMNTestBuilderStep1? step1;
    private IBPMNTestBuilderStep1? step2;
    private IBPMNTestBuilderStepGateway? stepGateway;

    public ProcessStepDefintions(IBPMNTestBuilder testBuilder)
    {
        this.testBuilder = testBuilder ?? throw new System.ArgumentNullException(nameof(testBuilder));
        this.random = new Random();
    }

    [Given(@"an asked question")]
    public void GivenAnAskedQuestion()
    {
        this.step1.Should().BeNull();
        this.step1 = testBuilder.FromUri(Adapters.Zeebe.ZeebeService.BPMN_FILE);
    }

    [When(@"the bot answers the question with a probability greater or equal the (.*)% and smaller then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualTheAndSmallerThen(int p0, int p1)
    {
        this.step2.Should().BeNull();
        this.step2 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(p0, p1 - 1)));
    }

    [When(@"the bot answers the question with a probability smaller then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilitySmallerThen(int p0)
    {
        this.step2.Should().BeNull();
        this.step2 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(0, p0 - 1)));
    }

    [When(@"the bot answers the question with a probability greater or equal then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualThen(int p0)
    {
        this.step1.Should().NotBeNull();
        this.step2 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(p0, 100)));
    }

    [When(@"the gateway with id ""(.*)"" is executed")]
    public void WhenTheGatewayWithIdIsExecuted(string p0)
    {
        this.stepGateway.Should().BeNull();
        this.stepGateway = IsNotNull(this.step2)
            .ExecuteGateway(p0);
    }

    [Then(@"the flow with id ""(.*)"" must be activated")]
    public void ThenTheFlowWithIdMustBeActivated(string p0)
    {
        var test = IsNotNull(this.stepGateway)
            .ExpectFlowActivated(p0)
            .Build();

        test.Execute().Should().BeTrue();
    }

    #region Private

    private static object CreateState(int probability)
    {
        return new { AnswerCorrectProbability = probability };
    }

    private static T IsNotNull<T>(T? t, [CallerArgumentExpression("t")] string? message = null)
    {
        t.Should().NotBeNull();

        if (t == null)
            throw new ArgumentNullException(message);

        return t;
    }

    #endregion
}