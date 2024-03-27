using ECommerceOrdersApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrdersApp.Controllers
{
    public class OrdersController : Controller
    {
        [Route("order")]
        public IActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
                HttpContext.Response.StatusCode = 400;
                return Content(errors);
            }
            Random random = new();
            var randomNumber = random.Next(1, 99999);
            var result = new { OrderNo = randomNumber };
            return Json(result);
        }
    }
}
