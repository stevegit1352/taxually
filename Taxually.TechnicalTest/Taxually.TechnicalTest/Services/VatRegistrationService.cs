using System;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Exceptions;
using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationService : IVatRegistrationService
    {
        private readonly IEnumerable<IVatRegistrationStrategy> _strategies;

        public VatRegistrationService(IEnumerable<IVatRegistrationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task RegisterAsync(VatRegistrationRequest request)
        {
            var strategy = _strategies.FirstOrDefault(s => s.CanHandle(request.Country));
            if (strategy == null)
                throw new UnsupportedCountryException();

            await strategy.RegisterAsync(request);
        }
    }
}