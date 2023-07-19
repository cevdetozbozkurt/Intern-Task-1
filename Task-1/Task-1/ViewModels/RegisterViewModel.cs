using System.ComponentModel.DataAnnotations;

namespace Task_1.ViewModels
{
	public class RegisterViewModel
	{
		[Display(Name = "Email address")]
		[Required(ErrorMessage = "Email address is required")]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		[Display(Name = "Name")]
		[Required(ErrorMessage = "Name is required")]
		[DataType(DataType.Text)]
		public string Name { get; set; }

		[Display(Name = "Surname")]
		[Required(ErrorMessage = "Surname is required")]
		[DataType(DataType.Text)]
		public string Surname { get; set; }

		[Display(Name = "Street")]
		[DataType(DataType.Text)]
		[Required(ErrorMessage = "Street is required")]
		public string Street { get; set; }

		[Display(Name = "City")]
		[DataType(DataType.Text)]
		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }

		[Display(Name = "State")]
		[DataType(DataType.Text)]
		[Required(ErrorMessage = "State is required")]
		public string State { get; set; }

		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password do not match")]
		public string ConfirmPassword { get; set; }
	}
}
