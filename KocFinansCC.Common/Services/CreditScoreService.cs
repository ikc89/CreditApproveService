namespace KocFinansCC.Common.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Abstract;

    public class CreditScoreService : ICreditScoreService
    {
        private HttpClient _httpClient;

        public CreditScoreService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public int GetCreditScore(string citizenNo)
        {
            // Commented intentionally
            // var response = _httpClient.GetAsync($"/creditScore?citizenNo={citizenNo}");
            var random = new Random();
            return random.Next(0, 2000);
        }
    }
}
