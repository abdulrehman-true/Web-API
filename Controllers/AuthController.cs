using Microsoft.AspNetCore.Mvc;
using Web_API.DTO.UserDTO;
using Web_API.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO request)
        {
            var response = await _authRepo.Register(
                new User{UserName = request.UserName}, request.Password
            );
            if(!response.Success)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDTO request)
        {
            var response = await _authRepo.Login(request.UserName, request.Passwowrd);
            if(!response.Success)
                return BadRequest(response);
            return Ok(response);

        }
    }
}