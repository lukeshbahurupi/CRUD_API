using CRUD_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CRUD_API.Models;
using System.Security.Claims;

namespace CRUD_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _apiClient;

        public HomeController()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri("http://localhost:57226/");
        }

        // Your action method that makes use of the API
        public async Task<ActionResult> Index()
        {
            // Call the login API endpoint
            var loginModel = new LoginModel { UserName = "Admin1", Password = "Admin@123" };
            var loginResponse = await _apiClient.PostAsJsonAsync("api/auth/login", loginModel);

            if (loginResponse.IsSuccessStatusCode)
            {
                // Read the token from the response
                var token = await loginResponse.Content.ReadAsAsync<TokenResponse>();

                // Use the token in subsequent requests
                _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                

                // Call the secure-action API endpoint
                var secureActionResponse = await _apiClient.GetAsync("api/auth/secure-action");

                if (secureActionResponse.IsSuccessStatusCode)
                {
                    var secureActionResult = await secureActionResponse.Content.ReadAsAsync<SecureActionResponse>();
                    // Do something with the secure action result
                    return View(secureActionResult);
                }
                else
                {
                    // Handle unsuccessful secure-action API call
                    return View("Error");
                }
            }
            else
            {
                // Handle unsuccessful login API call
                return View("Error");
            }
        }
    }

    // Define your models for API responses
    public class TokenResponse
    {
        public string Token { get; set; }
    }

    public class SecureActionResponse
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }

}
