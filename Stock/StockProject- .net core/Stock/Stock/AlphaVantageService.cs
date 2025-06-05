
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class AlphaVantageService
    {
        private readonly HttpClient _httpClient;

        public AlphaVantageService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<StockInfo> GetStockDataAsync(string symbol)
        {
            string apiKey = "a54138e121ca417bb7dbd91f315f910b"; // המפתח שלך ל־TwelveData
            //string apiUrl = $"https://api.twelvedata.com/time_series?symbol={symbol}&interval=1min&apikey={apiKey}";
            string apiUrl = $"https://api.twelvedata.com/time_series?symbol={symbol}&interval=1day&outputsize=365&apikey={apiKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                var stockInfo = JsonConvert.DeserializeObject<StockInfo>(response);

                if (stockInfo == null || stockInfo.Values == null || stockInfo.Values.Count == 0)
                {
                    throw new Exception("No data found in the response.");
                }

                return stockInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving stock data: " + ex.Message);
            }
        }
    }
}

