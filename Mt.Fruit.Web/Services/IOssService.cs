using Csp.Web;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IOssService
    {
        Task<OptResult> Upload(IFormFile file,string dir);
    }
}
