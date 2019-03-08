using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        public Account LoadAccount(string AccountNumber)
        {
            Account result = new Account();
            Dictionary<string, Account> accounts = new Dictionary<string, Account>();
            accounts = RetrieveAccounts();
            if (accounts.TryGetValue(AccountNumber, out result))
            {
            }
            else
            {
                Console.WriteLine("Account not found");
            }
            return result;
        }
        public void SaveAccount(Account account)
        {
            Dictionary<string, Account> accounts = new Dictionary<string, Account>();
            accounts = RetrieveAccounts();
            accounts[account.AccountNumber] = account;
            StreamWriter sw = new StreamWriter("Accounts.txt");
            foreach (Account item in accounts.Values)
            {
                sw.WriteLine(AccountMapper.AccountToString(item));
                sw.Flush();
            }
            if (sw != null) sw.Close();
        }
        private Dictionary<string, Account> RetrieveAccounts()
        {
            Dictionary<string, Account> accounts = new Dictionary<string, Account>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader("Accounts.txt");
                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Account a = AccountMapper.StringToAccount(row);
                    accounts.Add(a.AccountNumber, a);
                }
            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine(fileNotFound.FileName + " was not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return accounts;
        }
    }
}
