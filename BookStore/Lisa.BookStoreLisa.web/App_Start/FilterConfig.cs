using System.Web;
using System.Web.Mvc;

namespace Lisa.BookStoreLisa.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
