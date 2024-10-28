using Microsoft.AspNetCore.Mvc;
using BeezyServer.Models;

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
            Models.User? modelsUser = BeezyDbContext.GetUser(UserPassword.Email);

            // Check if user exists and if the password matches
            if (modelsUser == null || !VerifyPassword(loginDto.Password, modelsUser.UserPassword))
            {
                return Unauthorized("Access Denied: Invalid email or password.");
            }

            // Login succeeded! Store user information in session
            HttpContext.Session.SetString("loggedInUser", modelsUser.UserEmail);

            // Create a DTO for the response
            DTO.AppUser dtoUser = new DTO.AppUser(modelsUser)
            {
                ProfileImagePath = GetProfileImageVirtualPath(modelsUser.UserId) // Assuming you have a method for the profile image path
            };

            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    // Helper method for verifying passwords
    private bool VerifyPassword(string plainTextPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
    }

}
