using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserBackend.DTO;
using UserBackend.Helpers;
using UserBackend.Models;
using UserBackend.Repositories;

namespace UserBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        //Register the User and Encrypt Password
        [HttpPost]
        [Route("register")]
        [SwaggerOperation(
            Summary = "Create a Users",
            Description = "Create Users",
            OperationId = "Register"
        )]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                //Hashin Password with bcrypt
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };


            return Created("succes", _userRepository.Create(user));
        }


        //Login In
        [HttpPost]
        [Route("login")]
        [SwaggerOperation(
            Summary = "Login to the App",
            Description = "Login in",
            OperationId = "Login"
        )]
        public IActionResult Login(LoginDto loginDto) 
        {
            var user = _userRepository.GetByEmail(loginDto.Email);
            //In cas of the user dont exist
            if (user == null) return BadRequest(new {message = "Invalid Credentials"});
            //In case of bad paswword
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) 
            {
                return BadRequest(new { message = "Invalid Password" });
            }
            //This is were the Jwt is encoded
            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly= true
            });

            return Ok(new
                {
                message = "success"
            });
        }

        //Getting the Authenticated User from the cookie
        [HttpGet]
        [Route("user")]
        [SwaggerOperation(
            Summary = "Getting the Authenticated User",
            Description = "Get the User",
            OperationId = "User"
        )]
        public IActionResult User(string username, string password) 
        {
            try
            {
                //Get the User from the JWT token sent in the cookies
                var jwt = Request.Cookies["jwt"];

                //Getting the Token
                var token = _jwtService.Verify(jwt);

                //Get the user Id
                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetById(userId);

                return Ok(user);
            }catch(Exception ex) 
            {
                return Unauthorized();
            }
        }

        //LogOut
        [HttpPost]
        [Route("logout")]
        [SwaggerOperation(
            Summary = "Getting the Authenticated User to Log Out",
            Description = "Logout",
            OperationId = "Logout"
        )]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new 
            {
                message = "success" 
            });
        }
    }
}
