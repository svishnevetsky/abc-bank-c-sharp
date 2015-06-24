using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public enum operation { Withdrawal, Deposit, Transfer };
        public readonly double amount;
        public readonly operation tranType;
        private DateTime _transactionDate;

        public Transaction(double amount, operation type)
        {
            this.amount = amount;
            this.tranType = type;
            this._transactionDate = DateProvider.getInstance().now();
        }
        public DateTime transactionDate
        {
            get { return _transactionDate; }
        }


    }
}
