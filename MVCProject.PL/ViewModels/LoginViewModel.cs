using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.ViewModels
{
	public class LoginViewModel
	{

		[Required(ErrorMessage = "Email  is Requied")]
        [EmailAddress(ErrorMessage = "Invaild Email")]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password  is Requied")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RemenberMe { get; set; }

	}
}
