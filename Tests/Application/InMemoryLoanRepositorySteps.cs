using System.Collections.ObjectModel;
using System.Globalization;
using Application;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using NodaTime;
namespace Tests.Application;

public partial class LoanRepositorySpecsShould : CommonLoanSteps
{
    private InMemoryLoanRepository _inMemoryLoanRepository = null!;
    private Loan other_loan = null!;
    private ReadOnlyCollection<Loan> loans = null!;
    private uint the_loan_id;
    private const int other_loan_id = 2;
    private const int non_existent_loan_id = 3;
    private const string UK_code = "GB";
    private const string UK_iso_currency_symbol = "GBP";
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
        the_loan_id = loan_id;
    }

    private void loan_parameters()
    {
        a_start_date();
        an_end_date();
        an_amount();
        a_currency();
        a_base_interest_rate();
        a_margin_interest_rate();
    }
    
    private void a_loan()
    {
        loan_parameters();
        creating_a_loan();
    }    
    
    private void another_loan()
    {
        start_date = LocalDate.FromDateTime(DateTime.Parse(other_start_date_value));
        end_date = LocalDate.FromDateTime(DateTime.Parse(other_end_date_value));
        amount = new Money(other_amount_value);
        currency = new Currency(UK_code);
        base_interest_rate = new InterestRate(other_base_interest_rate_value);
        margin_interest_rate = new InterestRate(other_margin_interest_rate_value);
        other_loan = _inMemoryLoanRepository.CreateLoan(start_date, end_date, amount, currency, base_interest_rate, margin_interest_rate);
    }

    private void creating_a_loan()
    {
        loan = _inMemoryLoanRepository.CreateLoan(start_date, end_date, amount, currency, base_interest_rate, margin_interest_rate);
    }

    private void updating_a_loan()
    {
        loan_parameters();
        amount = new Money(updated_loan_amount);
        loan = _inMemoryLoanRepository.UpdateLoan(the_loan_id, start_date, end_date, amount, currency, base_interest_rate, margin_interest_rate);
    }
    
    private void listing_loans()
    {
        loans = _inMemoryLoanRepository.ListLoans();
    }

    private void the_loan_is_updated()
    {
        loan.Id.Should().Be(the_loan_id);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(end_date_value);
        ((decimal)loan.Amount).Should().Be(updated_loan_amount);
        loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        ((decimal)loan.BaseInterestRate).Should().Be(base_interest_rate);
        ((decimal)loan.MarginInterestRate).Should().Be(margin_interest_rate);
        
    }    
    
    private void other_loan_is_not_updated()
    {
        other_loan_id.Should().Be(loan_id + 1);
        other_loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(other_start_date_value);
        other_loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(other_end_date_value);
        ((decimal)other_loan.Amount).Should().Be(other_amount_value);
        other_loan.Currency.ToString().Should().Be(UK_iso_currency_symbol);
        ((decimal)other_loan.BaseInterestRate).Should().Be(other_base_interest_rate_value);
        ((decimal)other_loan.MarginInterestRate).Should().Be(other_margin_interest_rate_value);
    }

    private void a_non_existent_loan_id()
    {
        the_loan_id = non_existent_loan_id;
    }

    private void updating_a_non_existing_loan()
    {
        updating_a_loan();
    }

    private void all_loans_are_listed()
    {
        loans.Count.Should().Be(2);
        loans[0].Id.Should().Be(loan_id);
        loans[1].Id.Should().Be(other_loan_id);
    }
}