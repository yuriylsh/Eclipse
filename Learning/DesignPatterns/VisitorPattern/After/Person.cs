using System.Collections.Generic;

namespace VisitorPattern.After
{
    public interface IVisitor
    {
        void Visit(RealEstate realEstate);
        void Visit(BankAccount bankAccount);
        void Visit(Loan loan);
    }

    public class NetWorthVisitor: IVisitor
    {
        public int Total { get; set; }

        public void Visit(RealEstate realEstate) => Total += realEstate.EstimatedValue;

        public void Visit(BankAccount bankAccount) => Total += bankAccount.Amount;

        public void Visit(Loan loan) => Total -= loan.Owed;
    }

    public class IncomeVisitor: IVisitor
    {
        public double Amount { get; set; }

        public void Visit(RealEstate realEstate) => Amount += realEstate.MonthlyRent;

        public void Visit(BankAccount bankAccount) => Amount += bankAccount.Amount * bankAccount.MonthlyInterest;

        public void Visit(Loan loan) => Amount -= loan.MonthlyPayment;
    }

    public interface IAsset
    {
        void Accept(IVisitor visitor);
    }

    public class Person: IAsset
    {
        public List<IAsset> Assets { get; set; } = new List<IAsset>();

        void IAsset.Accept(IVisitor visitor)
        {
            foreach (var asset in Assets)
            {
                asset.Accept(visitor);
            }
        }
    }

    public class RealEstate: IAsset
    {
        public int EstimatedValue { get; set; }

        public int MonthlyRent { get; set; }

        void IAsset.Accept(IVisitor visitor) => visitor.Visit(this);
    }

    public class BankAccount: IAsset
    {
        public int Amount { get; set; }

        public double MonthlyInterest { get; set; }

        void IAsset.Accept(IVisitor visitor) => visitor.Visit(this);
    }

    public class Loan: IAsset
    {
        public int Owed { get; set; }

        public int MonthlyPayment { get; set; }

        void IAsset.Accept(IVisitor visitor) => visitor.Visit(this);
    }
}
