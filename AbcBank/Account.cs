using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void deposit(double amount, bool isTransfer)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, isTransfer ? Transaction.operation.Transfer : Transaction.operation.Deposit));

            }
        }

        public void withdraw(double amount, bool isTransfer)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, isTransfer ? Transaction.operation.Transfer : Transaction.operation.Withdrawal));
            }
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                // case SUPER_SAVINGS:
                //     if (amount <= 4000)
                //         return 20;
                case MAXI_SAVINGS:
                    //old
                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //else if (amount <= 2000)
                    //    return 20 + (amount - 1000) * 0.05;
                    //else 
                    //    return 70 + (amount - 2000) * 0.1;

                    //new 
                    if (this.isWithdrawal(10))
                        return amount * 0.001;
                    else
                        return amount * 0.05;

                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
        {
            return checkIfTransactionsExist(true);
        }

        private double checkIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public bool isWithdrawal(int numOfDays)
        {
            bool ret = false;
            foreach (Transaction t in transactions)
            {
                if (t.amount < 0 && t.transactionDate > DateTime.Now.AddDays(-numOfDays))
                {
                    ret = true;
                    break;
                }
            }
            return ret;

        }

        public int getAccountType()
        {
            return accountType;
        }

    }
}
