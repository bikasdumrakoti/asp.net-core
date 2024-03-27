using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            //return new ContentResult() { Content = "Hello from Index", ContentType = "text/plain" };

            //return Content("Hello from Index", "text/plain");

            return Content("<h1>Welcome</h1><h2>Hello from Index</h2>", "text/html");
        }

        [Route("person")]
        public JsonResult Person()
        {
            //return "{\"key\":\"value\"}";

            Person person = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Smith",
                LastName = "James",
                Age = 30
            };

            //return new JsonResult(person);

            return Json(person);
        }

        [Route("file-download1")]
        public VirtualFileResult FileDownload1()
        {
            //return new VirtualFileResult("/munamadan-book.pdf", "application/pdf");

            return File("/munamadan-book.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult(@"D:\aspnetcore\Section 5 Routing\resources\munamadan-book.pdf", "application/pdf");

            return PhysicalFile(@"D:\aspnetcore\Section 5 Routing\resources\munamadan-book.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] file = System.IO.File.ReadAllBytes(@"D:\aspnetcore\Section 5 Routing\resources\munamadan-book.pdf");
            //return new FileContentResult(file, "application/pdf");

            return File(file, "application/pdf");
        }
    }
}
