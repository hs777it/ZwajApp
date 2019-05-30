using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.Dtos
{
    public class UserFoRegisterDto
    {
        [Required(ErrorMessage="يجب إدخال اسم المستخدم")]
        public string Username { get; set; }
        [StringLength(8,MinimumLength=4,ErrorMessage = "يجب أن تكون كلمة السر من 4 إلى 8 حروف")]
        public string Password { get; set; }
    }
}