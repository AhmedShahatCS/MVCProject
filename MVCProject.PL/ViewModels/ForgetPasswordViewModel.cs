using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email  is Requied")]
        [EmailAddress(ErrorMessage = "Invaild Email")]

        public string Email { get; set; }
    }
}
