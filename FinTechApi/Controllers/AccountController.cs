using AutoMapper;
using FinTech.DB;
using FinTech.DB.DTO;
using FinTechCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinTechApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public  readonly IAcccountService _acccountService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public AccountController(IAcccountService acccountService, IMapper mapper, AppDbContext context)
        {
            _acccountService = acccountService;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        //[Route("Create_New_Account")]
        public IActionResult CreateNewBankAccount([FromBody] AccountDTo newAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(newAccount);
                var account = _mapper.Map<Account>(newAccount);
                var gAccount = new Account();
                account.AccountNumber = gAccount.GenerateAccountNumber();
               account.AccountName=gAccount.Fullname();
                return Ok(_acccountService.Create(account, newAccount.Password, newAccount.ConfirmPassword));

             }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
       // [Route("Get_All_Bank_Account")]
        public IActionResult GetAllBankAccount()
        {
            try
            {
                var account = _acccountService.GetAllAccounts();
                if (account == null)
                {
                    return NotFound("Bank Account not found");
                }

                // var cleanAccount = _mapper.Map<GetBankAccountDTo>(account );
                //var cleanAccount = new GetBankAccountDTo();

                return Ok(account);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Get_BankAccount_By_AccountNumber")]
        public IActionResult GetByWalletNumber(string accountNumber)
        {
            if (Regex.IsMatch(accountNumber, @"/^\d{10}$/"))
            {
                var account = _acccountService.GetAccountByAccountNumber(accountNumber);
                var cleanAccount = _mapper.Map<GetBankAccountDTo>(account );
                return Ok(cleanAccount);
            }
            return BadRequest("Account Number must be 10-digits");
        }
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            {
                var accountEmail = await _acccountService.GetByEmail(email);
                var cleanAccount = _mapper.Map<GetBankAccountDTo>(accountEmail);
                return Ok(cleanAccount);
            }
        }
        [HttpPut]
        [Route("Update_wallet")]
        public async Task<IActionResult> UpdateBankAccount([FromBody] UpdateAccountDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<Account>(model);
            await _acccountService.Update(account);
            return Ok(account);
        }
        [HttpDelete]
        [Route("Delete_By_AccountNumber")]
        public async Task<IActionResult >DeleteAccount(int accountNumber)
        {
            if (!ModelState.IsValid || accountNumber == 0) return BadRequest(ModelState);
            _acccountService.Delete(accountNumber);
            return  Ok();

        }
       
        [HttpGet]
        [Route("Get_All_BankAccount")]
        public async Task<ActionResult<List<Account>>> GetAccounts(int page)
        {
            if (_context.Accounts== null)
                return NotFound();
            var pageResult = 2f;
            var pageCount = Math.Ceiling(_context.Accounts .Count() / pageResult);
            var wallet = await _context.Accounts 
            .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult).ToListAsync();
            var responsonse = new Pagination<Account>
            {
                Translist = wallet,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(responsonse);

        }
    }
}
