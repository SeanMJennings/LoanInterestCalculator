using System.Collections.ObjectModel;
using Domain.Entities;
using Domain.Primitives;
using NodaTime;

namespace Application;

public interface IAmALoanRepository
{
    Loan CreateLoan(LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate);
    Loan UpdateLoan(uint loanId, LocalDate startDate, LocalDate endDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate);
    
    ReadOnlyCollection<Loan> ListLoans();
}