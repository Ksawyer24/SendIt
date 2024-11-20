using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendIt.Auth;
using SendIt.Dto;
using SendIt.Repo;

namespace SendIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepo tokenRepo;

        public AuthController(UserManager<User> userManager, ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }




        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignUpRequestDto signUpRequest)
        {
            var identityuser = new User
            {
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email,
                // PhoneNumber = signUpRequest.PhoneNumber,
                FirsttName = signUpRequest.FirsttName,
               
            };


            var identityResult = await userManager.CreateAsync(identityuser, signUpRequest.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this user

                if (signUpRequest.Roles != null && signUpRequest.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityuser, signUpRequest.Roles);


                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! You can login now.");

                    }

                }



            }

            return BadRequest("Something went wrong");

        }







        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] SignInRequestDto signInRequest)
        {
            var user = await userManager.FindByNameAsync(signInRequest.Username);

            if (user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, signInRequest.Password);

                if (checkPassword)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);


                    if (roles != null)
                    {
                        var jwttoken = tokenRepo.CreateJWTToken(user, roles.ToList());

                        var response = new SignInResponseDto
                        {

                            JwtToken = jwttoken,
                        };

                        return Ok(jwttoken);
                    }


                }

            }



            return BadRequest("Username or password is incorrect");

        }
    }
}
