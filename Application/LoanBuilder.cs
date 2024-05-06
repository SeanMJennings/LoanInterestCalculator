using System.Collections.ObjectModel;
using Domain.Entities;
using Domain.Primitives;
using NodaTime;
using Utilities;

namespace Application;

public class LoanBuilder(IAmALoanRepository loanRepository)
{
    private LocalDate StartDate { get; set; }
    private LocalDate EndDate { get; set; }
    private Money Amount { get; set; }
    private Currency Currency { get; set; }
    private InterestRate BaseInterestRate { get; set; }
    private InterestRate MarginInterestRate { get; set; }
    
    public LoanBuilder WithStartDate(string value)
    {
        StartDate = Date.ParseDate(value);
        return this;
    }
    
    public LoanBuilder WithEndDate(string value)
    {
        EndDate = Date.ParseDate(value);
        return this;
    }
    
    public LoanBuilder WithAmount(string value)
    {
        Amount = new Money(value);
        return this;
    }
    
    public LoanBuilder WithCurrency(string value)
    {
        Currency = new Currency(value);
        return this;
    }
    
    public LoanBuilder WithBaseInterestRate(string value)
    {
        BaseInterestRate = new InterestRate(value);
        return this;
    }
    
    public LoanBuilder WithMarginInterestRate(string value)
    {
        MarginInterestRate = new InterestRate(value);
        return this;
    }

    public string CurrencyNativeName()
    {
        return Currency.RegionInfo.CurrencyNativeName;
    }
    
    public void Create()
    {
        loanRepository.CreateLoan(StartDate, EndDate, Amount, Currency, BaseInterestRate, MarginInterestRate);
    }
    
    public void Update(uint id)
    {
        loanRepository.UpdateLoan(id, StartDate, EndDate, Amount, Currency, BaseInterestRate, MarginInterestRate);
    }
    
    public ReadOnlyCollection<Loan> List()
    {
        return loanRepository.ListLoans();
    }
}