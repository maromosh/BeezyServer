using Microsoft.AspNetCore.Mvc;
using BeezyServer.Models;
using BeezyServer.DTO;

namespace BeezyServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class BeezyAPIController : ControllerBase
    {
        //a variable to hold a reference to the db context!
        private BeezyDbContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public BeezyAPIController(BeezyDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }

        [HttpGet]
        [Route("TestServer")]
        public ActionResult<string> TestServer()
        {
            return Ok("Server Responded Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginInfo loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); // Logout any previous login attempt

                // Get the user from the database using the email
                Models.User? modelsUser = context.GetUser(loginDto.Email);

                // Check if user exists and if the password matches
                if (modelsUser == null || modelsUser.UserPassword != loginDto.Password)
                {
                    return Unauthorized("Access Denied: Invalid email or password.");
                }

                // Login succeeded! Store user information in session
                HttpContext.Session.SetString("loggedInUser", modelsUser.UserEmail);

                // Create a DTO for the response
                DTO.User dtoUser = new DTO.User(modelsUser);
                //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        //// Helper method for verifying passwords
        //private bool VerifyPassword(string plainTextPassword, string hashedPassword)
        //{
        //    return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        //}

        [HttpPost("register")]
        public IActionResult Register([FromBody] DTO.User userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model user class
                Models.User modelsUser = userDto.GetModel();

                context.Users.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.User dtoUser = new DTO.User(modelsUser);
                //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

   


}
