using System;
using System.Collections.Generic;
using System.Linq;

namespace Mt.Ask.Web.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpenDate { get; set; }

        public decimal Fee { get; set; }

        public string Address { get; set; }

        public string BaseInfo { get; set; }

        public string BackSummary { get; set; }

        public string TeaSummary { get; set; }

        public string Summary { get; set; }
    }

    public static class CourseFactory
    {
        public static IEnumerable<Article> ToArticles(this IEnumerable<Course> courses)
        {
            return courses?.Select(a => ToArticle(a));
        }

        public static Article ToArticle(this Course course)
        {
            return new Article
            {
                Id=course.Id,
                Title=course.Name,
                CreatedAt=course.OpenDate
            };
        }
    }
}
