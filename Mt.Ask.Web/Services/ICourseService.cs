using Mt.Ask.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface ICourseService
    {
        Task<Course> GetCourse(int id);

        Task<IEnumerable<Course>> GetCourses(int size);

        Task<IEnumerable<Course>> GetCoursesByCondtion(string academy, string classify);

        Task<IEnumerable<Course>> GetHotCourses();
    }
}
