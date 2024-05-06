using Domain.Entities;
using Domain.Primitives;
using NodaTime;
using Utilities;

namespace Tests.Domain.Entities;

public partial class LoanSpecsShould : CommonLoanSteps
{
    private void an_end_date_equal_to_start_date()
    {
        end_date = LocalDate.FromDateTime(DateTime.Parse(start_date_value));
    }

    private void an_amount_equal_to_zero()
    {
        amount = Money.Zero;
    }

    private void a_total_interest_rate_equal_to_zero()
    {
        base_interest_rate = new InterestRate(base_interest_rate_value);
        margin_interest_rate = new InterestRate(-base_interest_rate_value);
    }
    
    private void creating_a_loan()
    {
        loan = new Loan(loan_id, start_date, end_date, Date.Now.Day, amount, currency, base_interest_rate, margin_interest_rate);
    }
}