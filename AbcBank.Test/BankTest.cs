using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void customerSummaryOneAccount()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }

        [Test]
        public void customerSummaryTwoAccounts()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            john.openAccount(new Account(Account.SAVINGS));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.customerSummary());
        }


        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.00, false);

            Assert.AreEqual(0.10, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings500_account()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(savingsAccount));

            savingsAccount.deposit(500.00, false);

            Assert.AreEqual(0.50, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings1500_account()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(savingsAccount));

            savingsAccount.deposit(1500.00, false);

            Assert.AreEqual(2.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        //Old tests I added for existing functionality
        //will be ignored since I changed

        [Ignore]
        public void maxi_savings500_account()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(500.00, false);

            Assert.AreEqual(10.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Ignore]
        public void maxi_savings1500_account()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(1500.00, false);

            Assert.AreEqual(45.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
        [Ignore]
        public void maxi_savings3000_account()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(3000.00, false);

            Assert.AreEqual(170.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Ignore]
        public void total_interest_forBank()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);
            checkingAccount.deposit(100.00, false);

            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Customer sam = new Customer("Sam").openAccount(maxiSavingsAccount);
            bank.addCustomer(sam);
            maxiSavingsAccount.deposit(3000.0, false);

            Account savingsAccount = new Account(Account.SAVINGS);
            Customer john = new Customer("John").openAccount(savingsAccount);
            bank.addCustomer(john);
            savingsAccount.deposit(1500.00, false);
            //should be 0.1+170+2
            Assert.AreEqual(172.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account_noWithdrawal()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(1000.00, false);

            Assert.AreEqual(50.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account_with_Withdrawal()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(1200.00, false);
            maxiSavingsAccount.withdraw(200.00, false);

            Assert.AreEqual(1.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void transfer()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Account checkingAccount = new Account(Account.CHECKING);
            Customer john = new Customer("John").openAccount(savingsAccount).openAccount(checkingAccount);
            bank.addCustomer(john);
            savingsAccount.deposit(1500.00, false);
            checkingAccount.deposit(200.00, false);
            john.transfer(savingsAccount, checkingAccount, 300);
            Assert.AreEqual(500, checkingAccount.sumTransactions(), DOUBLE_DELTA);
            Assert.AreEqual(1200, savingsAccount.sumTransactions(), DOUBLE_DELTA);
        }

    }
}
