using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dtos.Token;
using WebApi.Models;

namespace WebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(ProvidedTokenDto providedToken)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Token token = await _context.Tokens.FirstOrDefaultAsync(t => t.Content.Equals(providedToken.Content));

            if (token == null)
            {
                response.Success = false;
                response.Message = "Token is not valid.";
            }
            else
            {
                response.Data = CreateToken(token);
            }

            return response;
        }

        private string CreateToken(Token token)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, token.Id.ToString()),
            };
            
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );
            
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =  new ClaimsIdentity(claims),
                Expires =  DateTime.Now.AddDays(14),
                SigningCredentials = creds
            };
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(jwtToken);
        }
    }
}