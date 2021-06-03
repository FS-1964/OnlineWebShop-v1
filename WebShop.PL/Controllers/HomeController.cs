using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.PL.Filters;
using WebShop.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebShop.PL.ViewModels;
using WebShop.Logic.Service;
using WebShop.DAL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{
    //[ServiceFilter(typeof(TimerAction))]
    public class HomeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ICakeRepository cakeRepository, ILogger<HomeController> logger)
        {
            _cakeRepository = cakeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var homeViewModel = new HomeViewModel
            {
                CakesOfTheWeek =  await _cakeRepository.GetCakesOfTheWeek()
            };
            _logger.LogInformation(LogEventIds.LoadHomepage,"Loading HomePage");
            return View(homeViewModel);
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
