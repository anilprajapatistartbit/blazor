using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HMS.Pages
{
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string paramUsername, bool paramIsremember, string paramLoginType,string paramName, string paramId)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email, paramUsername),
                                    new Claim(ClaimTypes.Name, paramName),
                                    new Claim(ClaimTypes.UserData, paramId),
                                    new Claim(ClaimTypes.Role, paramLoginType),
                                };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = paramIsremember,
                RedirectUri = this.Request.Host.Value,
                ExpiresUtc= DateTimeOffset.UtcNow.AddHours(2),
            };
            try
            {
                
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            if (paramLoginType.ToLower() == "patient")
            {
                returnUrl = Url.Content("~/Appointments");
            }
            return LocalRedirect(returnUrl);
        }
    }
}
