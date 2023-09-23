using Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Auth
{
    public class AuthToken
    {
        public string GenerateToken(UserDto userDto)
        {
          
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthInfo.keyString));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.FullName),            
                new Claim("Id", userDto.Id.ToString()),
                new Claim("UserRol",userDto.Role.Name)

            };

            List<Claim> ClaimList = new List<Claim>() { };
          
            ClaimList.AddRange(Claims);
            //payload

            var payload = new JwtPayload(
                 AuthInfo.IssuerString,
                 AuthInfo.AudienceString,
                 ClaimList,
                 DateTime.Now,
                 DateTime.UtcNow.AddHours(1))
            { 
                
            };
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
