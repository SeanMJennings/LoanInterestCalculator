using NUnit.Framework;

namespace Tests.Domain.Primitives;

[TestFixture]
public partial class CurrencySpecsShould
{
    [Test]
    public void convert_a_two_letter_iso_code_to_a_currency()
    {
        Given(a_two_letter_iso_code);
        When(converting_to_currency);
        Then(currency_is_created);
    }

    [Test]
    public void informs_unknown_currency_is_unknown()
    {
        Given(an_unknown_code);
        When(Validating(converting_to_currency));
        Then(Informs("The code provided is unknown."));
    }
}