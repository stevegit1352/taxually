using Taxually.TechnicalTest.Core;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface IVatRegistrationStrategy
    {
        bool CanHandle(string country);
        Task RegisterAsync(VatRegistrationRequest request);
    }
}