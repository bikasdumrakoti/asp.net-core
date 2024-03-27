using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BankApp.Controllers
{
    public class BankAccountController : Controller
    {
        private object _accountDetails;

        public BankAccountController()
        {
            _accountDetails = new
            {
                accountNumber = 1001,
                accountHolderName = "James Smith",
                currentBalance = 5000
            };
        }

        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank", "text/plain");
        }

        [Route("account-details")]
        public IActionResult AccountDetails()
        {
            return Json(_accountDetails);
        }

        [Route("account-statement")]
        public VirtualFileResult AccountStatement()
        {
            return File("/bank-statement.pdf", "application/pdf");
        }

        [Route("get-current-balance")]
        public IActionResult Return404NotFound()
        {
            return NotFound("Account Number should be supplied");
        }

        [Route("get-current-balance/{accountNumber}")]
        public IActionResult GetCurrentBalance()
        {
            string? accountNumber = Convert.ToString(ControllerContext.HttpContext.Request.RouteValues["accountNumber"]);
            if (!int.TryParse(accountNumber, out _))
            {
                return BadRequest("Account Number should be an integer value");
            }
            if (Convert.ToInt32(accountNumber) != 1001)
            {
                return BadRequest("Account Number should be 1001");
            }
            Type type = _accountDetails.GetType();
            PropertyInfo? propertyInfo = type.GetProperty("currentBalance");
            int currentBalance = Convert.ToInt32(propertyInfo?.GetValue(_accountDetails) ?? 0);
            return Content(currentBalance.ToString(), "text/plain");
        }
    }
}
