using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace CRUD_API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private ApplicationDbContext DB = new ApplicationDbContext();

        [Route("login")]
        public async Task<IHttpActionResult> PostLoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                bool isValidUser = await ValidateUserAsync(loginModel.UserName, loginModel.Password);

                if (isValidUser)
                {
                    bool isAdmin = await CheckIfUserIsAdminAsync(loginModel.UserName);

                    // Generate JWT token
                    string token = JwtTokenUtility.GenerateToken(loginModel.UserName, isAdmin);
                     
                    return Ok(new { Token = token });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Unauthorized();
        }

        private async Task<bool> ValidateUserAsync(string username, string password)
        {
            await Task.Delay(100);
            var user = await DB.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user != null;
        }

        private async Task<bool> CheckIfUserIsAdminAsync(string username)
        {
            await Task.Delay(100);
            var user = await DB.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null && user.IsAdmin;
        }

        //[Authorize(Roles = "Admin")]
        [Route("secure-action")]
        public async Task<HttpResponseMessage> GetSecureAction()
        {
            try
            {
                await Task.Delay(100);
                // Access user information using User.Identity.Name, etc.
                string username = User.Identity.Name;
                bool isAdmin = User.IsInRole("Admin");

                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "This is a secure action.", Username = username, IsAdmin = isAdmin });
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message + "An error occurred while processing the request." });
            }
        }
    }

}
