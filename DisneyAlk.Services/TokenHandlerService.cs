using DisneyAlk.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.Services
{
    public class TokenHandlerService:ITokenHandlerService
    {
        private readonly JwtConfig _jwtconfig;
        public TokenHandlerService(IOptionsMonitor<JwtConfig> ioptionsMonitor) 
        {
            _jwtconfig = ioptionsMonitor.CurrentValue;
        }

        public string GenerateJwtToken(ITokenParameters pars) {
            
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_jwtconfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                    new Claim("Id", pars.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, pars.Username),
                    new Claim(JwtRegisteredClaimNames.Email, pars.Username),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha512Signature)
            
            
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
