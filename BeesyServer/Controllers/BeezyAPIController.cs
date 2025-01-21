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

                //Check if the user is beekeeper
                Beekeeper? b = context.GetBeekeeper(modelsUser.UserId);
                if (b != null)
                {
                    b.BeeKeeper = modelsUser;
                    // Create a DTO for the response
                    DTO.BeeKeeper dtoBeekeeper = new DTO.BeeKeeper(b);
                    dtoBeekeeper.ProfileImagePath = GetProfileImageVirtualPath(dtoBeekeeper.UserId);
                    return Ok(dtoBeekeeper);
                }
                    

                // Create a DTO for the response
                DTO.User dtoUser = new DTO.User(modelsUser);
                dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
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
                dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
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
                dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        //This method call the UpdateUser web API on the server and return true if the call was successful
        //or false if the call fails
        [HttpPost("updateUser")]
        public IActionResult UpdateUser([FromBody] DTO.User userDto)
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Get model user class from DB with matching email. 
                Models.User? user = context.GetUser(userEmail);
                //Clear the tracking of all objects to avoid double tracking
                context.ChangeTracker.Clear();

                //Check if the user that is logged in is the same user of the task
                //this situation is ok only if the user is a manager
                if (user == null || (userDto.UserId != user.UserId))
                {
                    return Unauthorized("User is trying to update a different user");
                }

                Models.User u = userDto.GetModel();

                context.Entry(u).State = EntityState.Modified;

                context.SaveChanges();

                //Task was updated!
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("updateBeekeeper")]
        public IActionResult UpdateBeekeeper([FromBody] DTO.BeeKeeper userDto)
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Get model user class from DB with matching email. 
                Models.User? user = context.GetUser(userEmail);
                //Clear the tracking of all objects to avoid double tracking
                // Ensure the logged-in user is the same as the one trying to update the beekeeper
                if (userDto.UserId != user.UserId)
                {
                    return Unauthorized("You can only update your own beekeeper information.");
                }

                // Get the beekeeper from the database
                Models.Beekeeper? beekeeper = context.Beekeepers.FirstOrDefault(b => b.BeeKeeperId == user.UserId);
                if (beekeeper == null)
                {
                    return NotFound("Beekeeper not found for this user.");
                }

                // Update beekeeper fields
                beekeeper.BeekeeperKind = userDto.BeekeeperKind;
                beekeeper.BeekeeperRadius = userDto.BeekeeperRadius;
                beekeeper.BeekeeperIsActive = userDto.BeekeeperIsActive;

                // Save the changes
                context.SaveChanges();

                // Return the updated beekeeper as a DTO
                DTO.BeeKeeper updatedBeekeeper = new DTO.BeeKeeper(beekeeper);
                updatedBeekeeper.ProfileImagePath = GetProfileImageVirtualPath(updatedBeekeeper.UserId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            Models.User? user = context.GetUser(userEmail);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (user == null)
            {
                return Unauthorized("User is not found in the database");
            }


            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    return BadRequest("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.UserId}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                    }

                }

            }

            DTO.User dtoUser = new DTO.User(user);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
            return Ok(dtoUser);
        }

        //Helper functions

        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }

        //this function check which profile image exist and return the virtual path of it.
        //if it does not exist it returns the default profile image virtual path
        private string GetProfileImageVirtualPath(int userId)
        {
            string virtualPath = $"/profileImages/{userId}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/profileImages/default.png";
                }
            }

            return virtualPath;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Read all users

                List<Models.User> list = context.GetUsers();

                List<DTO.User> users = new List<DTO.User>();

                foreach (Models.User u in list)
                {
                    DTO.User user = new DTO.User(u);

                    users.Add(user);
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllReports")]
        public IActionResult GetAllReports()
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Read all posts

                List<Models.Report> list = context.GetReports();

                List<DTO.Report> allReports = new List<DTO.Report>();

                foreach (Models.Report p in list)
                {
                    DTO.Report report = new DTO.Report(p);

                    allReports.Add(report);
                }
                return Ok(allReports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}


