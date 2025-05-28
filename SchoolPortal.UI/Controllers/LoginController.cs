using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.CustomModels;
using System;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolPortal.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private const int MaxLoginAttempts = 3;
        private const int LockoutMinutes = 15;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("OcelotClient");
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginDto loginInput)
        {
            if (loginInput == null)
            {
                ModelState.AddModelError(string.Empty, "Login information is required.");
                return View(new LoginDto());
            }

            if (!ModelState.IsValid)
            {
                return View(loginInput);
            }

            // Get login attempts and lockout data from session
            var loginAttempts = HttpContext.Session.GetInt32("LoginAttempts") ?? 0;
            var lockoutEndTimeStr = HttpContext.Session.GetString("LockoutEndTime");

            if (!string.IsNullOrEmpty(lockoutEndTimeStr) &&
                DateTime.TryParse(lockoutEndTimeStr, out var lockoutEndTime) &&
                lockoutEndTime > DateTime.UtcNow)
            {
                var remainingMinutes = Math.Ceiling((lockoutEndTime - DateTime.UtcNow).TotalMinutes);
                ModelState.AddModelError(string.Empty, $"Account is locked. Please try again after {remainingMinutes} minutes.");
                return View(loginInput);
            }

            try
            {
                //http://localhost:5000/api/v1/userdetails/authenticate
                var response = await _httpClient.PostAsJsonAsync("api/v1/userdetails/session", loginInput);

                if (response.IsSuccessStatusCode)
                {
                    var sessionData = await response.Content.ReadFromJsonAsync<SessionModel>();
                    if (sessionData != null)
                    {
                        // Reset login attempts and lockout time
                        HttpContext.Session.Remove("LoginAttempts");
                        HttpContext.Session.Remove("LockoutEndTime");

                        // Store session data
                        HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(sessionData));

                        // Redirect to Home page after successful login
                        return RedirectToAction("Index", "Country");
                    }
                }

                // Increment login attempts on failure
                loginAttempts++;
                HttpContext.Session.SetInt32("LoginAttempts", loginAttempts);

                if (loginAttempts >= MaxLoginAttempts)
                {
                    var lockoutTime = DateTime.UtcNow.AddMinutes(LockoutMinutes);
                    HttpContext.Session.SetString("LockoutEndTime", lockoutTime.ToString("o"));
                    ModelState.AddModelError(string.Empty, $"Too many failed attempts. Account is locked for {LockoutMinutes} minutes.");
                }
                else
                {
                    var remaining = MaxLoginAttempts - loginAttempts;
                    ModelState.AddModelError(string.Empty, $"Invalid username or password. {remaining} attempts remaining.");
                }

                return View(loginInput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed due to unexpected error.");
                ModelState.AddModelError(string.Empty, "An error occurred while trying to log in. Please try again later.");
                return View(loginInput);
            }
        }

        public IActionResult Logout()
        {
            // Clear session data on logout
            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.Remove("LoginAttempts");
            HttpContext.Session.Remove("LockoutEndTime");

            // Redirect to Login page after logout
            return RedirectToAction("Index", "Login");
        }
    }
}
