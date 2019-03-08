using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.WithdrawRules
{
    public class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    FreeAccountWithdrawRules freeWithdrawRule = new FreeAccountWithdrawRules();
                    return freeWithdrawRule;
                case AccountType.Basic:
                    BasicAccountWithdrawRules basicWithdrawRule = new BasicAccountWithdrawRules();
                    return basicWithdrawRule;
                case AccountType.Premium:
                    PremiumAccountWithdrawRules premiumWithdrawRule = new PremiumAccountWithdrawRules();
                    return premiumWithdrawRule;
                default:
                    throw new Exception("Account type is not supported!");
            }
        }
    }
}
