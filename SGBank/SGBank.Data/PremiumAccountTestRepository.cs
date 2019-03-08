using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Premium Account",
            Balance = 100.00M,
            AccountNumber = "55555",
            Type = AccountType.Premium,
        };
        public Account LoadAccount(string accountNumber)
        {
            if (accountNumber != _account.AccountNumber)
            {
                Console.WriteLine("This is not a valid account number.");
                return null;
            }
            return _account;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
