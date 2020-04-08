using Csp.Web;
using Mt.Ask.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface IAuthService
    {
        Task<string> GetAuthUrl(string redirectUrl);

        Task<User> GetUser(string code);

        Task<OptResult> BindCell(string cell, int userId);

        Task<HttpResponseMessage> SignByPwd(LoginModel model);
    }
}
