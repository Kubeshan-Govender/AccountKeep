using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
    {

        // Checks if user is logged in via session
        // If not, redirects to Login page before action executes
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
        {
            // Not logged in
            context.Result = RedirectToAction("Index", "Login");
        }

        // Calls base to continue normal execution if logged in
        base.OnActionExecuting(context);
    }
}
