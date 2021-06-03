using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{
    [Route("Error/{statusCode}")]
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    {
                        ViewBag.ErrorMessage = "Requested resource not found";
                        ViewBag.Path = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case 500:
                    {
                        ViewBag.ErrorMessage = "Requested resource not found";
                        ViewBag.Path = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }

                default:
                    break;
            }
            return View("Error");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
