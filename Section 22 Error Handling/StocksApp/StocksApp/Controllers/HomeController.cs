using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature is not null && exceptionHandlerPathFeature.Error is not null)
            {
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            }
            else
            {
                ViewBag.ErrorMessage = "Error encountered!";
            }
            return View();
        }
    }
}
