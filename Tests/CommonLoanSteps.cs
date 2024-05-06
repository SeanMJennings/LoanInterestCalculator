using System.Globalization;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using NodaTime;
using Utilities;

namespace Tests;

public class CommonLoanSteps : Specification
{
    protected const int loan_id = 1;
    private const string US_code = "US";
    protected const string US_iso_currency_symbol = "USD";
    protected const string start_date_value = "2022-01-01";
    protected const string end_date_value = "2022-12-31";
    private const decimal amount_value = 1000m;
    protected const decimal base_interest_rate_value = 5m;
    private const decimal margin_interest_rate_value = 2.5m;
    protected Currency currency;
    protected LocalDate start_date;
    protected LocalDate end_date;
    protected Money amount;
    protected InterestRate base_interest_rate;
    protected InterestRate margin_interest_rate;
    protected Loan loan = null!;

    protected override void before_each()
    {
        base.before_each();
        currency = default;
        start_date = default;
        end_date = default;
        amount = default;
        base_interest_rate = default;
        margin_interest_rate = default;
        loan = null!;
    }
    
    protected void a_start_date()
    {
        start_date = LocalDate.FromDateTime(DateTime.Parse(start_date_value));
    }

    protected void an_end_date()
    {
        end_date = LocalDate.FromDateTime(DateTime.Parse(end_date_value));
    }

    protected void an_amount()
    {
        amount = new Money(amount_value);
    }

    protected void a_currency()
    {
        currency = new Currency(US_code);
    }

    protected void a_base_interest_rate()
    {
        base_interest_rate = new InterestRate(base_interest_rate_value);
    }

    protected void a_margin_interest_rate()
    {
        margin_interest_rate = new InterestRate(margin_interest_rate_value);
    }
    
    protected void the_loan_is_created()
    {
        loan.Should().NotBeNull();
        loan.Id.Should().Be(1);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(end_date_value);
        ((decimal)loan.Amount).Should().Be(amount);
        loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        ((decimal)loan.BaseInterestRate).Should().Be(base_interest_rate);
        ((decimal)loan.MarginInterestRate).Should().Be(margin_interest_rate);
    }

    protected void is_formatted_correctly()
    {
        var expected_formatting = $"""
            Loan ID: {loan.Id}
            Period: {loan.StartDate:yyyy-MM-dd} to {loan.EndDate:yyyy-MM-dd}
            Value: {loan.Amount} {loan.Currency}
            Base interest rate: {loan.BaseInterestRate}
            Margin interest rate: {loan.MarginInterestRate}
            Accrual Date: Day {loan.AccrualDate} of month
            Daily interest without margin: {Math.Round(amount * base_interest_rate / 100 / 365, 2)} {loan.Currency}
            Daily interest: {Math.Round(amount * (base_interest_rate + margin_interest_rate) / 100 / 365, 2)} {loan.Currency}
            Days elapsed: {Period.Between(start_date, Date.Now, PeriodUnits.Days).Days}
            Total interest: {Math.Round(amount * (base_interest_rate + margin_interest_rate) / 100 / 365 * Period.Between(start_date, end_date, PeriodUnits.Days).Days,2)} {loan.Currency}
            """;
        loan.ToString().Should().Be(expected_formatting);
    }
}