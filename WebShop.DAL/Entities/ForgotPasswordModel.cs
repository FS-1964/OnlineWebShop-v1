using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL.Entities
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
