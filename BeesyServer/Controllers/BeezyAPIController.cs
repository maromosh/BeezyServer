using Microsoft.AspNetCore.Mvc;
using BeezyServer.Models;
using BeezyServer.DTO;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("registerBeekeeper")]
        public IActionResult RegisterBeekeeper([FromBody] DTO.BeeKeeper userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model user class
                Models.Beekeeper modelsBeekeeper = userDto.GetModel();

                context.Beekeepers.Add(modelsBeekeeper);
                context.SaveChanges();

                //User was added!
                DTO.BeeKeeper dtoUser = new DTO.BeeKeeper(modelsBeekeeper);
                //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //This method call the UpdateUser web API on the server and return true if the call was successful
        //or false if the call fails
        //[HttpPost("updateUser")]
        //public async Task<bool> UpdateUser(User user)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}updateuser";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(user);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        ////This method call the UploadProfileImage web API on the server and return the AppUser object with the given URL
        ////of the profile image or null if the call fails
        ////when registering a user it is better first to call the register command and right after that call this function
        //public async Task<User?> UploadProfileImage(string imagePath)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}uploadprofileimage";
        //    try
        //    {
        //        //Create the form data
        //        MultipartFormDataContent form = new MultipartFormDataContent();
        //        var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
        //        form.Add(fileContent, "file", imagePath);
        //        //Call the server API
        //        HttpResponseMessage response = await client.PostAsync(url, form);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            User? result = JsonSerializer.Deserialize<User>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}




    }




}
