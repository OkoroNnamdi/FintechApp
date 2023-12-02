using FinTech.DB;
using FinTechCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Implementations
{
    public class AccountService : IAcccountService
    {
        private readonly AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            _context = context;
        }
        public Account Authenticate(string AccountNumber, string password)
        {
            var account = _context.Accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefault();
            if (account == null)
                return null;


            if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
                return null;

            return account;
        }
        private bool VerifyPasswordHash(string paswword, byte[] passwordHash, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(paswword))
                throw new ArgumentNullException("password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(paswword));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;

        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var Hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Hmac.Key;
                passwordHash = Hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }

        }
        public Account Create(Account account, string password, string confirmPassword)
        {
            //if (_DbContext.Wallets.Any(x => x.Email == wallet.Email))
            //    throw new System.ApplicationException("A wallet already exist with this Email");
            if (!password.Equals(confirmPassword))
                throw new ArgumentException("Passwords do not match");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            _context.Add(account);
            _context.SaveChanges();
            return account;
        }

        public void Delete(int accountId)
        {
            var account = _context.Accounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            if(account != null)
            {
                _context.Accounts.Remove(account );
                _context.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public async  Task<Account> GetByEmail(string Email)
        {
           var account = _context.Accounts.Where(x => x.EmailAddress == Email).FirstOrDefaultAsync();
            if (account  == null)
                return null;
            return await account;
        }

        public Account GetAccountByAccountNumber(string AccountNumber)
        {
            var account = _context.Accounts .Where(x => x.AccountNumber == AccountNumber).FirstOrDefault();
            if (account  == null)
                return null;
            return account;
        }

        public async  Task Update(Account account)
        {
            var AccountToUpdate =await  _context.Accounts.Where(x => x.AccountId == account.AccountId ).FirstOrDefaultAsync();
            if (AccountToUpdate != null)
            {
                AccountToUpdate.AccountId = account.AccountId ;
                AccountToUpdate.AccountNumber  = account.AccountNumber;
                AccountToUpdate.FirstName  = account.FirstName ;
                AccountToUpdate.LastName  = account.LastName ;
                AccountToUpdate.Password  = account.Password ;
                AccountToUpdate.LastUpdatedDate = DateTime.Now;
                AccountToUpdate.EmailAddress = account.EmailAddress;

            }
             _context.Accounts.Update(account );
           await  _context.SaveChangesAsync();
        }
    }
}
