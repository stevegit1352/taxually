using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest.Core
{
    public class UkVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly ITaxuallyHttpClient _httpClient;

        public UkVatRegistrationStrategy(ITaxuallyHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool CanHandle(string country) => country == "GB";

        public Task RegisterAsync(VatRegistrationRequest request)
        {
            return _httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}