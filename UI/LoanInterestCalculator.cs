using System.ComponentModel.DataAnnotations;
using Utilities;

namespace UI;

public static class LoanInterestCalculator
{
    public static void Run()
    {
        Console.WriteLine("To end loan interest calculator, type: exit");
        while (true)
        {
            CreateOrUpdateLoan();
        }
    }
    
    private static void CreateOrUpdateLoan()
    {
        if (Services.LoanBuilder.List().Count == 0)
        {
            CreateLoan();
        }
        else
        {
            foreach (var loan in Services.LoanBuilder.List())
            {
                Console.WriteLine(loan);
            }
            var input = GetInput<string>("\nTo update existing loan, type: update. Else type any other key.", s => s);
            if (input.Equals("update", StringComparison.InvariantCultureIgnoreCase))
            {
                UpdateLoan();
            }
            else
            {
                CreateLoan();
            }
        }
    }

    private static void CreateLoan()
    {
        Console.WriteLine("Create loan:");
        GetLoanInputs();
        try
        {
            Services.LoanBuilder.Create();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void GetLoanInputs()
    {
        GetInput("Please enter the loan start date in format: YYYY-MM-DD", Services.LoanBuilder.WithStartDate);
        GetInput("Please enter the loan end date in format: YYYY-MM-DD", Services.LoanBuilder.WithEndDate);
        GetInput("Please enter the loan amount", Services.LoanBuilder.WithAmount);
        Console.WriteLine($"Supported currencies: {string.Join(", ", Regions.Countries())}");
        GetInput("Please enter the currency code", Services.LoanBuilder.WithCurrency);
        Console.WriteLine($"Selected currency: {Services.LoanBuilder.CurrencyNativeName()}");
        GetInput("Please enter the base interest rate", Services.LoanBuilder.WithBaseInterestRate);
        GetInput("Please enter the margin interest rate", Services.LoanBuilder.WithMarginInterestRate);
    }
    
    private static void UpdateLoan()
    {
        Console.WriteLine("Updating loan:");
        var loanId = GetInput("Please enter the loan id", uint.Parse);
        GetLoanInputs();
        try
        {
            Services.LoanBuilder.Update(loanId);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static T GetInput<T>(string prompt, Func<string,T> inputLambda)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (input?.Equals("exit", StringComparison.InvariantCultureIgnoreCase) ?? false)
                {
                    Environment.Exit(0);
                }
                return inputLambda(input!);
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}