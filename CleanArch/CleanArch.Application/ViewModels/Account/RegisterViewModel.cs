using System.ComponentModel.DataAnnotations;

namespace CleanArch.Application.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
