using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Logic.Service;
using WebShop.PL.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{

    [Authorize(Policy = "ManageCakes")]
    public class CakeManagementController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CakeManagementController(ICakeRepository cakeRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ViewResult> Index()
        {
            var cakes = await _cakeRepository.GetAllCakes();
            return View(cakes.OrderBy(p => p.CakeId));
        }

        public async Task<IActionResult> AddCake()
        {
            var categories =  await _categoryRepository.GetAllCategories();
            var cakeEditViewModel = new CakeEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                CategoryId = categories.FirstOrDefault().CategoryId
            };
            return View(cakeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCake(CakeEditViewModel cakeEditViewModel)
        {
            //Basic validation
            if (ModelState.IsValid)
            {
                var addResult = await _cakeRepository.CreateCake(cakeEditViewModel.Cake);
                //TempData["DbSave"] = _crudResult.GetDbSaveResult();
                //TempData["DbSet"] = _crudResult.DbSetResult;
                //TempData["Title"] = "Added";
                return RedirectToAction("Index");
            }
            return View(cakeEditViewModel);
        }
        //[Route("[controller]/EditCake/{cakeId?}")]
        public async Task<IActionResult> EditCake(Guid? cakeId)
        {
            var categories = await _categoryRepository.GetAllCategories();
            var cakeid = cakeId;
            var cakes = await  _cakeRepository.GetAllCakes();
            var cake = cakes.FirstOrDefault(p => p.CakeId == cakeid);
            var cakeEditViewModel = new CakeEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem()
                { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                Cake = cake,
                CategoryId = cake.CategoryId
            };

            var item = cakeEditViewModel.Categories.FirstOrDefault(c => c.Value == cake.CategoryId.ToString());
            item.Selected = true;

            return View(cakeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCake(CakeEditViewModel cakeEditViewModel)
        {
            cakeEditViewModel.Cake.CategoryId = cakeEditViewModel.CategoryId;

            if (ModelState.IsValid)
            {
                var updateResult = await _cakeRepository.UpdateCake(cakeEditViewModel.Cake);
                //TempData["DbSave"] = _crudResult.GetDbSaveResult();
                //TempData["Title"] = "Edit";
                return RedirectToAction("Index");
            }
            return View(cakeEditViewModel);
        }
        public async Task<IActionResult> DeleteCake(CakeEditViewModel cakeEditViewModel)
        {
            var cakes =  await _cakeRepository.GetAllCakes();
            return View(cakes.OrderBy(p => p.CakeId));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCake(Guid cakeId)
        {
            var cake = await _cakeRepository.GetCakeById(cakeId);
            await _cakeRepository.DeleteCake(cake);
            //TempData["DbSave"] = _crudResult.GetDbSaveResult();
            //TempData["Title"] = "Deleted";
            return RedirectToAction("Index");
        }

    }
}
