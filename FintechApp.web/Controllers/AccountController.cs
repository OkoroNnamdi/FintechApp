using System;
using System.Net.Http;
using System.Collections.Generic;
using FintechApp.web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FintechApp.web.Controllers
{
    public class AccountController : Controller
    {
        Uri basaAddress = new Uri("https://localhost:7074/api");
        private readonly HttpClient _client;
       
        public AccountController(HttpClient client)
        {
            _client = new HttpClient();
            _client.BaseAddress = basaAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<AccountViewModel> accounts = new List<AccountViewModel>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress
                    + "/Account/GetAllBankAccount").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    accounts = JsonConvert.DeserializeObject<List<AccountViewModel>>(data);
                }
                return View(accounts);
            }
            catch (Exception)
            {

                return View();
            }
           
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AccountViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(basaAddress + "/Account/CreateNewBankAccount", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Bank Account Created";
                  return  RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"]= ex.Message;
                return View();
            }
            return View();
        }
    }
}
