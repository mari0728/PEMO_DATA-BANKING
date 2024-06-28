using System.Web;
using System.Web.Mvc;

namespace PEMO_DATA_BANKING
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
