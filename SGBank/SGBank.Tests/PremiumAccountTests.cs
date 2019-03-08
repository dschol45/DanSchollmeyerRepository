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
    public class PremiumAccountTests
    {
        [TestCase("55555","Premium Account", 100, AccountType.Free, 250, false)]//fail: wrong account type
        [TestCase("55555","Premium Account", 100, AccountType.Premium, -100, false)]//fail, negative deposit
        [TestCase("55555","Premium Account", 100, AccountType.Premium, 250, true)]//success
        public void PremiumAccountDepositRuleTest(string accountNumber,string name, decimal balance, AccountType type, decimal amount, bool expectedResult)
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

        [TestCase("55555", "Premium Account", 100, AccountType.Basic, -100, 100, false)]//fail, wrong type
        [TestCase("55555", "Premium Account", 200, AccountType.Premium, 100, 200, false)]//fail, positive amount
        [TestCase("55555", "Premium Account", 250, AccountType.Premium, -50, 200, true)]//success
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, -150, -50, true)]//success-overdraft
        [TestCase("55555", "Premium Account", 200, AccountType.Premium, -800, 200, false)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRules();
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
