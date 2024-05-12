using NUnit.Framework;

namespace Tests.Application;

[TestFixture]
public partial class LoanRepositorySpecsShould
{
    [Test]
    public void create_loan()
    {
        Given(loan_parameters);
        When(creating_a_loan);
        Then(the_loan_is_created);
    }

    [Test]
    public void update_loan()
    {
        Given(a_loan);
        And(another_loan);
        When(updating_a_loan);
        Then(the_loan_is_updated);
        And(other_loan_is_not_updated);
    }
    
    [Test]
    public void inform_when_loan_does_not_exist()
    {
        Given(a_non_existent_loan_id);
        When(Validating(updating_a_non_existing_loan));
        Then(Informs($"Loan with id {non_existent_loan_id} not found"));
    }
    
    [Test]
    public void list_loans()
    {
        Given(a_loan);
        And(another_loan);
        When(listing_loans);
        Then(all_loans_are_listed);
    }
}