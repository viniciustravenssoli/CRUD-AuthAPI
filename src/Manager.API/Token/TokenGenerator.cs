using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Manager.Services.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Manager.API.Token{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDTO userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", userDTO.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDTO.Name),
                    new Claim(ClaimTypes.Email, userDTO.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}