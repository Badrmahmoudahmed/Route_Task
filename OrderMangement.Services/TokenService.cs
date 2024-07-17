using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var authcredentials = new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:AuthClaim,
                signingCredentials: authcredentials,
                expires: DateTime.Now.AddDays(1)
                
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
