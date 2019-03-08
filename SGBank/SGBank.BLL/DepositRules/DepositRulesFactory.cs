using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.DepositRules
{
    public class DepositRulesFactory
    {
        public static IDeposit Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    return new FreeAccountDepositRules();
                case AccountType.Basic:
                case AccountType.Premium:
                    return new NoLimitDepositRule();
                default:
                    break;
            }

            throw new Exception("Account type is not supported!");
        }
    }
}
