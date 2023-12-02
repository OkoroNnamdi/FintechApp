using FinTech.DB.DTO;
using FinTechCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinTechCore.Implementations
{
    public class CurrentApiService : ICurrencyAPI
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CurrentApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<double> GetApiAsync(string currency)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                using (var response = await httpClient.GetAsync("https://open.er-api.com/v6/latest/NGN", HttpCompletionOption.ResponseHeadersRead))
                {
                    var con = currency.ToUpper();
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var rates = await JsonSerializer.DeserializeAsync<ApiDto>(stream);
                    if (rates != null)
                        return rates.rates[con];
                }
            }
            catch (Exception)
            {
                throw;

            }
            return 0;
        }
    }
}
