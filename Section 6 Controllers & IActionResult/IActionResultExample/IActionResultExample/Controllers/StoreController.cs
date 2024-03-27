using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books/{id}")]
        public IActionResult Books()
        {
            int bookId = Convert.ToInt32(Request.RouteValues["id"]);
            return Content($"<h1>Book Store {bookId}</h1>", "text/html");
        }
    }
}
