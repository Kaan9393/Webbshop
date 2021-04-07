using System.Threading.Tasks;
using DataAccess.Entities;
using Kladbutiken.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class PrivacyModel : PageModel
    {
        public User LoggedInAs { get; set; }

        public async Task OnGet()
        {
            var userDetailsCookie = Request.Cookies["UserDetails"];
            var cart = HttpContext.Session.GetString("cart");

            if (userDetailsCookie != null)
            {
                LoggedInAs = await UserCookieHandler.GetUserAndCartByCookies(userDetailsCookie, cart);
            }
        }
    }
}
