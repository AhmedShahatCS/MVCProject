using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password  is Requied")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword  is Requied")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Not Match Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
