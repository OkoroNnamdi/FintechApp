using AspNetCoreHero.Results;
using FinTech.DB;
using FinTechCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static FinTech.DB.Transaction;

namespace FinTechCore.Implementations
{
    public class TransactionServices : ITransaction
    {
        public  readonly ICurrencyAPI _currencyAPI;
        public  readonly IAcccountService _acccountService;
        private readonly AppDbContext _context;
        private readonly ILogger<TransactionServices> _logger;
        public TransactionServices(ICurrencyAPI currencyAPI, IAcccountService acccountService, AppDbContext context, ILogger<TransactionServices> logger)
        {
            _currencyAPI = currencyAPI;
            _acccountService = acccountService;
            _context = context;
            _logger = logger;
        }
        public async Task<Response> CheckAccountBalance(string accountNumber, string currency)
        {
            var response = new Response();
            var getAccount = _acccountService.GetAccountByAccountNumber(accountNumber );
            if (getAccount == null)
                throw new ApplicationException("Invalid Credential");
            var curr = getAccount.Amount * await _currencyAPI.GetApiAsync(currency);
            response.ResponseMessage = $"Your Balance {currency.ToUpper()} is {curr} ";
            response.ResponseCode = "00";

            return response;
        }
        public async  Task<Response> CreateNewTransaction(Transaction transactions)
        {
            Response response = new Response();
            await _context .Transactions.AddAsync(transactions);
            await _context .SaveChangesAsync();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction create sucessful";
            response.Data = null;
            return response;
        }

        public async  Task<Response> FindTransactionByDate(DateTime dateTime)
        {
            Response response = new Response();
            var transaction = await _context.Transactions.Where(x => x.TransactionDate == dateTime).ToListAsync();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction create sucessful";
            response.Data = transaction;
            return response;

        }

        public async  Task<Response> MakeDeposit(string accountNumber, double amount, string TransactionPin)
        {
            Response response = new Response();
            //Wallet sourceAccount;
            Account destinationAccount;

            Transaction transaction = new Transaction();
            try
            {
                if (string.IsNullOrWhiteSpace(accountNumber))
                    throw new ApplicationException("Invalid transaction credentials");
                if (amount < 0)
                    throw new ApplicationException("Invalid transaction amount");
                //sourceAccount = this.walletServices.GetWalletByWalletNumber(_OurWalletStatementAccount);
                destinationAccount = this._acccountService.GetAccountByAccountNumber(accountNumber );
                destinationAccount.Amount += amount;
                if ((_context.Entry(destinationAccount).State == EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction sucessful";
                    response.Data = null;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseMessage = "Transaction fail";
                    response.ResponseCode = "02";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                return new Response() {  ResponseMessage = ex.Message };
            }
            transaction.TransactionType = Transtype.Deposit;
            transaction.TransactionDestinationAccount = accountNumber ;
            transaction.TransactionAmount = (decimal)amount;
            transaction.TransactionDate = DateTime.Now;

            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
                $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
                $"ON DATE => {transaction.TransactionDate}" +
                $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
                $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
                $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            await _context .Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return response;
        }
        public async  Task<Response> MakeTransfer(string fromWallet, string toWallet, double amount, string TransactionPin)
        {

            Response response = new Response();
            Account sourceAccount, destinationAccount;
            Transaction transaction = new Transaction();

            try
            {
                if (string.IsNullOrWhiteSpace(fromWallet) || string.IsNullOrWhiteSpace(toWallet))
                    throw new ApplicationException("Invalid transaction credentials");
                if (amount < 0)
                    throw new ApplicationException("Invalid transaction amount");
                sourceAccount = _acccountService.GetAccountByAccountNumber(fromWallet);
                destinationAccount = _acccountService.GetAccountByAccountNumber(toWallet);
                if (sourceAccount.Amount < 1000)
                    throw new Exception("Insufficient Amount");
                if (sourceAccount.Amount < amount)
                    throw new ApplicationException("Insufficient transaction Amount");
                sourceAccount.Amount -= amount;
                destinationAccount.Amount += amount;
                if ((_context.Entry(sourceAccount).State == EntityState.Modified) &&
                        (_context.Entry(destinationAccount).State == EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction sucessful";
                    response.Data = null;
                    
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseMessage = "Transaction fail";
                    response.ResponseCode = "02";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseMessage =$"Transaction failed {ex.Message}" ,
                };
            }
            transaction.TransactionType = Transtype.Transfer;
            transaction.TransactionSourceAccount = fromWallet;
            transaction.TransactionDestinationAccount = toWallet;
            transaction.TransactionAmount = (decimal)amount;
            transaction.TransactionDate = DateTime.Now;

            transaction.TransactionParticulars = $"NEW TRANSACTION FRO" +
            $"M SOURCE => " +
            $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
            $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
            $"ON DATE => {transaction.TransactionDate}" +
            $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
            $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
            $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            
        return response;

    } public async  Task<Response> MakeWithdrawal(string accountNumber, double amount, string TransactionPin)
        {
            Response response = new Response();
            Account  sourceAccount;
            Transaction transaction = new Transaction();
            var authUser = this._acccountService.Authenticate(accountNumber, TransactionPin);
            if (authUser == null)
                throw new ApplicationException("Invalid credentials");
            try
            {
                sourceAccount = _acccountService.GetAccountByAccountNumber(accountNumber);
                sourceAccount.Amount -= amount;

                if ((_context.Entry(sourceAccount).State == EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction sucessful";
                    response.Data = null;


                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseMessage = "Transaction fail";
                    response.ResponseCode = "02";
                    response.Data = null;
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError($"AN ERROR OCCURRED=> {ex.Message}");
            }
            transaction.TransactionType = Transtype.Withdraw;
            //transaction.TransactionSourceAccount = _OurWalletStatementAccount;
            transaction.TransactionDestinationAccount = accountNumber;
            transaction.TransactionAmount = (decimal)amount;
            transaction.TransactionDate = DateTime.Now;

            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
                $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
                $"ON DATE => {transaction.TransactionDate}" +
                $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
                $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
                $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            await _context .Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return response;
        }
    }
}
