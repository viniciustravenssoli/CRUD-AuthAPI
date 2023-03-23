using System;
using System.Threading.Tasks;
using Manager.API.Token;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Manager.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        private readonly IUserService _userService;


        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator, IUserService userService)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        [HttpPost]
        [Route("/api/v1/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _userService.Auth(loginViewModel.Email, loginViewModel.Password);

                if (user != null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuario autenticado com sucesso",
                        Success = true,
                        Data = new
                        {
                            Token = _tokenGenerator.GenerateToken(user),
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]))
                        }
                    });
                }

                else
                {
                    return StatusCode(401, Responses.UnauthorizedErrorMessage());
                }
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}