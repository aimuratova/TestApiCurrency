using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestApiCurrency.BLL;

namespace TestApiCurrency.Controllers
{
    [Route("api/rate_usd_kzt")]
    [ApiController]
    public class CurrencyUsdKztController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RateUsdKztBll _rateCurrencyBll;

        public CurrencyUsdKztController(RateUsdKztBll rateUsdKztBll, IConfiguration configuration)
        {
            this._rateCurrencyBll = rateUsdKztBll;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rateVal = await _rateCurrencyBll.getRateUsdKzt();

            if (rateVal.HasValue)
            {
                return Ok(rateVal.Value);
            }
            else
            {
                return NotFound(new { error = "Не удалось получить курс валют" });
            }

        }
    }
}
