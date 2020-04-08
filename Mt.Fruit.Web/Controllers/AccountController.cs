using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AccountController(IAuthService authService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public IActionResult Index(string returnUrl = null)
        {
            // If the user is already authenticated we do not need to display the login page, so we redirect to the landing page. 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            ViewBag.returnUrl = returnUrl;

            var response = await _authService.SignByPwd(model);
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.GetResult();

                ModelState.AddModelError(result.Msg.IndexOf("密码") > -1 ? "Password" : "UserName", result.Msg);
                return View("Index", model);
            }

            var user = await response.GetResult<User>();
            var accessTokenResult = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(model.UserName, AddMyClaims(user));

            //清空cookie身份信息
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal, accessTokenResult.AuthProperties);
            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public IActionResult Reg(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registered(RegModel user, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("reg", user);
            }
            
            ViewBag.returnUrl = returnUrl;

            var response=await _authService.Create(user);
            
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index), new { returnUrl });

            var result = await response.GetResult();

            ModelState.AddModelError("UserName", result.Msg);
            return View("reg", user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<Claim> AddMyClaims(User user)
        {
            var myClaims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name,user.UserInfo?.Name??""),
                //new Claim(ClaimTypes.Role,dto.Role.ToString()),
                //new Claim(ClaimTypes.Email,user.UserInfo?.Email??""),
                new Claim(ClaimTypes.MobilePhone,user.Cell),
                new Claim(ClaimTypes.NameIdentifier,user.NickName??""),
                new Claim(ClaimTypes.GroupSid,$"{user.TenantId}"),
                new Claim(ClaimTypes.Sid,$"{user.Id}"),
                new Claim("HeadImgUrl",user.HeadImgUrl??""),
                new Claim("UserName",user.UserLogin.UserName),
                new Claim("WebSiteId",$"{user.UserLogin.WebSiteId}"),
                //new Claim("OpenId",user.ExternalLogin?.OpenId??""),
                //new Claim("WebSiteId",$"{user.ExternalLogin?.WebSiteId??user.UserLogin?.WebSiteId??0}"),
                new Claim("aud", "OAuth"),
                new Claim("aud", "Blog"),
                new Claim("aud", "Upload"),
                new Claim("aud", "AskApi"),
                new Claim("aud", "AskWeb"),
                new Claim("aud", "MtWeb")
            };

            return myClaims;
        }
    }
}
