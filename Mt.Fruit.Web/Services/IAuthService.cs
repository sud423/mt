using Mt.Fruit.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IAuthService
    {

        Task<HttpResponseMessage> Create(RegModel model);

        Task<HttpResponseMessage> SignByPwd(LoginModel model);
    }
}
