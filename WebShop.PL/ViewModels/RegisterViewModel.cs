//using WebShop.DAL.Auth;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WebShop.PL.ViewModels
//{
//    public class RegisterViewModel : IViewModel
//    {
//        [Required]
//        [Display(Name = "User name")]
//        public string UserName { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }
//        [Required]
//        public string FirstName { get; set; }
//        [Required]
//        public string LastName { get; set; }
//        [Required]
//        public string AddressLine1 { get; set; }
//        public string AddressLine2 { get; set; }
//        [DateValidator]
//        [DataType(DataType.Date)]
//        public DateTime BirthDate { get; set; }
//        public string ZipCode { get; set; }

//        public string City { get; set; }

//        public string Country { get; set; }
//        public string State { get; set; }
//        public string PhoneNumber { get; set; }
//        [Required]
//        public string Email { get; set; }
//        public IFormFile Photo { get; set; }
//    }
//}

///////////////////////////////////////new code//////////////////////////////////////////////
///

using System.ComponentModel.DataAnnotations;

namespace WebShop.PL.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

