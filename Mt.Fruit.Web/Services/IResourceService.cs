using Csp.Web.Mvc.Paging;
using Mt.Fruit.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IResourceService
    {
        Task<HttpResponseMessage> Create(Resource resource);

        Task<HttpResponseMessage> Delete(int id);

        Task<Resource> GetResource(int id);

        Task<Resource> GetResource(int id, string ip, string browser, string device, string os, int userId = 0);

        Task<PagedResult<Resource>> GetResources(int categoryId, int page, int size);

        Task<PagedResult<Resource>> GetResources(string type,int userId, int page, int size);
    }
}
