﻿using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly List<CityWeather> cityWeathers = new()
        {
            new CityWeather(){CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
            new CityWeather(){CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60},
            new CityWeather(){CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82}
        };

        [Route("/")]
        public IActionResult Index()
        {
            return View(cityWeathers);
        }

        [Route("/weather/{cityCode?}")]
        public IActionResult Details(string? cityCode)
        {
            if (cityCode == null)
                return View("ErrorPage");
            CityWeather? cityWeather = cityWeathers.Where(weather => weather.CityUniqueCode == cityCode).FirstOrDefault();
            if (cityWeather == null)
                return View("ErrorPage");
            ViewData["Title"] = cityWeather.CityName + " City Weather";
            return View(cityWeather);
        }
    }
}
