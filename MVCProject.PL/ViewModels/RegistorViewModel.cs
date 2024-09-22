using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.ViewModels
{
    public class RegistorViewModel
    {
        [Required(ErrorMessage ="First Name is Requied")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last Name is Requied")]

        public string LName { get; set; }
        [Required(ErrorMessage = "Email  is Requied")]
        [EmailAddress(ErrorMessage ="Invaild Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password  is Requied")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword  is Requied")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Not Match Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }



    }
}
