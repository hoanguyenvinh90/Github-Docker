using Microsoft.AspNetCore.Mvc;

namespace Automobile.Web.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Login()
        {
            var redirectUrl = Url.Content("~/");
            return View();
        }
    }
}
