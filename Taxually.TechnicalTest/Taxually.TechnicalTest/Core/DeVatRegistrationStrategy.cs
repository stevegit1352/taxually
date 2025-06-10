using System.Xml.Serialization;
using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest.Core
{
    public class DeVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly ITaxuallyQueueClient _queueClient;

        public DeVatRegistrationStrategy(ITaxuallyQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public bool CanHandle(string country) => country == "DE";

        public Task RegisterAsync(VatRegistrationRequest request)
        {
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            using var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, request);
            var xml = stringWriter.ToString();
            return _queueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}