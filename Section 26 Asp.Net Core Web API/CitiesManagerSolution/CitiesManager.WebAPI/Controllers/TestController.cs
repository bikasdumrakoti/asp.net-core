using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    public class TestController : CustomControllerBase
    {
        [HttpGet]
        public string Method()
        {
            return "Hello World";
        }
    }
}
