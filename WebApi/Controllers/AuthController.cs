using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dtos.Token;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(ProvidedTokenDto providedToken)
        {
            ServiceResponse<string> response = await _authRepository.Login(providedToken);

            if (!response.Success)
                return BadRequest(response);
            else
                return Ok(response);
        }
    }
}