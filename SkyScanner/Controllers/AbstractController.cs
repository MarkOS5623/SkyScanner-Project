using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SkyScanner.Controllers
{
    public abstract partial class ApplicationController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewData["userid"] ="";

            base.OnActionExecuted(filterContext);
        }
    }
}
