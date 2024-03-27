using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            List<Person> persons = new List<Person>()
            {
                new Person(){Name="John",DateOfBirth=DateTime.Parse("2000-05-06"),PersonGender=Gender.Male},
                new Person(){Name="Linda",DateOfBirth=DateTime.Parse("2005-01-09"),PersonGender=Gender.Female},
                new Person(){Name="Susan",DateOfBirth=null,PersonGender=Gender.Other}
            };
            return View("Index", persons); //Views/Home/Index.cshtml
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if (name == null)
                return Content("Person name can't be null");
            List<Person> persons = new List<Person>()
            {
                new Person(){Name="John",DateOfBirth=DateTime.Parse("2000-05-06"),PersonGender=Gender.Male},
                new Person(){Name="Linda",DateOfBirth=DateTime.Parse("2005-01-09"),PersonGender=Gender.Female},
                new Person(){Name="Susan",DateOfBirth=null,PersonGender=Gender.Other}
            };
            Person? person = persons.Where(temp => temp.Name == name).FirstOrDefault();
            return View(person); //Views/Home/Details.cshtml
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            Person person = new Person()
            {
                Name = "Sara",
                PersonGender = Gender.Female,
                DateOfBirth = Convert.ToDateTime("2004-07-01")
            };
            Product product = new Product()
            {
                ProductId = 1,
                ProductName = "Air Conditioner"
            };
            PersonWithProductWrapperModel personWithProductWrapperModel = new PersonWithProductWrapperModel()
            {
                PersonData = person,
                ProductData = product
            };
            return View(personWithProductWrapperModel);
        }

        [Route("home/all")]
        public IActionResult All()
        {
            return View();
            //Views/Home/All.cshtml
            //Views/Shared/All.cshtml
        }
    }
}
