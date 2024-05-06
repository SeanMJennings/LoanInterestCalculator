using Domain.Primitives;
using FluentAssertions;

namespace Tests.Domain.Primitives;

public partial class InterestRateSpecsShould : Specification
{
    private const decimal positive_value = 1.4567m;
    private decimal value;
    private InterestRate interest_rate;
    private InterestRate other_interest_rate;

    protected override void before_each()
    {
        base.before_each();
        value = 0m;
    }
    
    private void a_decimal_value()
    {
        value = positive_value;
    }

    private void converting_to_interest_rate()
    {
        interest_rate = new InterestRate(value);
    }

    private void interest_is_formatted_to_2_dp_with_percentage_symbol()
    {
        interest_rate.ToString().Should().Be($"{Math.Round(positive_value, 2)}%");
    }

    private void an_interest_rate()
    {
        interest_rate = new InterestRate(positive_value);
    }

    private void another_interest_rate()
    {
        other_interest_rate = new InterestRate(positive_value);
    }

    private void adding_to_another_interest_rate()
    {
        interest_rate += other_interest_rate;
    }

    private void the_rates_are_added_together()
    {
        ((decimal)interest_rate).Should().Be(positive_value + positive_value);
    }
}