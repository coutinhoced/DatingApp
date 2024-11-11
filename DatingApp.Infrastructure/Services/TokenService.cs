using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Common.Options;
using DatingApp.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private string TokenKey;
        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
            TokenKey = tokenOptions.Value.TokenKey;
        }
        public string CreateToken(AppUser user)
        {
            if (string.IsNullOrEmpty(TokenKey))
            {
                throw new Exception("Token key is not provided");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
