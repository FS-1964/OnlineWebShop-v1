using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShop.PL.Util;
using WebShop.PL.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;

        public ContactController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel contactviewmodel)
        {
            var message = new Message(new string[] { contactviewmodel.Email }, contactviewmodel.UserName, contactviewmodel.Message, contactviewmodel.AttachmentPath);
            await _emailSender.SendEmailAsync(message);
            return View("Confirm");
        }
    }
}
