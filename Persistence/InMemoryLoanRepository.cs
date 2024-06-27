using System.Collections.ObjectModel;
using Domain.Entities;
using Domain.Primitives;
using NodaTime;
using Utilities;

namespace Persistence;

public interface IAmALoanRepository
{
    void CreateLoan(LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate);
    void UpdateLoan(uint loanId, LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate);
    
    ReadOnlyCollection<Loan> ListLoans();
}

public class InMemoryLoanRepository : IAmALoanRepository
{
    private readonly List<Loan> _loans = [];
    
    public void CreateLoan(LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        var loan = new Loan(GetNextLoanId(), startDate, endDate, Date.Now.Day, amount, currency, baseInterestRate, marginInterestRate);
        _loans.Add(loan);
    }

    public void UpdateLoan(uint loanId, LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        var loan = _loans.FirstOrDefault(l => l.Id == loanId);
        if (loan == null)
        {
            throw new InvalidOperationException($"Loan with id {loanId} not found");
        }

        _loans[(int)loanId - 1] = new Loan(loanId, startDate, endDate, loan.AccrualDate, amount, currency, baseInterestRate, marginInterestRate);
    }
    
    public ReadOnlyCollection<Loan> ListLoans()
    {
        return _loans.AsReadOnly();
    }
    
    private uint GetNextLoanId()
    {
        return (uint)_loans.Count + 1;
    }
}