using KC.FileMan.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KC.FileMan.Web.Controllers.Filters
{
    public class DataSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            WebSessionManager.OpenSession("KC_FileMan");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            WebSessionManager.CloseSession("KC_FileMan");
        }
    }
}
