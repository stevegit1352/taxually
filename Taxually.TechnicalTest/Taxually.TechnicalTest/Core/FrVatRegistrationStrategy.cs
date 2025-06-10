using System.Text;
using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest.Core
{
    public class FrVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly ITaxuallyQueueClient _queueClient;

        public FrVatRegistrationStrategy(ITaxuallyQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public bool CanHandle(string country) => country == "FR";

        public Task RegisterAsync(VatRegistrationRequest request)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return _queueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}