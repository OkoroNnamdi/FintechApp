using FinTech.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Interfaces
{
    public  interface IAcccountService
    {
        Account Authenticate(string AccounNumber, string password);
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountByAccountNumber(string AccountNumber);
        Account Create(Account  account, string password, string confirmPassword);
        Task Update(Account account );
        void Delete(int accountId);
        Task<Account> GetByEmail(string Email);
    }
}
