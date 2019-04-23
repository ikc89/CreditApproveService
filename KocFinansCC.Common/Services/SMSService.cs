namespace KocFinansCC.Common.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Abstract;

    public class SMSService : ISMSService
    {
        private HttpClient _httpClient;

        public SMSService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool SendSMS(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            // Commented intentionally
            // var response = _httpClient.GetAsync($"/sendSMS?phoneNumber={phoneNumber}");
            var random = new Random();
            return Convert.ToBoolean(random.Next(0, 1));
        }
    }
}
