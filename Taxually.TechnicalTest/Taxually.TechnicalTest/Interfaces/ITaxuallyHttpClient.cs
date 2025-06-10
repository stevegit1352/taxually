using Taxually.TechnicalTest.Core;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface ITaxuallyHttpClient
    {
        Task PostAsync(string url, VatRegistrationRequest request);
    }
}