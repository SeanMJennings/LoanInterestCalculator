using FluentAssertions;
using NodaTime.Extensions;
using NUnit.Framework;
using Utilities;

namespace Tests;

public class Specification
{
    protected static Exception error = null!;

    [SetUp]
    protected virtual void before_each()
    {
        error = null!;
        Date.Now = DateTime.Now.ToLocalDateTime().Date;
    }
    
    protected static void Given(Action testAction)
    {
        testAction.Invoke();
    }

    protected static void And(Action testAction)
    {
        testAction.Invoke();
    }

    protected static void When(Action testAction)
    {
        testAction.Invoke();
    }

    protected static void Then(Action testAction)
    {
        testAction.Invoke();
    }
    
    protected static void Scenario(Action testAction)
    {
        testAction.Invoke();
    }
    
    protected static Action Validating(Action testAction)
    {
        return () =>
        {
            try
            {
                testAction.Invoke();
            }
            catch (Exception e)
            {
                error = e;
            }
        };
    }
    
    protected static Action Informs(string message)
    {
        return () =>
        {
            error.Message.Should().Be(message);
        };
    }
}