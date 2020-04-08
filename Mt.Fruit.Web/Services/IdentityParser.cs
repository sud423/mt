using Csp.Jwt;
using Microsoft.AspNetCore.Http;
using Mt.Fruit.Web.Models;
using System.Linq;
using System.Security.Claims;

namespace Mt.Ask.Web.Services
{
    public class IdentityParser : IIdentityParser<User>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public IdentityParser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User Parse()
        {
            if (_httpContextAccessor.HttpContext.User is ClaimsPrincipal claims)
            {
                return new User
                {
                    Id = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? "0"),
                    TenantId = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GroupSid)?.Value ?? "0"),
                    UserLogin=new UserLogin() { 
                        UserName=claims.Claims.FirstOrDefault(a=>a.Type== ClaimTypes.NameIdentifier)?.Value??"",
                        WebSiteId = int.Parse(claims.Claims.FirstOrDefault(a => a.Type == "WebSiteId")?.Value ?? "0") 
                    },
                    //ExternalLogin=new ExternalLogin
                    //{
                    //    OpenId = claims.Claims.FirstOrDefault(a => a.Type == "OpenId")?.Value ?? "",
                    //    WebSiteId = int.Parse(claims.Claims.FirstOrDefault(a => a.Type == "WebSiteId")?.Value ?? "0")
                    //},
                    NickName = claims.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value ?? "",
                    HeadImgUrl = claims.Claims.FirstOrDefault(a => a.Type == "HeadImgUrl")?.Value ?? ""
                };
            }

            return null;
        }
    }
}
