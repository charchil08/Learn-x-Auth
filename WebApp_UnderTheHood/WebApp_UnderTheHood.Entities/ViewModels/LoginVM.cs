using System.ComponentModel.DataAnnotations;

namespace WebApp_UnderTheHood.Entities.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "User name / Email is Required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Display(Name = "Remeber Me")]
        public bool RememberMe { get; set; }
    }
}
