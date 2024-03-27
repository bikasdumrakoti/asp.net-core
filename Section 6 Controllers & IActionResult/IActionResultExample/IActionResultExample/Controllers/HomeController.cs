using IActionResultExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{bookid?}/{isloggedin?}")]
        //URL: /bookstore?bookid=5&isloggedin=true
        public IActionResult Index(int? bookid, [FromRoute] bool? isloggedin, Book book)
        {
            //book id should be supplied
            if (bookid.HasValue == false)
            {
                return BadRequest("Book id is not supplied or empty");
            }

            //book id can't be less than or equal to 0
            if (bookid <= 0)
            {
                return BadRequest("Book id can't be less than or equal to 0");
            }

            //book id should be between 1 and 1000
            if (bookid > 1000)
            {
                return NotFound("Book id can't be greater than 1000");
            }

            //isloggedin should be true
            if (isloggedin == false)
            {
                return StatusCode(401);
            }

            return Content($"Book id is {bookid}, Book: {book}", "text/plain");
        }
    }
}
