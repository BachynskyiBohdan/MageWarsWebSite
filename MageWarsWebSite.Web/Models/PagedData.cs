using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MageWarsWebSite.Web.Models
{
    public static class PagedData
    {
        public static IEnumerable<T> GetPagedQuery<T>(IQueryable<T> query, int page, int count) where T : class
        {
            var skip = (page - 1) * count;
            return query.AsEnumerable().Skip(skip).Take(count);
        }
    }

    public class NavigationViewModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }

        public int ElementsCount { get; set; }
        public int ElementsPerPage { get; set; }
    }
}