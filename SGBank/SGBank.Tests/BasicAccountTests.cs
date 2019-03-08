using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.WithdrawRules;
using SGBank.BLL.DepositRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    public class BasicAccountTests
    {
        
        [TestCase("33333","Basic Account", 100, AccountType.Free, 250, false)]//fail: wrong account type
        [TestCase("33333","Basic Account", 100, AccountType.Basic, -100, false)]//fail, negative number deposit
        [TestCase("33333","Basic Account", 100, AccountType.Basic, 250, true)]//success
        public void BasicAccountDepositRuleTest(string accountNumber,string name, decimal balance, AccountType type, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = type,
            };
            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]//fail, too much withdrawn
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]//fail, not basic account
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]//fail, positive amount
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]//success
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]//success with overdraft fee
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new BasicAccountWithdrawRules();
            Account account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = type,
            };
            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
