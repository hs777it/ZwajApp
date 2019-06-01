using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.Dtos
{
    public class UserFoRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage = "يجب أن تكون كلمة السر من 4 إلى 8 حروف")]
        public string Password { get; set; }
    }
}