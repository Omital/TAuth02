using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace TAuth02.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TAuth02ControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}