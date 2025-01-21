using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SPHealthSupportSystem_MVCWebApp.Models;

namespace SPHealthSupportSystem_MVCWebApp.Controllers
{
    public class UserAccountsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        private string APIEndPoint = "https://localhost:7254/api/";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);  // Return the login model to show validation errors
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "UserAccounts/Login", login))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var tokenString = await response.Content.ReadAsStringAsync();

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                            if (jwtToken != null)
                            {
                                var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                                if (userName != null && role != null)
                                {
                                    var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, userName),
                                new Claim(ClaimTypes.Role, role),
                            };

                                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                                    // Secure cookies in production
                                    Response.Cookies.Append("UserName", userName, new CookieOptions { HttpOnly = true, Secure = true });
                                    Response.Cookies.Append("Role", role, new CookieOptions { HttpOnly = true, Secure = true });

                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid login attempt.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            // If we reach here, something went wrong
            return View(login);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "UserAccounts");
        }

        public async Task<IActionResult> Forbidden()
        {
            return View();
        }
    }
}
