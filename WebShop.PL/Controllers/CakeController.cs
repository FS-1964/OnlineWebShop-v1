using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebShop.DAL.Entities;
using WebShop.Logic.Service;
using WebShop.PL.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ICakeReviewRepository _cakeReviewRepository;
        public CakeController(ICakeRepository cakeRepository, ICakeReviewRepository cakeReviewRepository, ICategoryRepository categoryRepository, HtmlEncoder htmlEncoder)
        {
            _cakeRepository = cakeRepository;
            _categoryRepository = categoryRepository;
            _cakeReviewRepository = cakeReviewRepository;
            _htmlEncoder = htmlEncoder;
        }

        public async Task<ViewResult> List(string category)
        {
            IEnumerable<Cake> cakes;
            string currentCategory=string.Empty;
            Category selectedCategory=null;
            
            if (string.IsNullOrEmpty(category))
            {
                var result =  await _cakeRepository.GetAllCakes();
                cakes = result.OrderByDescending(p => p.CakeId);
                currentCategory = "All cakes";
            }
            else
            {

                selectedCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category);
                cakes =  await _cakeRepository.GetCakesByCategory(selectedCategory);
              
            }

            return View(new CakesListViewModel
            {
                Cakes = cakes,
                CurrentCategory = selectedCategory?.CategoryName

            });
        }

        [Route("[controller]/Details/{id?}")]
        public async Task<IActionResult> Details(Guid? id)
        {
           
            var cake = await _cakeRepository.GetCakeById(id );
            if (cake == null)
                return NotFound();

            return View(new CakeDetailViewModel() { Cake = cake });
        }

        [Route("[controller]/Details/{id?}")]
        [HttpPost]
        public async Task<IActionResult> Details(Guid? id, string review)
        {
            //throw new Exception("error in list");
            var cake = await _cakeRepository.GetCakeById(id);
            if (cake == null)
                return NotFound();
            string encodedReview = _htmlEncoder.Encode(review);
            var addResult = await _cakeReviewRepository.AddCakeReview(new CakeReview()
            { Cake = cake, Review = encodedReview });
            return View(new CakeDetailViewModel() { Cake = cake });
        }
    }
}
