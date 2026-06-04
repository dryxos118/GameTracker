using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Web.Controllers;

[Route("culture")]
public class CultureController : Controller
{
    [HttpGet("set-culture")]
    public IActionResult SetCulture(string? culture, string redirectUri = "/")
    {
        if (!string.IsNullOrWhiteSpace(culture))
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture, culture)));
        }

        return LocalRedirect(redirectUri);
    }
}