using FluentAssertions;
using Xunit;

namespace VisitorPattern.Before
{
    public class TestBefore
    {
        [Fact]
        public void NetWorth()
        {
            var person = SetupPerson();
            
            var netWorth = 0;
            foreach (var bankAccount in person.BankAccounts)
            {
                netWorth += bankAccount.Amount;
            }
            foreach (var realEstate in person.RealEstates)
            {
                netWorth += realEstate.EstimatedValue;
            }

            foreach (var loan in person.Loans)
            {
                netWorth -= loan.Owed;
            }

            netWorth.Should().Be(42_000);
        }

        [Fact]
        public void Income()
        {
            var person = SetupPerson();
            
            double amount = 0;
            foreach (var bankAccount in person.BankAccounts)
            {
                amount += bankAccount.Amount * bankAccount.MonthlyInterest;
            }
            foreach (var realEstate in person.RealEstates)
            {
                amount += realEstate.MonthlyRent;
            }

            foreach (var loan in person.Loans)
            {
                amount -= loan.MonthlyPayment;
            }

            amount.Should().Be(510d);
        }

        private static Person SetupPerson()
        {
            var person = new Person();
            person.BankAccounts.Add(new BankAccount {Amount = 1_000, MonthlyInterest = 0.01});
            person.BankAccounts.Add(new BankAccount {Amount = 2_000, MonthlyInterest = 0.02});
            person.RealEstates.Add(new RealEstate {EstimatedValue = 79_000, MonthlyRent = 500});
            person.Loans.Add(new Loan {Owed = 40_000, MonthlyPayment = 40});
            return person;
        }
    }
}
