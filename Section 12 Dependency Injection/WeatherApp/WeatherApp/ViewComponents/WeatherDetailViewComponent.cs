using Microsoft.AspNetCore.Mvc;
using Models;

namespace WeatherApp.ViewComponents
{
    public class WeatherDetailViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather cityWeather)
        {
            return View(cityWeather);
        }
    }
}
