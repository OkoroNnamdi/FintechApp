using FinTech.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Interfaces
{
    public  interface ITransaction
    {
        Task<Response> CreateNewTransaction(Transaction transactions);
        Task<Response> FindTransactionByDate(DateTime dateTime);
        Task<Response> MakeDeposit(string accountNumber, double amount, string TransactionPin);
        Task<Response> MakeWithdrawal(string accountNumber, double amount, string TransactionPin);
        Task<Response> MakeTransfer(string fromWallet, string toWallet, double amount, string TransactionPin);
        Task<Response> CheckAccountBalance(string accountNumber, string currency);
    }
}
