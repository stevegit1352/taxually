using Taxually.TechnicalTest.Core;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface IVatRegistrationService
    {
        Task RegisterAsync(VatRegistrationRequest request);
    }
}