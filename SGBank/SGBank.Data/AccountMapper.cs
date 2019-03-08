using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.Data
{
    class AccountMapper
    {
        public static string AccountToString(Account account)
        {
            string type = account.Type.ToString().Substring(0, 1);
            return $"{account.AccountNumber},{account.Name},{account.Type.ToString().Remove(1)},{account.Balance.ToString()}";
        }

        public static Account StringToAccount(string row)
        {
            string[] fields = row.Split(new string[] { "," }, StringSplitOptions.None);
            AccountType type = new AccountType();
            switch (fields[2])
            {
                case "F":
                    type = AccountType.Free;
                    break;
                case "B":
                    type = AccountType.Basic;
                    break;
                case "P":
                    type = AccountType.Premium;
                    break;
            }
            //type = (AccountType)Enum.Parse(typeof(AccountType), fields[2], false);
            Account result = new Account()
            {
                AccountNumber = fields[0],
                Name = fields[1],
                Type = type,
                Balance = decimal.Parse(fields[3]),
            };
            return result;
        }
    }
}
