using System.Collections.ObjectModel;
using Domain.Entities;
using Domain.Primitives;
using NodaTime;
using Utilities;

namespace Application;

public class InMemoryLoanRepository : IAmALoanRepository
{
    private List<Loan> loans = [];
    
    public Loan CreateLoan(LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        var loan = new Loan(GetNextLoanId(), startDate, endDate, Date.Now.Day, amount, currency, baseInterestRate, marginInterestRate);
        loans.Add(loan);
        return loan;
    }

    public Loan UpdateLoan(uint loanId, LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        var loan = loans.FirstOrDefault(l => l.Id == loanId);
        if (loan == null)
        {
            throw new InvalidOperationException($"Loan with id {loanId} not found");
        }
        loan = new Loan(loan.Id, startDate, endDate, loan.AccrualDate, amount, currency, baseInterestRate, marginInterestRate);
        return loan;
    }
    
    public ReadOnlyCollection<Loan> ListLoans()
    {
        return loans.AsReadOnly();
    }
    
    private uint GetNextLoanId()
    {
        return (uint)loans.Count + 1;
    }
}