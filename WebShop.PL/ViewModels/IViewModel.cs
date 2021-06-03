using Microsoft.AspNetCore.Http;

namespace WebShop.PL.ViewModels
{
    public interface IViewModel
    {
        public IFormFile Photo { get; set; }
    }
}