using Domain.Primitives;
using NodaTime;
using Utilities;

namespace Domain.Entities;

public class Loan
{
    public LocalDate StartDate { get; }
    public LocalDate EndDate { get; }
    public int AccrualDate { get; }
    public Money Amount { get; }
    public Currency Currency { get; }
    public InterestRate BaseInterestRate { get; }
    public InterestRate MarginInterestRate { get; }
    public uint Id { get; }

    public Loan(uint id, LocalDate startDate, LocalDate endDate, int accrualDate, Money amount, Currency currency, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        ValidateInputs(startDate, endDate, amount, baseInterestRate, marginInterestRate);
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        AccrualDate = accrualDate;
        Amount = amount;
        Currency = currency;
        BaseInterestRate = baseInterestRate;
        MarginInterestRate = marginInterestRate;
    }

    public override string ToString()
    {
        return $"""
            Loan ID: {Id}
            Period: {StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}
            Value: {Amount} {Currency}
            Base interest rate: {BaseInterestRate}
            Margin interest rate: {MarginInterestRate}
            Accrual Date: Day {AccrualDate} of month
            Daily interest without margin: {DailyInterest(true)} {Currency}
            Daily interest: {DailyInterest()} {Currency}
            Days elapsed: {DaysElapsed()}
            Total interest: {TotalInterest()} {Currency}
            """;
    }
    
    private static void ValidateInputs(LocalDate startDate, LocalDate endDate, Money amount, InterestRate baseInterestRate, InterestRate marginInterestRate)
    {
        Validation.BasedOn(errors =>
        {
            if (endDate <= startDate)
            {
                errors.Add("End date must be after start date.");
            }
            if (amount <= 0)
            {
                errors.Add("Amount must be greater than zero.");
            }
            if (baseInterestRate + marginInterestRate <= 0)
            {
                errors.Add("Total interest rate must be greater than zero.");
            }
        });
    }

    private Money DailyInterest(bool withoutMargin = false)
    {
        return Amount * (withoutMargin ? BaseInterestRate : BaseInterestRate + MarginInterestRate) / 100 / 365;
    }
    
    private int DaysElapsed()
    {
        return Period.Between(StartDate, Date.Now, PeriodUnits.Days).Days;
    }
    
    private Money TotalInterest()
    {
        return DailyInterest() * Period.Between(StartDate, EndDate, PeriodUnits.Days).Days;
    }
}