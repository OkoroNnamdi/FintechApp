using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Utilities
{
    public class TokenDetails : ITokenDetails
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenDetails(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes .NameIdentifier);
        }

        public string GetRoles()
        {
          return   _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
