using System.Collections.ObjectModel;
using System.Globalization;
using Application;
using Domain.Entities;
using FluentAssertions;
using Persistence;

namespace Tests.Application;

public partial class LoanBuilderSpecsShould : CommonLoanSteps
{
    private InMemoryLoanRepository _inMemoryLoanRepository = null!;
    private LoanBuilder _loanBuilder = null!;
    private Loan other_loan = null!;
    private ReadOnlyCollection<Loan> loans = null!;
    private const uint other_loan_id = 2;
    private const uint non_existent_loan_id = 3;

    private const string other_start_date_value = "2023-01-01";
    private const string other_end_date_value = "2023-12-31";
    private const decimal other_amount_value = 2000m;
    private const decimal other_base_interest_rate_value = 4m;
    private const decimal other_margin_interest_rate_value = 1.5m;
    private const decimal updated_loan_amount = 3000m;

    protected override void before_each()
    {
        base.before_each();
        _inMemoryLoanRepository = new InMemoryLoanRepository();
        _loanBuilder = new LoanBuilder(_inMemoryLoanRepository);
    }

    private void loan_parameters() {}
    
    private void a_loan()
    {
        loan_parameters();
        creating_a_loan();
    }    
    
    private void another_loan()
    {
        _loanBuilder.WithStartDate(other_start_date_value)
            .WithEndDate(other_end_date_value)
            .WithAmount(other_amount_value)
            .WithCurrency(US_code)
            .WithBaseInterestRate(other_base_interest_rate_value)
            .WithMarginInterestRate(other_margin_interest_rate_value)
            .Create();
    }

    private void creating_a_loan()
    {
        _loanBuilder.WithStartDate(start_date_value)
            .WithEndDate(end_date_value)
            .WithAmount(amount_value)
            .WithCurrency(UK_code)
            .WithBaseInterestRate(base_interest_rate_value)
            .WithMarginInterestRate(margin_interest_rate_value)
            .Create();
        loan = _loanBuilder.List().First();
    }

    private void updating_a_loan()
    {
        _loanBuilder.WithStartDate(start_date_value)
            .WithEndDate(end_date_value)
            .WithAmount(updated_loan_amount)
            .WithCurrency(US_code)
            .WithBaseInterestRate(base_interest_rate_value)
            .WithMarginInterestRate(margin_interest_rate_value)
            .Update(loan_id);
    }
    
    private void listing_loans()
    {
        loans = _loanBuilder.List();
    }

    private void the_loan_is_updated()
    {
        loan = _loanBuilder.List().First();
        loan.Id.Should().Be(loan_id);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(end_date_value);
        ((decimal)loan.Amount).Should().Be(updated_loan_amount);
        loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        ((decimal)loan.BaseInterestRate).Should().Be(base_interest_rate_value);
        ((decimal)loan.MarginInterestRate).Should().Be(margin_interest_rate_value);
        
    }    
    
    private void other_loan_is_not_updated()
    {
        other_loan = _loanBuilder.List().Last();
        other_loan.Id.Should().Be(loan_id + 1);
        other_loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(other_start_date_value);
        other_loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(other_end_date_value);
        ((decimal)other_loan.Amount).Should().Be(other_amount_value);
        other_loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        ((decimal)other_loan.BaseInterestRate).Should().Be(other_base_interest_rate_value);
        ((decimal)other_loan.MarginInterestRate).Should().Be(other_margin_interest_rate_value);
    }

    private static void a_non_existent_loan_id(){}

    private void updating_a_non_existing_loan()
    {
        _loanBuilder.WithAmount(updated_loan_amount)
            .WithCurrency(US_code)
            .Update(non_existent_loan_id);
    }

    private void all_loans_are_listed()
    {
        loans.Count.Should().Be(2);
        loans[0].Id.Should().Be(loan_id);
        loans[1].Id.Should().Be(other_loan_id);
    }
}