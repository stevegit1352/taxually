using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Exceptions;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationWebApiController : ControllerBase
    {
        private readonly IVatRegistrationService _vatRegistrationService;

        public VatRegistrationWebApiController(IVatRegistrationService vatRegistrationService)
        {
            _vatRegistrationService = vatRegistrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            try
            {
                await _vatRegistrationService.RegisterAsync(request);
                return Ok();
            }
            catch (UnsupportedCountryException)
            {
                return BadRequest("Country not supported");
            }
        }
    }
}
