using System;

namespace Taxually.TechnicalTest.Core
{
    public class VatRegistrationRequest
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }
    }
}