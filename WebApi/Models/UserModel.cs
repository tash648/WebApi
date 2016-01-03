using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickErrandsWebApi.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль {0} должен содержать минимум {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль из подверждение пороля не равны.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public List<string> Roles { get; set; }
    }
}