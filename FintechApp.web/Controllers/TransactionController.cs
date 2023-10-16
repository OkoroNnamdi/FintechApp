using FintechApp.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FintechApp.web.Controllers
{
    public class TransactionController : Controller
    {
        Uri basaAddress = new Uri("https://localhost:7074/api");
        private readonly HttpClient _client;

        public TransactionController(HttpClient client)
        {
            _client = new HttpClient();
            _client.BaseAddress = basaAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress
                + "/Transaction/GetTransactions").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                transactions = JsonConvert.DeserializeObject<List<TransactionViewModel>>(data);
            }
            return View(transactions);
        }
    }
}
