using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        //private field
        private readonly WeatherApiOptions _weatherApiOptions;

        //constructor
        public HomeController(IOptions<WeatherApiOptions> weatherApiOptions)
        {
            _weatherApiOptions = weatherApiOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.ClientID = _weatherApiOptions.ClientID;
            ViewBag.ClientSecret = _weatherApiOptions.ClientSecret;

            return View();
        }
    }
}
