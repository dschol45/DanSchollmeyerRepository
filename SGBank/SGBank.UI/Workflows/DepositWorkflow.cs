using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models.Responses;

namespace SGBank.UI.Workflows
{
    public class DepositWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            AccountManager accountManager = AccountManagerFactory.Create();
            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();
            decimal amount;
            while (true)
            {
                Console.WriteLine("Enter a deposit amount: ");
                bool decParse = decimal.TryParse(Console.ReadLine(), out amount);
                if (decParse == false)
                {
                    Console.WriteLine("Entry was not a valid type. Please enter a number.");
                }
                else
                {
                    break;
                }
            }
            

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("\n--------------------------------------");
                Console.WriteLine("Deposit completed!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Deposited: {response.Amount:c}");
                Console.WriteLine($"New Balace: {response.Account.Balance:c}");
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
