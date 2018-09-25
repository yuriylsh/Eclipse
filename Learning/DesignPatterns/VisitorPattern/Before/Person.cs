using System.Collections.Generic;

namespace VisitorPattern.Before
{
    public class Person
    {
        public List<RealEstate> RealEstates = new List<RealEstate>();

        public List<BankAccount> BankAccounts = new List<BankAccount>();

        public List<Loan> Loans = new List<Loan>();
    }

    public class RealEstate
    {
        public int EstimatedValue { get; set; }

        public int MonthlyRent { get; set; }
    }

    public class BankAccount
    {
        public int Amount { get; set; }

        public double MonthlyInterest { get; set; }
    }

    public class Loan
    {
        public int Owed { get; set; }

        public int MonthlyPayment { get; set; }
    }
}