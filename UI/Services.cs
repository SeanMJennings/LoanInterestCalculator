using Application;

namespace UI;

public static class Services
{
    private static IAmALoanRepository LoanRepository { get; } = new InMemoryLoanRepository();
    public static LoanBuilder LoanBuilder { get; } = new(LoanRepository);
}