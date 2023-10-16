using AutoMapper;
using FinTech.DB;
using FinTech.DB.DTO;
using FinTechCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinTechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public readonly IAcccountService _acccountService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ITransaction _transactions;
        public TransactionController(ITransaction transactions,IAcccountService acccountService, IMapper mapper, AppDbContext context)
        {
            _transactions = transactions;
            _acccountService = acccountService;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        [Route("Create_New_Transaction")]
        public async Task<IActionResult> Createnewtransaction([FromBody] TransactionDTO transationRequest)
        {
            if (!ModelState.IsValid) return BadRequest(transationRequest);
            var transaction = _mapper.Map<Transaction>(transationRequest);
            return Ok(await _transactions.CreateNewTransaction(transaction));

        }
        [HttpPost]
        [Route("Make_Deposit")]
        public async Task<IActionResult> MakeDeposit(string walletNumber, double amount, string TransactionPin)
        {
            if (!Regex.IsMatch(walletNumber, @"(?<!\d)\d{10}(?!\d)")) return BadRequest("Wallet Number must be 10-digit");
            return Ok(await _transactions.MakeDeposit(walletNumber, amount, TransactionPin));
        }

        [HttpPost]
        [Route("Make_Withdraw")]
        public async Task<IActionResult> MakeWithraw(string walletNumber, double amount, string TransactionPin)
        {
            if (!Regex.IsMatch(walletNumber, @"(?<!\d)\d{10}(?!\d)")) return BadRequest("Wallet Number must be 10-digit");
            return Ok(await _transactions.MakeWithdrawal(walletNumber, amount, TransactionPin));
        }
        [HttpPost]
        [Route("Make_Fund_Transfer")]
        public async Task<IActionResult> MakeTransfer(string fromWallet, string toWallet, double amount, string TransactionPin)
        {
            if (!Regex.IsMatch(fromWallet, @"(?<!\d)\d{10}(?!\d)") || !Regex.IsMatch(toWallet, @"(?<!\d)\d{10}(?!\d)")) return BadRequest("Wallet Number must be 10-digit");
            return Ok(await _transactions.MakeTransfer(fromWallet, toWallet, amount, TransactionPin));
        }
        [HttpGet]
      //  [Route("Get_All_Transaction")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            if (_context.Transactions == null)
                return NotFound();
            var transactionList =await  _context.Transactions.ToListAsync();
            //var pageResult = 2f;
            //var pageCount = Math.Ceiling(_context.Transactions.Count() / pageResult);
            //var transactions = await _context.Transactions
            //.Skip((int)((page - 1) * (int)pageResult))
            //    .Take((int)pageResult).ToListAsync();
            //var responsonse = new Pagination<Transaction>
            //{
            //    Translist = transactions,
            //    CurrentPage = (int)page,
            //    Pages = (int)pageCount
            //};
            return Ok( transactionList );
        }
        [HttpGet]
        [Route("Check_Account_Balance")]
        public async Task<IActionResult> CheckBalance(string walletNumber, string Currency)
        {
            if (!Regex.IsMatch(walletNumber, @"(?<!\d)\d{10}(?!\d)")) return BadRequest("Wallet Number must be 10-digit");
            return Ok(await _transactions.CheckAccountBalance(walletNumber, Currency));
        }
    }
}
