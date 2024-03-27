using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaLinks.Options;

namespace SocialMediaLinks.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly IOptions<SocialMediaLinksOptions> _socialMediaLinksOptions;

        public SocialMediaController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions)
        {
            _socialMediaLinksOptions = socialMediaLinksOptions;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Facebook = _socialMediaLinksOptions.Value.Facebook;
            ViewBag.Instagram = _socialMediaLinksOptions.Value.Instagram;
            ViewBag.Twitter = _socialMediaLinksOptions.Value.Twitter;
            ViewBag.Youtube = _socialMediaLinksOptions.Value.Youtube;
            return View();
        }
    }
}
