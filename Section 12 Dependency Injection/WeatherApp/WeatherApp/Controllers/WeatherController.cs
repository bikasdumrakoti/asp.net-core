using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceContracts;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<CityWeather> cityWeathers = _weatherService.GetWeatherDetails();
            return View(cityWeathers);
        }

        [Route("/weather/{cityCode?}")]
        public IActionResult Details(string? cityCode)
        {
            if (cityCode == null)
                return View("ErrorPage");
            CityWeather? cityWeather = _weatherService.GetWeatherByCityCode(cityCode);
            if (cityWeather == null)
                return View("ErrorPage");
            ViewData["Title"] = cityWeather.CityName + " City Weather";
            return View(cityWeather);
        }
    }
}
