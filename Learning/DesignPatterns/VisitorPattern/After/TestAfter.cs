using FluentAssertions;
using Xunit;

namespace VisitorPattern.After
{
    public class TestAfter
    {
        [Fact]
        public void NetWorth()
        {
            IAsset person = SetupPerson();
            var netWorthVisitor = new NetWorthVisitor();
            person.Accept(netWorthVisitor);

            netWorthVisitor.Total.Should().Be(42_000);
        }

        [Fact]
        public void Income()
        {
            IAsset person = SetupPerson();
            var incomeVisitor = new IncomeVisitor();
            person.Accept(incomeVisitor);

            incomeVisitor.Amount.Should().Be(510d);
        }

        private static Person SetupPerson() => new Person
        {
            Assets = 
            {
                new BankAccount {Amount = 1_000, MonthlyInterest = 0.01},
                new BankAccount {Amount = 2_000, MonthlyInterest = 0.02},
                new RealEstate {EstimatedValue = 79_000, MonthlyRent = 500},
                new Loan {Owed = 40_000, MonthlyPayment = 40}
            }
        };
    }
}