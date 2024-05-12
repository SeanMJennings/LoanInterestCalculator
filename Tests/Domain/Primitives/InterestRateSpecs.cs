using NUnit.Framework;

namespace Tests.Domain.Primitives;

[TestFixture]
public partial class InterestRateSpecsShould
{
    [Test]
    public void provide_rate_to_2_dp_with_percentage_symbol()
    {
        Given(a_decimal_value);
        When(converting_to_interest_rate);
        Then(interest_is_formatted_to_2_dp_with_percentage_symbol);
    }
    
    [Test]
    public void add_interest_rates_together()
    {
        Given(an_interest_rate);
        And(another_interest_rate);
        When(adding_to_another_interest_rate);
        Then(the_rates_are_added_together);
    }
}