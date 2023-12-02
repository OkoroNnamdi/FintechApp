using FinTech.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Utilities
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }
        public string CreateToken(UserModel user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return Token(authClaims);
        }

        private string Token(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            expires: DateTime.Now.AddHours(5),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public RefreshToken SetRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Refreshtoken = Guid.NewGuid().ToString(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
            };
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.RefreshTokenExpiryTime
            };
            _httpContext.HttpContext.Response.Cookies.Append("refresh-token", refreshToken.Refreshtoken.ToString(), cookieOptions);
            return refreshToken;
        }
    }
}
