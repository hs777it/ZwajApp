using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.Dtos
{
    public class UserFoRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage="")]
        [StringLength(16,MinimumLength=4,ErrorMessage = "كلمة السر من 4 إلى 16 حرف")]
        public string Password { get; set; }
    }
}