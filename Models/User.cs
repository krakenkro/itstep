using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class User
    {
        public Guid Id { get; set; } //Id первый первичный ключ 


        [Required(ErrorMessage = "Login is required.")]
        [Display(Name = "User login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "User Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Display(Name = "User Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
