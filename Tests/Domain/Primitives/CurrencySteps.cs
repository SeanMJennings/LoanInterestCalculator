using Domain.Primitives;
using FluentAssertions;

namespace Tests.Domain.Primitives;

public partial class CurrencySpecsShould : Specification
{
    private Currency currency;
    private string code;
    private string US_code = "US";
    private string unknown_code = "wibble";
    private string US_iso_currency_symbol = "USD";

    protected override void before_each()
    {
        base.before_each();
        code = string.Empty;
    }

    private void a_two_letter_iso_code()
    {
        code = US_code;
    }    
    
    private void an_unknown_code()
    {
        code = unknown_code;
    }

    private void converting_to_currency()
    {
        currency = new Currency(code);
    }

    private void currency_is_created()
    {
        currency.ToString().Should().Be(US_iso_currency_symbol);
    }
}